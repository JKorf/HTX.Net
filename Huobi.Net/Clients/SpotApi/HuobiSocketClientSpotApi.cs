using System;
using System.Collections.Generic;
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
using CryptoExchange.Net.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.ExtensionMethods;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Options;
using Huobi.Net.Objects.Sockets;
using Huobi.Net.Objects.Sockets.Queries;
using Huobi.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HuobiOrderUpdate = Huobi.Net.Objects.Models.Socket.HuobiOrderUpdate;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiSocketClientSpotApi : SocketApiClient, IHuobiSocketClientSpotApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");

        #region fields
        #endregion

        #region ctor
        internal HuobiSocketClientSpotApi(ILogger logger, HuobiSocketOptions options)
            : base(logger, options.Environment.SocketBaseAddress, options, options.SpotOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HuobiSpotPingSubscription(_logger));
            AddSystemSubscription(new HuobiPingSubscription(_logger));
        }

        #endregion

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

            var ping = message.GetValue<string>(_pingPath);
            if (ping != null)
                return "pingV3";

            var channel = message.GetValue<string>(_channelPath);
            if (action != null && action != "push")
                return action + channel;

            return channel;
        }

        /// <inheritdoc />
        public override ReadOnlyMemory<byte> PreprocessStreamMessage(WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type != WebSocketMessageType.Binary)
                return data;

            return data.DecompressGzip();
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        protected override Query GetAuthenticationRequest()
        {
            return new HuobiAuthQuery(new HuobiAuthRequest<HuobiAuthParams>
            {
                Action = "req",
                Channel = "auth",
                Params = ((HuobiAuthenticationProvider)AuthenticationProvider!).GetWebsocketAuthentication(new Uri(BaseAddress.AppendPath("ws/v2")))
            });
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ValidateHuobiSymbol();

            var query = new HuobiQuery<IEnumerable<HuobiKline>>($"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();

            var subscription = new HuobiSubscription<HuobiKline>(_logger, $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var query = new HuobiQuery<HuobiOrderBook>($"market.{symbol}.depth.step{mergeStep}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HuobiOrderBook>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var query = new HuobiQuery<HuobiIncementalOrderBook>($"market.{symbol}.mbp.{levels}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("feed"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HuobiIncementalOrderBook>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var subscription = new HuobiSubscription<HuobiOrderBook>(_logger, $"market.{symbol}.depth.step{mergeStep}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);

            var subscription = new HuobiSubscription<HuobiOrderBook>(_logger, $"market.{symbol}.mbp.refresh.{levels}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var subscription = new HuobiSubscription<HuobiIncementalOrderBook>(_logger, $"market.{symbol}.mbp.{levels}", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("feed"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();

            var query = new HuobiQuery<IEnumerable<HuobiSymbolTradeDetails>>($"market.{symbol}.trade.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiSymbolTradeDetails>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var subscription = new HuobiSubscription<HuobiSymbolTrade>(_logger, $"market.{symbol}.trade.detail", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();

            var query = new HuobiQuery<HuobiSymbolDetails>($"market.{symbol}.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiSymbolDetails>(result.Error!);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var subscription = new HuobiSubscription<HuobiSymbolDetails>(_logger, $"market.{symbol}.detail", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<IEnumerable<HuobiSymbolTicker>>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<IEnumerable<HuobiSymbolTicker>>(_logger, $"market.tickers", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiSymbolTick>(_logger, $"market.{symbol}.ticker", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default)
        {
            var subscription = new HuobiSubscription<HuobiBestOffer>(_logger, $"market.{symbol}.bbo", onData, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default)
        {
            symbol = symbol?.ValidateHuobiSymbol();

            var subscription = new HuobiOrderSubscription(_logger, symbol, onOrderSubmitted, onOrderMatched, onOrderCancelation, onConditionalOrderTriggerFailure, onConditionalOrderCanceled);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
        {
            if (updateMode != null && (updateMode > 2 || updateMode < 0))
                throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

            var subscription = new HuobiAccountSubscription(_logger, "accounts.update#" + (updateMode ?? 1), onAccountUpdate, true);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default)
        {
            var subscription = new HuobiOrderDetailsSubscription(_logger, symbol, onOrderMatch, onOrderCancel);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }
    }
}
