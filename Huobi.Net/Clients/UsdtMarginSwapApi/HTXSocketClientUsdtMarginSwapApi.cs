﻿using System.Net.WebSockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Options;
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

        #region ctor
        internal HTXSocketClientUsdtMarginSwapApi(ILogger logger, HTXSocketOptions options)
            : base(logger, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXPingSubscription(_logger));
        }

        #endregion

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}";

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<string>(_idPath);
            if (id != null)
                return id;

            var ping = message.GetValue<string>(_pingPath);
            if (ping != null)
                return "pingV3";

            var channel = message.GetValue<string>(_channelPath);
            var action = message.GetValue<string>(_actionPath);
            if (action != null && action != "push")
                return action + channel;

            return channel;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.kline.{EnumConverter.GetString(period)}", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HTXUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXUsdtMarginSwapIncementalOrderBook>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.step" + mergeStep, x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HTXUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXUsdtMarginSwapIncementalOrderBook>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.size_{limit}.high_freq", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HTXSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXSymbolTickUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.detail", x => onData(x.WithSymbol(contractCode)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HTXBestOfferUpdate>> onData, CancellationToken ct = default)
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

        //// WIP

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(Action<DataEvent<HTXIsolatedMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HTXSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders.*");
        ////    var internalHandler = new Action<DataEvent<HTXIsolatedMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        ////    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HTXIsolatedMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HTXSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders.{contractCode}");
        ////    var internalHandler = new Action<DataEvent<HTXIsolatedMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        ////    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(Action<DataEvent<HTXCrossMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HTXSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders_cross.*");
        ////    var internalHandler = new Action<DataEvent<HTXCrossMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        ////    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HTXCrossMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HTXSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders_cross.{contractCode}");
        ////    var internalHandler = new Action<DataEvent<HTXCrossMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        ////    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

    }
}