using System.Net.WebSockets;
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
    internal partial class HTXSocketClientSpotApi : SocketApiClient<HTXEnvironment, HTXAuthenticationProvider, HTXCredentials>, IHTXSocketClientSpotApi
    {
        protected override ErrorMapping ErrorMapping => HTXErrors.SpotMapping;

        /// <inheritdoc />
        public new HTXSocketOptions ClientOptions => (HTXSocketOptions)base.ClientOptions;

        #region ctor
        internal HTXSocketClientSpotApi(ILoggerFactory? loggerFactory, HTXSocketOptions options)
            : base(loggerFactory, HTXExchange.Metadata.Id, options.Environment.SocketBaseAddress, options, options.SpotOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            AddSystemSubscription(new HTXSpotPingSubscription(_logger));
            AddSystemSubscription(new HTXPingSubscription(_logger));

            RateLimiter = HTXExchange.RateLimiter.SpotConnection;

            SetDedicatedConnection(options.Environment.SocketBaseAddress.AppendPath("ws/trade"), true);
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new HTXSocketSpotMessageHandler();

        public IHTXSocketClientSpotApiShared SharedClient => this;

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

        /// <inheritdoc />
        protected override HTXAuthenticationProvider CreateAuthenticationProvider(HTXCredentials credentials)
            => new HTXAuthenticationProvider(credentials, false);

        /// <inheritdoc />
        public async Task<QueryResult<HTXKline[]>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXKline[]>(this, $"market.{symbol}.kline.{EnumConverter.GetString(period)}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXKline[]>(result);

            return QueryResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXKline>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXKline>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXKline>(_logger, this, $"market.{symbol}.kline.{EnumConverter.GetString(period)}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ToLowerInvariant();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var query = new HTXQuery<HTXOrderBook>(this, $"market.{symbol}.depth.step{mergeStep}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXOrderBook>(result);

            if (result.Data.Data == null)
                return QueryResult.Fail<HTXOrderBook>(result, new ServerError(ErrorInfo.Unknown with { Message = "No data in message" }));

            return QueryResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var query = new HTXQuery<HTXIncementalOrderBook>(this, $"market.{symbol}.mbp.{levels}", false);
            var result = await QueryAsync(BaseAddress.AppendPath("feed"), query).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXIncementalOrderBook>(result);

            if (result.Data.Data == null)
                return QueryResult.Fail<HTXIncementalOrderBook>(result, new ServerError(ErrorInfo.Unknown with { Message = "No data in message" }));

            return QueryResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXOrderBook>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXOrderBook>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(data.Data.SequenceNumber)
                    );
            });

            var subscription = new HTXSubscription<HTXOrderBook>(_logger, this, $"market.{symbol}.depth.step{mergeStep}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MillisecondAsync(string symbol, int levels, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);

            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXOrderBook>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXOrderBook>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(data.Data.SequenceNumber)
                    );
            });
            var subscription = new HTXSubscription<HTXOrderBook>(_logger, this, $"market.{symbol}.mbp.refresh.{levels}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HTXIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXIncementalOrderBook>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXIncementalOrderBook>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(data.Data.SequenceNumber)
                    );
            });
            var subscription = new HTXSubscription<HTXIncementalOrderBook>(_logger, this, $"market.{symbol}.mbp.{levels}", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("feed"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXSymbolTradeDetails[]>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXSymbolTradeDetails[]>(this, $"market.{symbol}.trade.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXSymbolTradeDetails[]>(result);

            return QueryResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTrade>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSymbolTrade>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXSymbolTrade>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolTrade>(_logger, this, $"market.{symbol}.trade.detail", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ToLowerInvariant();

            var query = new HTXQuery<HTXSymbolDetails>(this, $"market.{symbol}.detail", false);
            var result = await QueryAsync(BaseAddress.AppendPath("ws"), query).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXSymbolDetails>(result);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return QueryResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolDetails>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSymbolDetails>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXSymbolDetails>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolDetails>(_logger, this, $"market.{symbol}.detail", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HTXSymbolTicker[]>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSymbolTicker[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXSymbolTicker[]>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new HTXSubscription<HTXSymbolTicker[]>(_logger, this, $"market.tickers", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTick>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXSymbolTick>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXSymbolTick>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXSymbolTick>(_logger, this, $"market.{symbol}.ticker", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<HTXBestOffer>> onData, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, HTXDataEvent<HTXBestOffer>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Timestamp);

                onData(
                    new DataEvent<HTXBestOffer>(HTXExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSymbol(symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            symbol = symbol.ToLowerInvariant();
            var subscription = new HTXSubscription<HTXBestOffer>(_logger, this, $"market.{symbol}.bbo", internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HTXSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HTXMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HTXCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HTXTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HTXOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var subscription = new HTXOrderSubscription(_logger, this, symbol, onOrderSubmitted, onOrderMatched, onOrderCancelation, onConditionalOrderTriggerFailure, onConditionalOrderCanceled);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
        {
            if (updateMode != null && (updateMode > 2 || updateMode < 0))
                throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

            var subscription = new HTXAccountSubscription(_logger, this, "accounts.update#" + (updateMode ?? 1), onAccountUpdate, true);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<DataEvent<HTXTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default)
        {
            symbol = symbol?.ToLowerInvariant();

            var subscription = new HTXOrderDetailsSubscription(_logger, this, symbol, onOrderMatch, onOrderCancel);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<QueryResult<string>> PlaceOrderAsync(
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
                ClientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange),
                    64,
                    ClientOptions.AllowAppendingClientOrderId),
                Price = price,
                Type = orderType,
                Quantity = quantity,
                SourceType = source,
                StopOperator = stopOperator,
                StopPrice = stopPrice,
                Symbol = symbol
            };

            var query = new HTXOrderQuery<HTXSocketPlaceOrderRequest, string>(this, new HTXSocketOrderRequest<HTXSocketPlaceOrderRequest>
            {
                Channel = "create-order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = request
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<string>(result);

            return QueryResult.Ok(result, result.Data.Data!);
        }

        /// <inheritdoc />
        public async Task<QueryResult<CallResult<HTXBatchPlaceResult>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default)
        {
            var data = new List<HTXSocketPlaceOrderRequest>();
            foreach (var order in orders)
            {
                var orderType = EnumConverter.GetString(order.Side) + "-" + EnumConverter.GetString(order.Type);
                order.Symbol = order.Symbol.ToLowerInvariant();

                if (!long.TryParse(order.AccountId, out var accountId))
                    return QueryResult.Fail<CallResult<HTXBatchPlaceResult>[]>(Exchange, ArgumentError.Invalid(nameof(HTXOrderRequest.AccountId), "AccountId required on order"));

                var parameters = new HTXSocketPlaceOrderRequest()
                {
                    AccountId = accountId,
                    ClientOrderId = LibraryHelpers.ApplyBrokerId(
                        order.ClientOrderId,
                        LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange),
                        64,
                        ClientOptions.AllowAppendingClientOrderId),
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

            var query = new HTXOrderQuery<List<HTXSocketPlaceOrderRequest>, HTXBatchPlaceResult[]>(this, new HTXSocketOrderRequest<List<HTXSocketPlaceOrderRequest>>
            {
                Channel = "create-batchorder",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = data
            });
            var resultData = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            if (!resultData.Success)
                return QueryResult.Fail<CallResult<HTXBatchPlaceResult>[]>(resultData);

            if (!resultData.Data.Success && resultData.Data.Data?.Any() != true)
                return QueryResult.Fail<CallResult<HTXBatchPlaceResult>[]>(resultData, new ServerError(resultData.Data.ErrorCode!, GetErrorInfo(resultData.Data.ErrorCode!, resultData.Data.ErrorMessage)));

            var result = new List<CallResult<HTXBatchPlaceResult>>();
            foreach (var item in resultData.Data.Data!)
            {
                if (!string.IsNullOrEmpty(item.ErrorCode))
                    result.Add(CallResult.Fail<HTXBatchPlaceResult>(new ServerError(item.ErrorCode!, GetErrorInfo(item.ErrorCode!, item.ErrorMessage))));
                else
                    result.Add(CallResult.Ok(item));
            }

            if (result.All(x => !x.Success))
                return QueryResult.Fail<CallResult<HTXBatchPlaceResult>[]>(resultData, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return QueryResult.Ok(resultData, result.ToArray());
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXOrderId>> PlaceMarginOrderAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "account-id", accountId },
                { "symbol", symbol },
                { "type", orderType }
            };
            parameters.Add("trade-purpose", purpose);
            parameters.Add("source", source);

            parameters.Add("amount", quantity);
            parameters.Add("market-amount", quoteQuantity);
            parameters.Add("borrow-amount", borrowQuantity);
            parameters.Add("price", price);
            parameters.Add("stop-price", stopPrice);
            parameters.Add("operator", stopOperator);

            var query = new HTXOrderQuery<Parameters, HTXOrderId>(this, new HTXSocketOrderRequest<Parameters>
            {
                Channel = "create-margin-order",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXOrderId>(result);

            return QueryResult.Ok(result, result.Data.Data!);
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXByCriteriaCancelResult>> CancelAllOrdersAsync(
            long accountId,
            IEnumerable<string>? symbols = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "account-id", accountId }
            };
            parameters.AddCommaSeparated("symbol", symbols);

            var query = new HTXOrderQuery<Parameters, HTXByCriteriaCancelResult>(this, new HTXSocketOrderRequest<Parameters>
            {
                Channel = "cancelall",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXByCriteriaCancelResult>(result);

            return QueryResult.Ok(result, result.Data.Data!);
        }

        public async Task<QueryResult> CancelOrderAsync(
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            if (clientOrderId != null) {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange),
                    64,
                    ClientOptions.AllowAppendingClientOrderId);
            }

            var result = await CancelOrdersAsync(orderId == null ? null : [orderId], clientOrderId == null ? null : [clientOrderId], ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Successful.Contains(orderId ?? clientOrderId))
                return result;

            return QueryResult.Fail(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Cancel failed")));
        }

        /// <inheritdoc />
        public async Task<QueryResult<HTXBatchCancelResult>> CancelOrdersAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? clientOrderIds = null,
            CancellationToken ct = default)
        {
            
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.AddArray("order-ids", orderIds?.ToArray());
            parameters.AddArray("client-order-ids", clientOrderIds?.Select(x => LibraryHelpers.ApplyBrokerId(
                    x,
                    LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange),
                    64,
                    ClientOptions.AllowAppendingClientOrderId)).ToArray());

            var query = new HTXOrderQuery<Parameters, HTXBatchCancelResult>(this, new HTXSocketOrderRequest<Parameters>
            {
                Channel = "cancel",
                RequestId = ExchangeHelpers.NextId().ToString(),
                Params = parameters
            });
            var result = await QueryAsync(BaseAddress.AppendPath("ws/trade"), query, ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<HTXBatchCancelResult>(result);

            return QueryResult.Ok(result, result.Data.Data!);
        }
    }
}
