using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.Rest.Spot;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients.Rest.Spot
{
    /// <summary>
    /// Client for the Huobi REST API
    /// </summary>
    public class HuobiClientSpot : RestClient, IHuobiClientSpot, IExchangeClient
    {
        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        public new HuobiClientSpotOptions ClientOptions { get; }

        #region SubClients

        public IHuobiClientSpotAccount Account { get; }
        public IHuobiClientSpotExchangeData ExchangeData { get; }
        public IHuobiClientSpotTrading Trading { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of HuobiClient using the default options
        /// </summary>
        public HuobiClientSpot() : this(HuobiClientSpotOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiClient with the provided options
        /// </summary>
        public HuobiClientSpot(HuobiClientSpotOptions options) : base("Huobi", options, options.ApiCredentials == null ? null : new HuobiAuthenticationProvider(options.ApiCredentials, options.SignPublicRequests))
        {
            ClientOptions = options;
            manualParseError = true;

            Account = new HuobiClientSpotAccount(this);
            ExchangeData = new HuobiClientSpotExchangeData(this);
            Trading = new HuobiClientSpotTrading(this);
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(HuobiClientSpotOptions options)
        {
            HuobiClientSpotOptions.Default = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new HuobiAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), ClientOptions.SignPublicRequests));
        }
                
        internal async Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiApiResponseV2<T>>(uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

            if (result.Data.Code != 200)
                return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return new WebCallResult<(T, DateTime)>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

            if (result.Data.ErrorCode != null)
                return new WebCallResult<(T, DateTime)>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError($"{result.Data.ErrorCode}-{result.Data.ErrorMessage}"));

            return result.As((result.Data.Data, result.Data.Timestamp));
        }

        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1).ConfigureAwait(false);
            if (!result || result.Data == null)
                return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

            if (result.Data.ErrorCode != null)
                return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed,
            HttpMethodParameterPosition parameterPosition, ArrayParametersSerialization arraySerialization, int requestId,
            Dictionary<string, string>? additionalHeaders)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var uriString = uri.ToString();
            if (authProvider != null)
                parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed, parameterPosition, arraySerialization);

            if (parameterPosition == HttpMethodParameterPosition.InUri && parameters?.Any() == true)
                uriString += "?" + parameters.CreateParamString(true, arraySerialization);

            if (method == HttpMethod.Post && signed)
            {
                var uriParamNames = new[] { "AccessKeyId", "SignatureMethod", "SignatureVersion", "Timestamp", "Signature" };
                var uriParams = parameters.Where(p => uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
                uriString += "?" + uriParams.CreateParamString(true, ArrayParametersSerialization.MultipleValues);
                parameters = parameters.Where(p => !uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
            }

            var contentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
            var request = RequestFactory.Create(method, uriString, requestId);
            request.Accept = Constants.JsonContentHeader;

            var headers = new Dictionary<string, string>();
            if (authProvider != null)
                headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed, parameterPosition, arraySerialization);

            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                    request.AddHeader(header.Key, header.Value);
            }

            if (StandardRequestHeaders != null)
            {
                foreach (var header in StandardRequestHeaders)
                    // Only add it if it isn't overwritten
                    if (additionalHeaders?.ContainsKey(header.Key) != true)
                        request.AddHeader(header.Key, header.Value);
            }

            if (parameterPosition == HttpMethodParameterPosition.InBody)
            {
                if (parameters?.Any() == true)
                    WriteParamBody(request, parameters, contentType);
                else
                    request.SetContent("{}", contentType);
            }

            return request;
        }

        /// <inheritdoc />
        protected override Task<ServerError?> TryParseErrorAsync(JToken data)
        {
            if(data["code"] != null && data["code"]?.Value<int>() != 200)
                return Task.FromResult<ServerError?>(new ServerError($"{(string)data["code"]!}, {(string)data["message"]!}"));

            if (data["err-code"] == null && data["err-msg"] == null)
                return Task.FromResult<ServerError?>(null);

            return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["err-code"] == null || error["err-msg"] == null)
                return new ServerError(error.ToString());

            return new ServerError($"{(string)error["err-code"]!}, {(string)error["err-msg"]!}");
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, string? version = null)
        {
            return version == null ? new Uri($"{ClientOptions.BaseAddress}{endpoint}") : new Uri($"{ClientOptions.BaseAddress}v{version}/{endpoint}");
        }

        internal new string FillPathParameter(string path, params string[] values) => BaseClient.FillPathParameter(path, values);

        internal static long? ToUnixTimestamp(DateTime? time)
        {
            if (time == null)
                return null;
            return (long)(time.Value - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
        #endregion

        #region common interface
        /// <summary>
        /// Get the name of a symbol for Huobi based on the base and quote asset
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + quoteAsset).ToLowerInvariant();

#pragma warning disable 1066
        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var symbols = await ExchangeData.GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var tickers = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            return tickers.As<ICommonTicker>(Enumerable.Where<HuobiSymbolTick>(tickers.Data?.Ticks, w => w.Symbol == symbol).Select(t => (ICommonTicker)t).FirstOrDefault());
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var tickers = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            return tickers.As<IEnumerable<ICommonTicker>>(Enumerable.Select<HuobiSymbolTick, ICommonTicker>(tickers.Data?.Ticks, t => (ICommonTicker)t));
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (startTime != null || endTime != null)
                return WebCallResult<IEnumerable<ICommonKline>>.CreateErrorResult(new ArgumentError($"Huobi does not support the {nameof(startTime)}/{nameof(endTime)} parameters for the method {nameof(IExchangeClient.GetKlinesAsync)}"));

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), limit ?? 500).ConfigureAwait(false);
            return klines.As<IEnumerable<ICommonKline>>(klines.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await ExchangeData.GetOrderBookAsync(symbol, 0).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var trades = await ExchangeData.GetTradeHistoryAsync(symbol, 100).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonRecentTrade>>(trades.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            if (accountId == null)
                return WebCallResult<ICommonOrderId>.CreateErrorResult(new ArgumentError(
                    $"Huobi needs the {nameof(accountId)} parameter for the method {nameof(IExchangeClient.PlaceOrderAsync)}"));

            var huobiType = GetOrderType(type, side);
            var result = await Trading.PlaceOrderAsync(long.Parse(accountId), symbol, huobiType, quantity, price).ConfigureAwait(false);
            if (!result)
                return WebCallResult<ICommonOrderId>.CreateErrorResult(result.ResponseStatusCode,
                    result.ResponseHeaders, result.Error!);
            return result.As<ICommonOrderId>(new HuobiPlacedOrder()
            {
                Id = result.Data
            });
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol)
        {
            var order = await Trading.GetOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return order.As<ICommonOrder>(order.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var result = await Trading.GetOrderTradesAsync(long.Parse(orderId)).ConfigureAwait(false);
            return result.As<IEnumerable<ICommonTrade>>(result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol)
        {
            var orders = await Trading.GetOpenOrdersAsync(symbol: symbol).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol)
        {
            var result = await Trading.GetOrdersAsync(
                states: new[]
                {
                    OrderState.Filled
                }, symbol).ConfigureAwait(false);
            return result.As<IEnumerable<ICommonOrder>>(result.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol)
        {
            var result = await Trading.CancelOrderAsync(long.Parse(orderId)).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result ? new HuobiOrder() { Id = result.Data } : null);
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            if (accountId == null)
                return WebCallResult<IEnumerable<ICommonBalance>>.CreateErrorResult(new ArgumentError(
                    $"Huobi needs the {nameof(accountId)} parameter for the method {nameof(IExchangeClient.GetBalancesAsync)}"));

            var balances = await Account.GetBalancesAsync(long.Parse(accountId)).ConfigureAwait(false);
            if (!balances)
                return WebCallResult<IEnumerable<ICommonBalance>>.CreateErrorResult(balances.ResponseStatusCode,
                    balances.ResponseHeaders, balances.Error!);

            var result = new List<HuobiBalanceWrapper>();
            foreach (var balance in balances.Data)
            {
                if (balance.Type == BalanceType.Interest || balance.Type == BalanceType.Loan)
                    continue;

                var existing = result.SingleOrDefault(b => b.Asset == balance.Asset);
                if (existing == null)
                {
                    existing = new HuobiBalanceWrapper() { Asset = balance.Asset };
                    result.Add(existing);
                }

                if (balance.Type == BalanceType.Frozen)
                    existing.Frozen = balance.Balance;
                else
                    existing.Trade = balance.Balance;
            }

            return balances.As<IEnumerable<ICommonBalance>>(result);
        }
#pragma warning restore 1066

        private static OrderType GetOrderType(IExchangeClient.OrderType type, IExchangeClient.OrderSide side)
        {
            if (side == IExchangeClient.OrderSide.Sell)
            {
                if (type == IExchangeClient.OrderType.Limit)
                    return OrderType.LimitSell;
                return OrderType.MarketSell;
            }
            else
            {
                if (type == IExchangeClient.OrderType.Limit)
                    return OrderType.LimitBuy;
                return OrderType.MarketBuy;
            }
        }

        private static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;
            if (timeSpan == TimeSpan.FromDays(365)) return KlineInterval.OneYear;

            throw new ArgumentException("Unsupported timespan for Huobi Klines, check supported intervals using Huobi.Net.Objects.HuobiPeriod");
        }
        #endregion
    }
}
