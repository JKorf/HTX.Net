using HTX.Net.Interfaces.Clients.SpotApi;
﻿using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Clients.SpotApi
{
    internal class HTXSocketClientSpotSharedApi : 
        SharedApiBase,
        IHTXSocketClientSpotApiShared,
        IHTXSocketClientSpotSharedApi
    {
        private readonly HTXSocketClientSpotApi _api;

        private const string _topicId = "HTXSpot";
        private const string _exchangeName = "HTX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public HTXSocketClientSpotSharedApi(HTXSocketClientSpotApi api)
        : base(
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeAllTickersOptions,
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeUserTradeOptions,
                PlaceSpotOrderOptions,
                CancelSpotOrderOptions
                );
        }

        #region Tickers client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersSocket.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToTickerUpdatesAsync(update => handler(update.ToType<SharedSpotTicker[]>(update.Data.Select(x => 
            new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.ClosePrice ?? 0, 
                x.HighPrice ?? 0,
                x.LowPrice ?? 0,
                new SharedOrderQuantity(x.Volume, x.QuoteVolume), 
                (x.OpenPrice == null || x.OpenPrice == 0) ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
            {
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Ticker client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                    symbol,
                    update.Data.LastTradePrice, 
                    update.Data.HighPrice ?? 0,
                    update.Data.LowPrice ?? 0,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.QuoteVolume),
                    (update.Data.OpenPrice == null || update.Data.OpenPrice == 0) ? null : Math.Round((update.Data.ClosePrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))
            {
            })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Details.Select(x => 
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Book Ticker client

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbol, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                        symbol,
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(update.Data.BestAskQuantity), 
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(
                    request.Symbol, 
                    symbol, 
                    update.Data.OpenTime, 
                    update.Data.ClosePrice ?? 0, 
                    update.Data.HighPrice ?? 0, 
                    update.Data.LowPrice ?? 0, 
                    update.Data.OpenPrice ?? 0,
                    new SharedOrderQuantity(update.Data.Volume ?? 0, update.Data.QuoteVolume ?? 0)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 });
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToPartialOrderBookUpdates100MillisecondAsync(symbol, request.Limit ?? 20, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.BaseAsset, update.Data.Version, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToAccountUpdatesAsync(
                update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(
                        SupportedTradingModes,
                        update.Data.Asset, 
                        update.Data.Available ?? 0, 
                        update.Data.Balance ?? update.Data.Available ?? 0) })),
                2, ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Spot Order client

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await _api.SubscribeToOrderUpdatesAsync(null,
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region User Trade client
        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderDetailsUpdatesAsync(
                null,
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(update.Data.Quantity),
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        Role = update.Data.IsTaker ? SharedRole.Taker : SharedRole.Maker,
                        Fee = update.Data.TransactionFee,
                        FeeAsset = update.Data.FeeAsset
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Spot Order Management client

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.QuoteAsset,
                SharedQuantityType.BaseAsset);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceSpotOrderSocketOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderSocketOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "The id of the account", 123123123L)
            }
        };
        public async Task<QueryResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var quantity = request.Quantity?.QuantityInBaseAsset ?? 0;
            if (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy)
                quantity = request.Quantity?.QuantityInQuoteAsset ?? 0;

            var result = await _api.PlaceOrderAsync(
                accountId,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity,
                request.Price,
                request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.ToString()));
        }
        public CancelSpotOrderSocketOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderSocketOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.CancelOrderAsync(request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Limit && (tif == null || tif == SharedTimeInForce.GoodTillCanceled)) return OrderType.Limit;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.IOC;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKillLimit;
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (type == SharedOrderType.Market) return OrderType.Market;

            throw new ArgumentException($"The combination of order type `{type}` and time in force `{tif}` in invalid");
        }

        #endregion

        public SharedSpotOrderUpdate ParseOrder(HTXOrderUpdate orderUpdate)
        {
            if (orderUpdate is HTXSubmittedOrderUpdate update)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol,
                            update.OrderId.ToString(),
                            ParseOrderType(update.Type),
                            update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            SharedOrderStatus.Open,
                            update.CreateTime)
                {
                    ClientOrderId = update.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(update.Quantity, update.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(0, 0),
                    UpdateTime = update.UpdateTime,
                    OrderPrice = update.Price,
                    IsTriggerOrder = update.Type == OrderType.StopLimit,
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = 0
#pragma warning restore CS0618 // Type or member is obsolete
                };
            }
            if (orderUpdate is HTXMatchedOrderUpdate matchUpdate)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, matchUpdate.Symbol),
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            ParseOrderType(matchUpdate.Type),
                            matchUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            matchUpdate.QuantityRemaining == 0 ? SharedOrderStatus.Filled : SharedOrderStatus.Open,
                            null)
                {
                    ClientOrderId = matchUpdate.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.Quantity, matchUpdate.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? null : matchUpdate.QuantityFilled, matchUpdate.Type == Enums.OrderType.Market && matchUpdate.Side == Enums.OrderSide.Buy ? matchUpdate.QuantityFilled : null),
                    UpdateTime = matchUpdate.UpdateTime,
                    OrderPrice = matchUpdate.Price,
                    IsTriggerOrder = matchUpdate.Type == OrderType.StopLimit,
                    LastTrade = 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, matchUpdate.Symbol),
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.TradeId.ToString(),
                            matchUpdate.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(matchUpdate.TradeQuantity),
                            matchUpdate.TradePrice,
                            matchUpdate.TradeTime)
                        {
                            ClientOrderId = matchUpdate.ClientOrderId,
                            Role = matchUpdate.IsTaker ? SharedRole.Taker : SharedRole.Maker
                        }
                };
            }

            if (orderUpdate is HTXCanceledOrderUpdate cancelUpdate)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, cancelUpdate.Symbol),
                            cancelUpdate.Symbol,
                            cancelUpdate.OrderId.ToString(),
                            ParseOrderType(cancelUpdate.Type),
                            cancelUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            SharedOrderStatus.Canceled,
                            null)
                {
                    ClientOrderId = cancelUpdate.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.Quantity, cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.Quantity : null),
                    QuantityFilled = new SharedOrderQuantity(cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? null : cancelUpdate.QuantityFilled, cancelUpdate.Type == Enums.OrderType.Market && cancelUpdate.Side == Enums.OrderSide.Buy ? cancelUpdate.QuantityFilled : null),
                    UpdateTime = cancelUpdate.UpdateTime,
                    OrderPrice = cancelUpdate.Price,
                    IsTriggerOrder = cancelUpdate.Type == OrderType.StopLimit
                };
            }

            if (orderUpdate is HTXTriggerFailureOrderUpdate triggerFailUpdate)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, triggerFailUpdate.Symbol),
                            triggerFailUpdate.Symbol,
                            "", // Order id is not specified when trigger fails?
                            SharedOrderType.Limit,
                            triggerFailUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            SharedOrderStatus.Canceled,
                            null)
                {
                    ClientOrderId = triggerFailUpdate.ClientOrderId,
                    OrderQuantity = new SharedOrderQuantity(triggerFailUpdate.TotalTradeQuantity),
                    QuantityFilled = new SharedOrderQuantity(0),
                    UpdateTime = triggerFailUpdate.UpdateTime,
                    IsTriggerOrder = true
                };
            }

            throw new Exception("Unknown order update type");
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market || type == OrderType.MarketGrid || type == OrderType.IOC)
                return SharedOrderType.Market;

            if (type == OrderType.Limit || type == OrderType.LimitMaker || type == OrderType.LimitGrid || type == OrderType.StopLimit || type == OrderType.FillOrKillLimit || type == OrderType.FillOrKillStopLimit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
    }
}
