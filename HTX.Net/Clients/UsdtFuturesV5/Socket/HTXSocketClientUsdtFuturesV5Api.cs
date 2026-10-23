using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.UsdtFuturesV5;
using HTX.Net.Objects.Options;
using HTX.Net.Objects.Sockets.Queries;
using HTX.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    /// <inheritdoc />
    internal partial class HTXSocketClientUsdtFuturesV5Api : SocketApiClient<HTXEnvironment, HTXAuthenticationProvider, HTXCredentials>, IHTXSocketClientUsdtFuturesV5Api
    {
        private readonly HTXSocketClientUsdtFuturesV5SharedApi _sharedApi;

        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;

        #region ctor
        internal HTXSocketClientUsdtFuturesV5Api(ILoggerFactory? loggerFactory, HTXSocketOptions options)
            : base(loggerFactory, HTXExchange.Metadata.Id, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtFuturesV5Options)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXOpPingSubscription(_logger));
            AddSystemSubscription(new HTXCloseSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.UsdtConnection;
            _sharedApi = new HTXSocketClientUsdtFuturesV5SharedApi(this);
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new HTXSocketUsdtFuturesMessageHandler();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IHTXSocketClientUsdtFuturesV5SharedApi SharedApi => _sharedApi;

        /// <inheritdoc />
        public override ReadOnlySpan<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlySpan<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            return Task.FromResult(AuthenticationProvider!.GetAuthenticationQuery(this, connection, new Dictionary<string, object?> { { "version", "2" } }));
        }

        /// <inheritdoc />
        protected override HTXAuthenticationProvider CreateAuthenticationProvider(HTXCredentials credentials)
            => new HTXAuthenticationProvider(credentials, false);

        #region Subscribe To Account Updates

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXDataEventV5<HTXAccountUpdateV5>>> onData, CancellationToken ct = default)
        {
            var subscription = CreateSubscription("account", null, onData);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v5/notification"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe To Order Updates

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXOrderUpdateV5>>> onData, CancellationToken ct = default)
        {
            var subscription = CreateSubscription("orders", contractCode, onData);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v5/notification"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe To Position Updates

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXPositionUpdateV5[]>>> onData, CancellationToken ct = default)
        {
            var subscription = CreateSubscription("positions", contractCode, onData);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v5/notification"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe To User Trade Updates

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXMatchOrderUpdateV5[]>>> onData, CancellationToken ct = default)
        {
            var subscription = CreateSubscription("match_orders", contractCode, onData);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v5/notification"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<QueryResult<HTXOrderIdV5>> PlaceOrderAsync(
            string contractCode,
            MarginMode marginMode,
            OrderSide side, 
            OrderTypeV5 type, 
            decimal quantity, 
            FuturesPositionSide? positionSide = null,
            decimal? price = null, 
            PriceMatch? priceMatch = null, 
            string? clientOrderId = null, 
            bool? reduceOnly = null,
            TimeInForce? timeInForce = null,
            decimal? takeProfitTriggerPrice = null, 
            decimal? takeProfitOrderPrice = null, 
            OrderTypeV5? takeProfitType = null, 
            TriggerPriceType? takeProfitTriggerPriceType = null, 
            decimal? stopLossTriggerPrice = null, 
            decimal? stopLossOrderPrice = null, 
            OrderTypeV5? stopLossType = null,
            TriggerPriceType? stopLossTriggerPriceType = null,
            bool? priceProtect = null,
            SelfMatchPrevent? selfMatchPrevent = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "margin_mode", EnumConverter.GetString(marginMode) },
                { "side", EnumConverter.GetString(side) },
                { "type", EnumConverter.GetString(type) },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "channel_code", "AA1ef14811" }
            };
            parameters.Add("position_side", positionSide);
            parameters.Add("price_match", priceMatch);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("price", price);
            parameters.Add("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? 1 : 0);
            parameters.Add("time_in_force", timeInForce);
            parameters.Add("tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tp_order_price", takeProfitOrderPrice);
            parameters.Add("tp_type", takeProfitType);
            parameters.Add("tp_trigger_price_type", takeProfitTriggerPriceType);
            parameters.Add("sl_trigger_price", stopLossTriggerPrice);
            parameters.Add("sl_order_price", stopLossOrderPrice);
            parameters.Add("sl_type", stopLossType);
            parameters.Add("sl_trigger_price_type", stopLossTriggerPriceType);
            parameters.Add("price_protect", priceProtect);
            parameters.Add("self_match_prevent", selfMatchPrevent);

            var request = new HTXV5Request
            {
                Operation = "place_order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Data = parameters
            };
            var query = new HTXV5Query<HTXOrderIdV5>(this, request, true);
            var result = await QueryAsync(BaseAddress.AppendPath("linear-swap-trade"), query, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<QueryResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = new HTXV5Request
            {
                Operation = "cancel_order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Data = parameters
            };
            var query = new HTXV5Query<HTXOrderIdV5>(this, request, true);
            var result = await QueryAsync(BaseAddress.AppendPath("linear-swap-trade"), query, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<QueryResult<HTXOrderResultV5[]>> CancelAllOrdersAsync(string? contractCode = null, OrderSide? side = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("side", side);
            parameters.Add("position_side", positionSide);
            var request = new HTXV5Request
            {
                Operation = "cancel_all_orders",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Data = parameters
            };
            var query = new HTXV5Query<HTXOrderResultV5[]>(this, request, true);
            var result = await QueryAsync(BaseAddress.AppendPath("linear-swap-trade"), query, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        private HTXOpSubscription<HTXDataEventV5<T>> CreateSubscription<T>(string topic, string? contractCode, Action<DataEvent<HTXDataEventV5<T>>> onData)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEventV5<T>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXDataEventV5<T>>(HTXExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(data.ContractCode)
                        .WithStreamId(data.Topic)
                    );
            });

            return new HTXOpSubscription<HTXDataEventV5<T>>(_logger, this, topic, topic, internalHandler, true, contractCode ?? "*");
        }
    }
}
