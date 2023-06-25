using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiRestClientSpotApi : RestApiClient, IHuobiClientSpotApi, ISpotClient
    {
        /// <inheritdoc />
        public new HuobiRestOptions ClientOptions => (HuobiRestOptions)base.ClientOptions;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public string ExchangeName => "Huobi";

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IHuobiClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHuobiClientSpotApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HuobiRestClientSpotApi(ILogger logger, HttpClient? httpClient, HuobiRestOptions options)
            : base(logger, httpClient, options.Environment.RestBaseAddress, options, options.SpotOptions)
        {
            Account = new HuobiRestClientSpotApiAccount(this);
            ExchangeData = new HuobiRestClientSpotApiExchangeData(this);
            Trading = new HuobiRestClientSpotApiTrading(this);

            manualParseError = true;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        #region methods

        internal async Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiApiResponseV2<T>>(uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<(T, DateTime)>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<(T, DateTime)>(new ServerError($"{result.Data.ErrorCode}-{result.Data.ErrorMessage}"));

            return result.As((result.Data.Data, result.Data.Timestamp));
        }

        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<ServerError?> TryParseErrorAsync(JToken data)
        {
            if (data["code"] != null && data["code"]?.Value<int>() != 200)
            {
                if (data["err-code"] != null)
                    return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));

                return Task.FromResult<ServerError?>(new ServerError($"{(string)data["code"]!}, {(string)data["message"]!}"));
            }

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
            if (version == null)
                return new Uri(BaseAddress.AppendPath(endpoint));
            return new Uri(BaseAddress.AppendPath($"v{version}", endpoint));
        }

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
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

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(d => new Symbol
            {
                SourceObject = d,
                Name = d.Name,
                MinTradeQuantity = d.MinLimitOrderQuantity,
                PriceDecimals = d.PricePrecision,
                QuantityDecimals = d.QuantityPrecision
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var tickers = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<Ticker>(null);

            var ticker = tickers.Data.Ticks.SingleOrDefault(s => s.Symbol == symbol);
            return tickers.As(new Ticker
            {
                SourceObject =ticker,
                HighPrice = ticker.HighPrice,
                Symbol = ticker.Symbol,
                LastPrice = ticker.ClosePrice,
                LowPrice = ticker.LowPrice,
                Price24H = ticker.OpenPrice,
                Volume = ticker.Volume
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var tickers = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<IEnumerable<Ticker>>(null);

            return tickers.As(tickers.Data.Ticks.Select(t => new Ticker
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                Symbol = t.Symbol,
                LastPrice = t.ClosePrice,
                LowPrice = t.LowPrice,
                Price24H = t.OpenPrice,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            if (startTime != null || endTime != null)
                throw new ArgumentException($"Huobi does not support the {nameof(startTime)}/{nameof(endTime)} parameters for the method {nameof(IBaseRestClient.GetKlinesAsync)}");

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), limit ?? 500, ct: ct).ConfigureAwait(false);
            if (!klines)
                return klines.As<IEnumerable<Kline>>(null);

            return klines.As(klines.Data.Select(d => new Kline
            {
                SourceObject = d,
                ClosePrice = d.ClosePrice,
                HighPrice = d.HighPrice,
                LowPrice = d.LowPrice,
                OpenPrice = d.OpenPrice,
                OpenTime = d.OpenTime,
                Volume = d.Volume
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

            var book = await ExchangeData.GetOrderBookAsync(symbol, 0, ct: ct).ConfigureAwait(false);
            if (!book)
                return book.As<OrderBook>(null);

            return book.As(new OrderBook
            {
                SourceObject = book.Data,
                Asks = book.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = book.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));
            
            var trades = await ExchangeData.GetTradeHistoryAsync(symbol, 100, ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.SelectMany(t => t.Details).Select(t => new Trade
            {
                SourceObject = t,
                Price = t.Price,
                Quantity = t.Quantity,
                Symbol = symbol,
                Timestamp = t.Timestamp
            }));
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
                throw new ArgumentException(nameof(accountId) + " required for Huobi " + nameof(ISpotClient.PlaceOrderAsync), nameof(accountId));

            var huobiType = GetOrderType(type);
            var result = await Trading.PlaceOrderAsync(id, symbol, side == CommonOrderSide.Sell ? Enums.OrderSide.Sell: Enums.OrderSide.Buy, huobiType, quantity, price, clientOrderId: clientOrderId, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);
            return result.As(new OrderId()
            {
                SourceObject = result.Data,
                Id = result.Data.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if(!long.TryParse(orderId, out var id))
                throw new ArgumentException("Invalid order id for Huobi " + nameof(ISpotClient.GetOrderAsync), nameof(orderId));

            var order = await Trading.GetOrderAsync(id, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order.Data,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture),
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Symbol = order.Data.Symbol,
                Timestamp = order.Data.CreateTime,
                Side = order.Data.Side == OrderSide.Buy ? CommonOrderSide.Buy: CommonOrderSide.Sell,
                Type = order.Data.Type == OrderType.Limit ? CommonOrderType.Limit: order.Data.Type == OrderType.Market ? CommonOrderType.Market: CommonOrderType.Other,
                Status = order.Data.State == OrderState.Canceled || order.Data.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled: order.Data.State == OrderState.Filled ? CommonOrderStatus.Filled: CommonOrderStatus.Active
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Invalid order id for Huobi " + nameof(ISpotClient.GetOrderAsync), nameof(orderId));

            var result = await Trading.GetOrderTradesAsync(id, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<UserTrade>>(null);

            return result.As(result.Data.Select(t => new UserTrade
            {
                SourceObject = t,
                Id = t.Id.ToString(CultureInfo.InvariantCulture),
                OrderId = t.OrderId.ToString(CultureInfo.InvariantCulture),
                Symbol = t.Symbol,
                Fee = t.Fee,
                FeeAsset = t.FeeAsset,
                Price = t.Price,
                Quantity = t.Quantity,
                Timestamp = t.Timestamp
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await Trading.GetOpenOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            return orders.As(orders.Data.Select(o =>            
                new Order
                {
                    SourceObject = o,
                    Id = o.Id.ToString(CultureInfo.InvariantCulture),
                    Price = o.Price,
                    Quantity = o.Quantity,
                    QuantityFilled = o.QuantityFilled,
                    Symbol = o.Symbol,
                    Timestamp = o.CreateTime,
                    Side = o.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Type = o.Type == OrderType.Limit ? CommonOrderType.Limit : o.Type == OrderType.Market ? CommonOrderType.Market : CommonOrderType.Other,
                    Status = o.State == OrderState.Canceled || o.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled : o.State == OrderState.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
                }
            ));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(ISpotClient.GetClosedOrdersAsync), nameof(symbol));

            var result = await Trading.GetClosedOrdersAsync(symbol!, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Order>>(null);

            return result.As(result.Data.Select(o =>
                new Order
                {
                    SourceObject = o,
                    Id = o.Id.ToString(CultureInfo.InvariantCulture),
                    Price = o.Price,
                    Quantity = o.Quantity,
                    QuantityFilled = o.QuantityFilled,
                    Symbol = o.Symbol,
                    Timestamp = o.CreateTime,
                    Side = o.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Type = o.Type == OrderType.Limit ? CommonOrderType.Limit : o.Type == OrderType.Market ? CommonOrderType.Market : CommonOrderType.Other,
                    Status = o.State == OrderState.Canceled || o.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled : o.State == OrderState.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
                }
            ));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Invalid order id for Huobi " + nameof(ISpotClient.CancelOrderAsync), nameof(orderId));

            var result = await Trading.CancelOrderAsync(id, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);

            return result.As(new OrderId
            {
                SourceObject = result.Data,
                Id = result.Data.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
                throw new ArgumentException(nameof(accountId) + " required for Huobi " + nameof(ISpotClient.GetBalancesAsync), nameof(accountId));

            var balances = await Account.GetBalancesAsync(long.Parse(accountId), ct: ct).ConfigureAwait(false);
            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);

            var result = new List<Balance>();
            foreach (var balance in balances.Data)
            {
                if (balance.Type == BalanceType.Interest || balance.Type == BalanceType.Loan)
                    continue;

                var existing = result.SingleOrDefault(b => b.Asset == balance.Asset);
                if (existing == null)
                {
                    existing = new Balance() { Asset = balance.Asset };
                    result.Add(existing);
                }

                if (balance.Type == BalanceType.Frozen)
                {
                    existing.Total += balance.Balance;
                }
                else
                {
                    existing.Total += balance.Balance;
                    existing.Available = balance.Balance;
                }
            }

            return balances.As<IEnumerable<Balance>>(result);
        }

        private static Enums.OrderType GetOrderType(CommonOrderType type)
        {
            if (type == CommonOrderType.Limit)
                return Enums.OrderType.Limit;
            return Enums.OrderType.Market;
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
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        /// TODO make this take an accountId param so we don't need it in the interface?
        public ISpotClient CommonSpotClient => this;
    }
}
