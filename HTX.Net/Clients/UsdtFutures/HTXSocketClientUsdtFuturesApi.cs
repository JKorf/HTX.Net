using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Options;
using HTX.Net.Objects.Sockets.Queries;
using HTX.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;


namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal partial class HTXSocketClientUsdtFuturesApi : SocketApiClient, IHTXSocketClientUsdtFuturesApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");

        private static readonly MessagePath _cidPath = MessagePath.Get().Property("cid");
        private static readonly MessagePath _opPath = MessagePath.Get().Property("op");
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");

        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;

        #region ctor
        internal HTXSocketClientUsdtFuturesApi(ILogger logger, HTXSocketOptions options)
            : base(logger, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXPingSubscription(_logger));
            AddSystemSubscription(new HTXOpPingSubscription(_logger));
            AddSystemSubscription(new HTXCloseSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.UsdtConnection;
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(HTXExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new HTXSocketUsdtFuturesMessageHandler();

        public IHTXSocketClientUsdtFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        /// <inheritdoc />
        public override ReadOnlySpan<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlySpan<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var request = ((HTXAuthenticationProvider)AuthenticationProvider!).GetWebsocketAuthentication2(connection.ConnectionUri);
            return Task.FromResult<Query?>(new HTXOpAuthQuery(this, request));
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

            var op = message.GetValue<string>(_opPath);
            if (string.Equals(op, "ping")
                || string.Equals(op, "close")
                || string.Equals(op, "auth"))
            {
                return op;
            }

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
                if (topic.StartsWith("positions."))
                    topic = "positions";
                if (topic.StartsWith("accounts_cross."))
                    topic = "accounts_cross";
                if (topic.StartsWith("orders_cross."))
                    topic = "orders_cross";
                if (topic.StartsWith("positions_cross."))
                    topic = "positions_cross";

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
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSwapKline>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXSwapKline>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXSwapKline>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.kline.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HTXOrderBookUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXOrderBookUpdate>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXOrderBookUpdate>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.depth.step" + mergeStep, internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HTXIncrementalOrderBookUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXIncrementalOrderBookSubscription(_logger, this, snapshot, $"market.{contractCode.ToUpperInvariant()}.depth.size_{limit}.high_freq", x => onData(x.WithSymbol(contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSymbolTickUpdate>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXSymbolTickUpdate>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXSymbolTickUpdate>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.detail", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXBestOfferUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXBestOfferUpdate>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXBestOfferUpdate>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXBestOfferUpdate>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.bbo", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HTXUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXUsdtMarginSwapTradesUpdate>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapTradesUpdate>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXUsdtMarginSwapTradesUpdate>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.trade.detail", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXKline>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXKline>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXKline>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.index.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXKline>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXKline>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXKline>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.premium_index.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXKline>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXKline>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXKline>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.estimated_rate.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HTXUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXUsdtMarginSwapBasisUpdate>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapBasisUpdate>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXUsdtMarginSwapBasisUpdate>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.basis.{EnumConverter.GetString(period)}.{priceType}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXKline>>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXKline>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithSymbol(contractCode)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new HTXSubscription<HTXKline>(_logger, this, $"market.{contractCode.ToUpperInvariant()}.mark_price.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapLiquidationUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapLiquidationUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapLiquidationUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapLiquidationUpdate>(_logger, this, "public.*.liquidation_orders", "public.*.liquidation_orders", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapFundingRateUpdate[]>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapFundingRateUpdateWrapper>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapFundingRateUpdate[]>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapFundingRateUpdateWrapper>(_logger, this, "public.*.funding_rate", "public.*.funding_rate", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContractUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapContractUpdate[]>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapContractUpdateWrapper>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapContractUpdate[]>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapContractUpdateWrapper>(_logger, this, "public.*.contract_info", "public.*.contract_info", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContractElementsUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapContractElementsUpdate[]>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapContractElementsUpdateWrapper>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapContractElementsUpdate[]>(data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapContractElementsUpdateWrapper>(_logger, this, "public.*.contract_elements", "public.*.contract_elements", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<HTXStatusUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXStatusUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXStatusUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });
            var subscription = new HTXOpSubscription<HTXStatusUpdate>(_logger, this, "public.linear-swap.heartbeat", "public.linear-swap.heartbeat", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("center-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(MarginMode mode, Action<DataEvent<HTXUsdtMarginSwapOrderUpdate>> onData, CancellationToken ct = default)
        {
            if (mode == MarginMode.All)
                throw new ArgumentException("Mode should be either Cross or Isolated", nameof(mode));

            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapOrderUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapOrderUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var topic = mode == MarginMode.Cross ? "orders_cross" : "orders";
            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapOrderUpdate>(_logger, this, topic, topic + ".*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedBalanceUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapIsolatedBalanceUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapIsolatedBalanceUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedBalanceUpdate>(_logger, this, "accounts", "accounts.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossBalanceUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapCrossBalanceUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapCrossBalanceUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossBalanceUpdate>(_logger, this, "accounts_cross", "accounts_cross.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedPositionUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapIsolatedPositionUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapIsolatedPositionUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedPositionUpdate>(_logger, this, "positions", "positions.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginPositionUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossPositionUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapCrossPositionUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapCrossPositionUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossPositionUpdate>(_logger, this, "positions_cross", "positions_cross.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTradeUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapIsolatedTradeUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapIsolatedTradeUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedTradeUpdate>(_logger, this, "matchOrders", "matchOrders.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginUserTradeUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTradeUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapCrossTradeUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapCrossTradeUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossTradeUpdate>(_logger, this, "matchOrders_cross", "matchOrders_cross.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>(_logger, this, "trigger_order", "trigger_order.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginTriggerOrderUpdatesAsync(Action<DataEvent<HTXUsdtMarginSwapCrossTriggerOrderUpdate>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXUsdtMarginSwapCrossTriggerOrderUpdate>((receiveTime, originalData, data) =>
            {
                onData(
                    new DataEvent<HTXUsdtMarginSwapCrossTriggerOrderUpdate>(data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp)
                        .WithStreamId(data.Topic)
                    );
            });

            var subscription = new HTXOpSubscription<HTXUsdtMarginSwapCrossTriggerOrderUpdate>(_logger, this, "trigger_order_cross.*", "trigger_order_cross.*", internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-notification"), subscription, ct).ConfigureAwait(false);
        }
    }
}
