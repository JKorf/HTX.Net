using System.Net.WebSockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Options;
using HTX.Net.Objects.Sockets.Queries;
using HTX.Net.Objects.Sockets.Subscriptions;


namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXSocketClientUsdtMarginSwapApi : SocketApiClient, IHTXSocketClientUsdtMarginSwapApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");


        private static readonly MessagePath _cidPath = MessagePath.Get().Property("cid");
        private static readonly MessagePath _opPath = MessagePath.Get().Property("op");
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");

        #region ctor
        internal HTXSocketClientUsdtMarginSwapApi(ILogger logger, HTXSocketOptions options)
            : base(logger, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXPingSubscription(_logger));
            AddSystemSubscription(new HTXOpPingSubscription(_logger));
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}";

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        protected override Query? GetAuthenticationRequest(SocketConnection connection)
        {
            var request = ((HTXAuthenticationProvider)AuthenticationProvider!).GetWebsocketAuthentication2(connection.ConnectionUri);
            return new HTXOpAuthQuery(request);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<string>(_idPath) ?? message.GetValue<string>(_cidPath);
            if (id != null)
                return id;

            var ping = message.GetValue<long?>(_pingPath);
            if (ping != null)
                return "pingV3";

            var opPing = message.GetValue<string>(_opPath);
            if (string.Equals(opPing, "ping"))
                return "ping";

            if (string.Equals(opPing, "auth"))
                return "auth";

            var channel = message.GetValue<string>(_channelPath);
            var action = message.GetValue<string>(_actionPath);
            if (action != null && action != "push")
                return action + channel;

            var topic = message.GetValue<string>(_topicPath);
            if (topic != null) 
            {
                if (topic.EndsWith(".liquidation_orders"))
                    topic = "public.*.liquidation_orders";
                if (topic.EndsWith(".funding_rate"))
                    topic = "public.*.funding_rate";
                if (topic.StartsWith("accounts."))
                    topic = "accounts";
                if (topic.StartsWith("orders."))
                    topic = "orders";
                if (topic.StartsWith("accounts_cross."))
                    topic = "accounts_cross";
                if (topic.StartsWith("orders_cross."))
                    topic = "orders_cross";

                return topic;
            }

            return channel;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXSwapKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXSwapKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.kline.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HTXOrderBookUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXOrderBookUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.step" + mergeStep, x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HTXIncrementalOrderBookUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXIncrementalOrderBookUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.size_{limit}.high_freq", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXSymbolTickUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.detail", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXBestOfferUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXBestOfferUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.bbo", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HTXUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXUsdtMarginSwapTradesUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.trade.detail", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.index.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.premium_index.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.estimated_rate.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HTXUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXUsdtMarginSwapBasisUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.basis.{EnumConverter.GetString(period)}.{priceType}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.mark_price.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapLiquidationUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapLiquidationUpdate>(_logger, "public.*.liquidation_orders", "public.*.liquidation_orders", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapFundingRateUpdate>>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapFundingRateUpdateWrapper>(_logger, "public.*.funding_rate", "public.*.funding_rate", x => onData(x.As(x.Data.Data)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContractUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapContractUpdate>>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapContractUpdateWrapper>(_logger, "public.*.contract_info", "public.*.contract_info", x => onData(x.As(x.Data.Data)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContractElementsUpdatesAsync(Action<DataEvent<IEnumerable<HTXUsdtMarginSwapContractElementsUpdate>>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapContractElementsUpdateWrapper>(_logger, "public.*.contract_elements", "public.*.contract_elements", x => onData(x.As(x.Data.Data)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<HTXStatusUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXStatusUpdate>(_logger, "public.linear-swap.heartbeat", "public.linear-swap.heartbeat", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("center-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Enums.MarginMode mode, Action<DataEvent<HTXUsdtMarginSwapOrderUpdate>> onData, CancellationToken ct = default)
        {
            if (mode == MarginMode.All)
                throw new ArgumentException("Mode should be either Cross or Isolated", nameof(mode));

            var topic = mode == MarginMode.Cross ? "orders_cross" : "orders"; 
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapOrderUpdate>(_logger, topic, topic + ".*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedBalanceUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedBalanceUpdate>(_logger, "accounts", "accounts.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossBalanceUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossBalanceUpdate>(_logger, "accounts_cross", "accounts_cross.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedPositionUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedPositionUpdate>(_logger, "positions", "positions.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossPositionUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossPositionUpdate>(_logger, "positions_cross", "positions_cross.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTradeUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedTradeUpdate>(_logger, "matchOrders", "matchOrders.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTradeUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossTradeUpdate>(_logger, "matchOrders_cross", "matchOrders_cross.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>(_logger, "trigger_order", "trigger_order.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTriggerOrderUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossTriggerOrderUpdate>(_logger, "trigger_order_cross.*", "trigger_order_cross.*", onData, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }
    }
}
