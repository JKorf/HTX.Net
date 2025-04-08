using System.Diagnostics;
using System.Net.WebSockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Internal;
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
    internal partial class HTXSocketClientSpotApi : SocketApiClient, IHTXSocketClientSpotApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _idPath2 = MessagePath.Get().Property("cid");
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("ch");
        private static readonly MessagePath _pingPath = MessagePath.Get().Property("ping");

        /// <inheritdoc />
        public new HTXSocketOptions ClientOptions => (HTXSocketOptions)base.ClientOptions;

        #region ctor
        internal HTXSocketClientSpotApi(ILogger logger, HTXSocketOptions options)
            : base(logger, options.Environment.SocketBaseAddress, options, options.SpotOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXSpotPingSubscription(_logger));
            AddSystemSubscription(new HTXPingSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.SpotConnection;

            SetDedicatedConnection(options.Environment.SocketBaseAddress.AppendPath("ws/trade"), true);
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange.SerializerContext));

        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(HTXExchange.SerializerContext));

        public IHTXSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<string>(_idPath) ?? message.GetValue<string>(_idPath2);
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
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var path = connection.ConnectionUri;

            return Task.FromResult<Query?>(new HTXAuthQuery(new HTXAuthRequest<HTXAuthParams>
            {
                Action = "req",
                Channel = "auth",
                Params = ((HTXAuthenticationProvider)AuthenticationProvider!).GetWebsocketAuthentication(path)
            }));
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXKline[]>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXKline[]>($"market.{symbol}.kline.{EnumConverter.GetString(period)}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HTXKline[]>(result.Error!);
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MillisecondAsync(string symbol, int levels, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default)
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
        public async Task<CallResult<HTXSymbolTradeDetails[]>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXSymbolTradeDetails[]>($"market.{symbol}.trade.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<HTXSymbolTradeDetails[]>(result.Error!);
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HTXSymbolTicker[]>> onData, CancellationToken ct = default)
        {
            var subscription = new HTXSubscription<HTXSymbolTicker[]>(_logger, $"market.tickers", onData, false);
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

        /// <inheritdoc />
        public async Task<CallResult<string>> PlaceOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            decimal quantity,
            decimal? price = null,
            string? clientOrderId = null,
            SourceType? source = null,
            decimal? stopPrice = null,
            Operator? stopOperator = null,
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var orderType = EnumConverter.GetString(side) + "-" + EnumConverter.GetString(type);

            var request = new HTXSocketPlaceOrderRequest()
            {
                AccountId = accountId,
                ClientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, HTXExchange.ClientOrderId, 64, ClientOptions.AllowAppendingClientOrderId),
                Price = price,
                Type = orderType,
                Quantity = quantity,
                SourceType = source,
                StopOperator = stopOperator,
                StopPrice = stopPrice,
                Symbol = symbol
            };

            var query = new HTXOrderQuery<HTXSocketPlaceOrderRequest, string>(new HTXSocketOrderRequest<HTXSocketPlaceOrderRequest>
            {
                Channel = "create-order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = request
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            return result.As<string>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXBatchPlaceResult[]>> PlaceMultipleOrdersAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default)
        {
            var data = new List<HTXSocketPlaceOrderRequest>();
            foreach (var order in orders)
            {
                var orderType = EnumConverter.GetString(order.Side) + "-" + EnumConverter.GetString(order.Type);
                order.Symbol = order.Symbol.ToLowerInvariant();

                var parameters = new HTXSocketPlaceOrderRequest()
                {
                    AccountId = long.Parse(order.AccountId),
                    ClientOrderId = LibraryHelpers.ApplyBrokerId(order.ClientOrderId, HTXExchange.ClientOrderId, 64, ClientOptions.AllowAppendingClientOrderId),
                    Price = order.Price,
                    Type = orderType,
                    Quantity = order.Quantity,
                    SourceType = order.Source,
                    StopOperator = order.StopOperator,
                    StopPrice = order.StopPrice,
                    Symbol = order.Symbol
                };

                data.Add(parameters);
            }

            var query = new HTXOrderQuery<List<HTXSocketPlaceOrderRequest>, HTXBatchPlaceResult[]>(new HTXSocketOrderRequest<List<HTXSocketPlaceOrderRequest>>
            {
                Channel = "create-batchorder",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = data
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            return result.As<HTXBatchPlaceResult[]>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXOrderId>> PlaceMarginOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            Enums.MarginPurpose purpose,
            SourceType source,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            decimal? borrowQuantity = null,
            decimal? price = null,
            decimal? stopPrice = null,
            Operator? stopOperator = null,
            CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var orderType = EnumConverter.GetString(side) + "-" + EnumConverter.GetString(type);

            var parameters = new ParameterCollection()
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.AddEnum("trade-purpose", purpose);
            parameters.AddEnum("source", source);

            parameters.AddOptionalString("amount", quantity);
            parameters.AddOptionalString("market-amount", quoteQuantity);
            parameters.AddOptionalString("borrow-amount", borrowQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalString("stop-price", stopPrice);
            parameters.AddOptionalEnum("operator", stopOperator);

            var query = new HTXOrderQuery<ParameterCollection, HTXOrderId>(new HTXSocketOrderRequest<ParameterCollection>
            {
                Channel = "create-margin-order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            return result.As<HTXOrderId>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXByCriteriaCancelResult>> CancelAllOrdersAsync(
            long accountId,
            IEnumerable<string>? symbols = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "account-id", accountId }
            };
            parameters.AddOptional("symbol", symbols == null ? null : string.Join(",", symbols));

            var query = new HTXOrderQuery<ParameterCollection, HTXByCriteriaCancelResult>(new HTXSocketOrderRequest<ParameterCollection>
            {
                Channel = "cancelall",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            return result.As<HTXByCriteriaCancelResult>(result.Data?.Data);
        }

        public async Task<CallResult> CancelOrdersAsync(
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            if (clientOrderId != null)
                clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, HTXExchange.ClientOrderId, 64, ClientOptions.AllowAppendingClientOrderId);

            var result = await CancelOrdersAsync(orderId == null ? null : [orderId], clientOrderId == null ? null : [clientOrderId], ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Successful.Contains(orderId ?? clientOrderId))
                return result.AsDataless();

            return result.AsDatalessError(new ServerError("Cancel failed"));
        }

        /// <inheritdoc />
        public async Task<CallResult<HTXBatchCancelResult>> CancelOrdersAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? clientOrderIds = null,
            CancellationToken ct = default)
        {
            
            var parameters = new ParameterCollection();
            parameters.AddOptional("order-ids", orderIds?.ToArray());
            parameters.AddOptional("client-order-ids", clientOrderIds?.Select(x => LibraryHelpers.ApplyBrokerId(x, HTXExchange.ClientOrderId, 64, ClientOptions.AllowAppendingClientOrderId)).ToArray());

            var query = new HTXOrderQuery<ParameterCollection, HTXBatchCancelResult>(new HTXSocketOrderRequest<ParameterCollection>
            {
                Channel = "cancel",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            return result.As<HTXBatchCancelResult>(result.Data?.Data);
        }
    }
}
