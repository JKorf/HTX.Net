using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiClientSpotApi : RestApiClient, IHuobiClientSpotApi, IExchangeClient
    {
        private readonly HuobiClient _baseClient;
        private readonly HuobiClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IHuobiClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHuobiClientSpotApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HuobiClientSpotApi(Log log, HuobiClient baseClient, HuobiClientOptions options)
            : base(options, options.SpotApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new HuobiClientSpotApiAccount(this);
            ExchangeData = new HuobiClientSpotApiExchangeData(this);
            Trading = new HuobiClientSpotApiTrading(this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, _options.SignPublicRequests);

        #region methods

        internal Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiV2Request<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiTimestampRequest<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
            => _baseClient.SendHuobiRequest<T>(this, uri, method, cancellationToken, parameters, signed, weight);

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, string? version = null)
        {
            if (version == null)
                return new Uri(BaseAddress.AppendPath(endpoint));
            return new Uri(BaseAddress.AppendPath($"v{version}", endpoint));
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
            return tickers.As((tickers.Data?.Ticks).Where(w => w.Symbol == symbol).Select(t => (ICommonTicker)t).FirstOrDefault());
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var tickers = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            return tickers.As((tickers.Data?.Ticks).Select(t => (ICommonTicker)t));
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
            var result = await Trading.PlaceOrderAsync(long.Parse(accountId), symbol, side == IExchangeClient.OrderSide.Sell ? OrderSide.Sell: OrderSide.Buy, huobiType, quantity, price).ConfigureAwait(false);
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
            if (symbol == null)
                return WebCallResult<IEnumerable<ICommonOrder>>.CreateErrorResult(new ArgumentError(
                    $"Huobi needs the {nameof(symbol)} parameter for the method {nameof(IExchangeClient.GetClosedOrdersAsync)}"));

            var result = await Trading.GetClosedOrdersAsync(symbol).ConfigureAwait(false);
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
            if (type == IExchangeClient.OrderType.Limit)
                return OrderType.Limit;
            return OrderType.Market;
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

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.SpotApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;
    }
}
