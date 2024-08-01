using CryptoExchange.Net.Clients;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces.CommonClients;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApi : RestApiClient, IHTXRestClientSpotApi, ISpotClient
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

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
        public string ExchangeName => "HTX";

        internal readonly string _brokerId;

        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiMargin Margin { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HTXRestClientSpotApi(ILogger logger, HttpClient? httpClient, HTXRestOptions options)
            : base(logger, httpClient, options.Environment.RestBaseAddress, options, options.SpotOptions)
        {
            Account = new HTXRestClientSpotApiAccount(this);
            ExchangeData = new HTXRestClientSpotApiExchangeData(this);
            SubAccount = new HTXRestClientSpotApiSubAccount(this);
            Margin = new HTXRestClientSpotApiMargin(this);
            Trading = new HTXRestClientSpotApiTrading(this);

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "AA1ef14811";
        }
        #endregion

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToLowerInvariant()}{quoteAsset.ToLowerInvariant()}";

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        #region methods
        internal async Task<WebCallResult<T>> SendToAddressRawAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) 
        {
            var result = await base.SendAsync<HTXApiResponseV2<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<(T, DateTime)>> SendHTXTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HTXBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: 0).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<(T, DateTime)>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<(T, DateTime)>(new ServerError($"{result.Data.ErrorCode}-{result.Data.ErrorMessage}"));

            return result.As((result.Data.Data, result.Data.Timestamp));
        }

        internal Task<WebCallResult<(T, DateTime)>> SendTimestampAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendTimestampToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<(T, DateTime)>> SendTimestampToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<(T, DateTime)>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<(T, DateTime)>(new ServerError($"{result.Data.ErrorCode}-{result.Data.ErrorMessage}"));

            return result.As((result.Data.Data, result.Data.Timestamp));
        }

        internal Task<WebCallResult> SendBasicAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendBasicToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendBasicToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsDatalessError(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsDatalessError(new ServerError(result.Data.ErrorCode.Value, result.Data.ErrorMessage!));

            return result.AsDataless();

        }

        internal Task<WebCallResult<T>> SendBasicAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendBasicToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendBasicToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode.Value, result.Data.ErrorMessage!));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<string>(MessagePath.Get().Property("err-code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg"));

            if (code == null || msg == null)
                return new ServerError(accessor.GetOriginalString());

            return new ServerError($"{code}, {msg}");
        }

        /// <inheritdoc />
        protected override ServerError? TryParseError(IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<string>(MessagePath.Get().Property("err-code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg"));

            if (code == null && msg == null)
                return null;

            return new ServerError($"{code}, {msg}");
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
        /// Get the name of a symbol for HTX based on the base and quote asset
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + quoteAsset).ToLowerInvariant();

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolConfigAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(d => new Symbol
            {
                SourceObject = d,
                Name = d.Symbol,
                MinTradeQuantity = d.MinOrderQuantity,
                PriceDecimals = d.PricePrecision,
                QuantityDecimals = d.QuantityPrecision
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var tickers = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<Ticker>(null);

            var ticker = tickers.Data.Ticks.SingleOrDefault(s => s.Symbol == symbol);
            if (ticker == null)
                return tickers.AsError<Ticker>(new ServerError("Symbol not found"));

            return tickers.As(new Ticker
            {
                SourceObject = ticker,
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
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            if (startTime != null || endTime != null)
                throw new ArgumentException($"HTX does not support the {nameof(startTime)}/{nameof(endTime)} parameters for the method {nameof(IBaseRestClient.GetKlinesAsync)}");

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
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

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
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));
            
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
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
                throw new ArgumentException(nameof(accountId) + " required for HTX " + nameof(ISpotClient.PlaceOrderAsync), nameof(accountId));

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
                throw new ArgumentException("Invalid order id for HTX " + nameof(ISpotClient.GetOrderAsync), nameof(orderId));

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
                Status = order.Data.Status == OrderStatus.Canceled || order.Data.Status == OrderStatus.PartiallyCanceled ? CommonOrderStatus.Canceled: order.Data.Status == OrderStatus.Filled ? CommonOrderStatus.Filled: CommonOrderStatus.Active
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Invalid order id for HTX " + nameof(ISpotClient.GetOrderAsync), nameof(orderId));

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
                    Status = o.Status == OrderStatus.Canceled || o.Status == OrderStatus.PartiallyCanceled ? CommonOrderStatus.Canceled : o.Status == OrderStatus.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
                }
            ));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException(nameof(symbol) + " required for HTX " + nameof(ISpotClient.GetClosedOrdersAsync), nameof(symbol));

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
                    Status = o.Status == OrderStatus.Canceled || o.Status == OrderStatus.PartiallyCanceled ? CommonOrderStatus.Canceled : o.Status == OrderStatus.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
                }
            ));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Invalid order id for HTX " + nameof(ISpotClient.CancelOrderAsync), nameof(orderId));

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
                throw new ArgumentException(nameof(accountId) + " required for HTX " + nameof(ISpotClient.GetBalancesAsync), nameof(accountId));

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

            throw new ArgumentException("Unsupported timespan for HTX Klines, check supported intervals using HTX.Net.Objects.HTXPeriod");
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
