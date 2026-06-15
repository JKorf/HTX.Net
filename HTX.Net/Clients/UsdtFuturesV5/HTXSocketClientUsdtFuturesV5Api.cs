using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
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
        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;

        #region ctor
        internal HTXSocketClientUsdtFuturesV5Api(ILoggerFactory? loggerFactory, HTXSocketOptions options)
            : base(loggerFactory, HTXExchange.Metadata.Id, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtFuturesV5Options)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXOpPingSubscription(_logger));
            AddSystemSubscription(new HTXCloseSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.UsdtConnection;
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new HTXSocketUsdtFuturesMessageHandler();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

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
