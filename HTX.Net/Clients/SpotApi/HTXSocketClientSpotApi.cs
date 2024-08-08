using System.Net.WebSockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Options;
using HTX.Net.Objects.Sockets;
using HTX.Net.Objects.Sockets.Queries;
using HTX.Net.Objects.Sockets.Subscriptions;

using HTXOrderUpdate = HTX.Net.Objects.Models.Socket.HTXOrderUpdate;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXSocketClientSpotApi : SocketApiClient, IHTXSocketClientSpotApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");

        #region fields
        #endregion

        #region ctor
        internal HTXSocketClientSpotApi(ILogger logger, HTXSocketOptions options)
            : base(logger, options.Environment.SocketBaseAddress, options, options.SpotOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXSpotPingSubscription(_logger));
            AddSystemSubscription(new HTXPingSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.SpotConnection;
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToLowerInvariant()}{quoteAsset.ToLowerInvariant()}";

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<string>(_idPath);
            if (id != null)
                return id;

            var action = message.GetValue<string>(_actionPath);
            if (string.Equals(action, "ping", StringComparison.Ordinal))
                return "pingV2";

            var ping = message.GetValue<long?>(_pingPath);
            if (ping != null)
                return "pingV3";

            var channel = message.GetValue<string>(_channelPath);
            if (action != null && action != "push")
                return action + channel;

            return channel;
        }

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        protected override Query GetAuthenticationRequest(SocketConnection connection)
        {
            return new HTXAuthQuery(new HTXAuthRequest<HTXAuthParams>
            {
                Action = "req",
                Channel = "auth",
                Params = ((HTXAuthenticationProvider)AuthenticationProvider!).GetWebsocketAuthentication(new Uri(BaseAddress.AppendPath("ws/v2")))
            });
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<IEnumerable<HTXKline>>($"market.{symbol}.kline.{EnumConverter.GetString(period)}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HTXKline>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{symbol}.kline.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ToLowerInvariant();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var query = new HTXQuery<HTXOrderBook>($"market.{symbol}.depth.step{mergeStep}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HTXOrderBook>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var query = new HTXQuery<HTXIncementalOrderBook>($"market.{symbol}.mbp.{levels}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("feed"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HTXIncementalOrderBook>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var subscription = new HTXSubscription<HTXOrderBook>(_logger, $"market.{symbol}.depth.step{mergeStep}", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);

            var subscription = new HTXSubscription<HTXOrderBook>(_logger, $"market.{symbol}.mbp.refresh.{levels}", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HTXIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var subscription = new HTXSubscription<HTXIncementalOrderBook>(_logger, $"market.{symbol}.mbp.{levels}", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("feed"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HTXSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<IEnumerable<HTXSymbolTradeDetails>>($"market.{symbol}.trade.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HTXSymbolTradeDetails>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTrade>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolTrade>(_logger, $"market.{symbol}.trade.detail", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXSymbolDetails>($"market.{symbol}.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXSymbolDetails>(result.Error!);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolDetails>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolDetails>(_logger, $"market.{symbol}.detail", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<IEnumerable<HTXSymbolTicker>>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<IEnumerable<HTXSymbolTicker>>(_logger, $"market.tickers", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTick>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolTick>(_logger, $"market.{symbol}.ticker", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<HTXBestOffer>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXBestOffer>(_logger, $"market.{symbol}.bbo", x => onData(x.WithSymbol(symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HTXSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HTXMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HTXCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HTXTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HTXOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var subscription = new HTXOrderSubscription(_logger, symbol, onOrderSubmitted, onOrderMatched, onOrderCancelation, onConditionalOrderTriggerFailure, onConditionalOrderCanceled);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
        {
            if (updateMode != null && (updateMode > 2 || updateMode < 0))
                throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

            var subscription = new HTXAccountSubscription(_logger, "accounts.update#" + (updateMode ?? 1), onAccountUpdate, true);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<DataEvent<HTXTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var subscription = new HTXOrderDetailsSubscription(_logger, symbol, onOrderMatch, onOrderCancel);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }
    }
}
