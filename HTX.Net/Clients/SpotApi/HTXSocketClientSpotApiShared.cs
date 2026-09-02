using HTX.Net.Interfaces.Clients.SpotApi;
﻿using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXSocketClientSpotApi : IHTXSocketClientSpotApiShared
    {
        private const string _topicId = "HTXSpot";
        private const string _exchangeName = "HTX";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Tickers client
        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToTickerUpdatesAsync(update => handler(update.ToType<SharedSpotTicker[]>(update.Data.Select(x => 
            new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol), 
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
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, symbol),
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

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Details.Select(x => 
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, symbol), 
                        symbol,
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(update.Data.BestAskQuantity), 
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
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
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
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
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 });
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToPartialOrderBookUpdates100MillisecondAsync(symbol, request.Limit ?? 20, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.BaseAsset, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToAccountUpdatesAsync(
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

        SubscribeSpotOrderOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await SubscribeToOrderUpdatesAsync(null,
                update => handler(update.ToType<SharedSpotOrder[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrder[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrder[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrder[]>(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType<SharedSpotOrder[]>(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region User Trade client
        SubscribeUserTradeOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToOrderDetailsUpdatesAsync(
                null,
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol),
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

        SharedFeeDeductionType ISpotOrderManagementSocketClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderManagementSocketClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        SharedOrderType[] ISpotOrderManagementSocketClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderManagementSocketClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderManagementSocketClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.QuoteAsset,
                SharedQuantityType.BaseAsset);

        string ISpotOrderManagementSocketClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderSocketOptions ISpotOrderManagementSocketClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderSocketOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(long), "The id of the account", 123123123L)
            }
        };
        async Task<QueryResult<SharedId>> ISpotOrderManagementSocketClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var accountId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");
            var quantity = request.Quantity?.QuantityInBaseAsset ?? 0;
            if (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy)
                quantity = request.Quantity?.QuantityInQuoteAsset ?? 0;

            var result = await PlaceOrderAsync(
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
        CancelSpotOrderSocketOptions ISpotOrderManagementSocketClient.CancelSpotOrderOptions { get; } = new CancelSpotOrderSocketOptions(_exchangeName, true);
        async Task<QueryResult<SharedId>> ISpotOrderManagementSocketClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await CancelOrderAsync(request.OrderId).ConfigureAwait(false);
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

        public SharedSpotOrder ParseOrder(HTXOrderUpdate orderUpdate)
        {
            if (orderUpdate is HTXSubmittedOrderUpdate update)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Symbol),
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
                    Fee = 0
                };
            }
            if (orderUpdate is HTXMatchedOrderUpdate matchUpdate)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, matchUpdate.Symbol),
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
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, matchUpdate.Symbol),
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
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, cancelUpdate.Symbol),
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
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, triggerFailUpdate.Symbol),
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
