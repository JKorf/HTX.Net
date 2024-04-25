using System;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Options;
using Huobi.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiSocketClientUsdtMarginSwapApi : SocketApiClient, IHuobiSocketClientUsdtMarginSwapApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");

        #region ctor
        internal HuobiSocketClientUsdtMarginSwapApi(ILogger logger, HuobiSocketOptions options)
            : base(logger, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HuobiPingSubscription(_logger));
        }

        #endregion

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}";

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(WebSocketMessageType type, ReadOnlyMemory<byte> data)
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
            => new HuobiAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiUsdtMarginSwapIncementalOrderBook>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.step" + mergeStep, onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiUsdtMarginSwapIncementalOrderBook>(_logger, $"market.{contractCode.ToUpperInvariant()}.depth.size_{limit}.high_freq", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiSymbolTickUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.detail", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiBestOfferUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.bbo", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiUsdtMarginSwapTradesUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.trade.detail", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.premium_index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.estimated_rate.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiUsdtMarginSwapBasisUpdate>(_logger, $"market.{contractCode.ToUpperInvariant()}.basis.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}.{priceType}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{contractCode.ToUpperInvariant()}.mark_price.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), subscription, ct).ConfigureAwait(false);
        }

        //// WIP

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HuobiSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders.*");
        ////    var internalHandler = new Action<DataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        ////    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HuobiSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders.{contractCode}");
        ////    var internalHandler = new Action<DataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        ////    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(Action<DataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HuobiSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders_cross.*");
        ////    var internalHandler = new Action<DataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        ////    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

        /////// <inheritdoc />
        ////public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
        ////{
        ////    var request = new HuobiSocketRequest2(
        ////        "sub",
        ////        NextId().ToString(CultureInfo.InvariantCulture),
        ////        $"orders_cross.{contractCode}");
        ////    var internalHandler = new Action<DataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        ////    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        ////}

    }
}
