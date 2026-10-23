using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXSocketClientUsdtFuturesV5SharedApi
    {

        #region Place Futures Order

        async Task<IExchangeCallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public string GenerateClientOrderId() => ExchangeHelpers.RandomLong(10).ToString();

        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, true)
        {
            RequestNotes = "ClientOrderId can only be an integer",
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<QueryResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            // Margin mode priority: request parameter > exchange parameter > default (cross)
            var marginMode =
                request.MarginMode != null
                    ? (request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated)
                    : GetProvidedMarginMode(request) ?? MarginMode.Cross;
            var result = await _api.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                marginMode,
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Market ? OrderTypeV5.Market : request.OrderType == SharedOrderType.LimitMaker ? OrderTypeV5.PostOnly : OrderTypeV5.Limit,
                request.Quantity?.QuantityInContracts ?? 0,
                price: request.Price,
                timeInForce: GetTimeInForce(request.TimeInForce),
                reduceOnly: request.ReduceOnly,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? FuturesPositionSide.Long : FuturesPositionSide.Short,
                stopLossTriggerPrice: request.StopLossPrice,
                takeProfitTriggerPrice: request.TakeProfitPrice,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        private TimeInForce? GetTimeInForce(SharedTimeInForce? timeInForce)
        {
            if (timeInForce == null)
                return null;

            return timeInForce switch
            {
                SharedTimeInForce.GoodTillCanceled => TimeInForce.GoodTillCancel,
                SharedTimeInForce.ImmediateOrCancel => TimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill => TimeInForce.FillOrKill,
                _ => throw new ArgumentOutOfRangeException(nameof(timeInForce), $"Unsupported time in force: {timeInForce}"),
            };
        }

        #endregion

        #region Cancel Futures Order

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.CancelOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel Futures Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.CancelOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel All Futures Symbol Orders

        async Task<IExchangeCallResult> ICancelAllFuturesSymbolOrders.CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesSymbolOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesSymbolOrdersOptions CancelAllFuturesSymbolOrdersOptions { get; } = new CancelAllFuturesSymbolOrdersOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesSymbolOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.CancelAllOrdersAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok<SharedId>(result, new(string.Empty));
        }

        #endregion

        #region Cancel All Futures Orders

        async Task<IExchangeCallResult> ICancelAllFuturesOrders.CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesOrdersOptions CancelAllFuturesOrdersOptions { get; } = new CancelAllFuturesOrdersOptions(_exchangeName);
        public async Task<QueryResult> CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail(Exchange, validationError);

            var result = await _api.CancelAllOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return QueryResult.Fail(result);

            return QueryResult.Ok<object>(result, new());
        }

        #endregion

        #region Subscribe Futures Orders

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                null,
                update => {
                    handler(update.ToType<SharedFuturesOrderUpdate[]>(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.ContractCode),
                        update.Data.ContractCode!,
                        update.Data.Data.OrderId,
                        ParseOrderType(update.Data.Data),
                        update.Data.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Data),
                        update.Data.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.Data.ClientOrderId?.ToString(),
                        AveragePrice = update.Data.Data.AverageTradePrice,
                        OrderPrice = update.Data.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: update.Data.Data.TradeTurnover, contractQuantity: update.Data.Data.TradeQuantity),
                        TimeInForce = ParseTimeInForce(update.Data.Data),
                        UpdateTime = update.Data.Data.UpdateTime,
                        PositionSide = ParsePositionSide(update.Data.Data),
                        ReduceOnly = update.Data.Data.ReduceOnly,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = Math.Abs(update.Data.Data.Fee),
                        FeeAsset = update.Data.Data.FeeCurrency,
#pragma warning restore CS0618 // Type or member is obsolete
                    } }));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedPositionSide? ParsePositionSide(HTXOrderUpdateV5 data)
        {
            return data.PositionSide switch
            {
                Enums.FuturesPositionSide.Long => SharedPositionSide.Long,
                Enums.FuturesPositionSide.Short => SharedPositionSide.Short,
                _ => null
            };
        }
        private SharedTimeInForce? ParseTimeInForce(HTXOrderUpdateV5 data)
        {
            return data.TimeInForce switch
            {
                Enums.TimeInForce.GoodTillCancel => SharedTimeInForce.GoodTillCanceled,
                Enums.TimeInForce.ImmediateOrCancel => SharedTimeInForce.ImmediateOrCancel,
                Enums.TimeInForce.FillOrKill => SharedTimeInForce.FillOrKill,
                _ => null
            };
        }
        private SharedOrderStatus ParseOrderStatus(HTXOrderUpdateV5 data)
        {
            return data.Status switch
            {
                Enums.OrderStatusV5.New => SharedOrderStatus.Open,
                Enums.OrderStatusV5.PartiallyFilled => SharedOrderStatus.Open,
                Enums.OrderStatusV5.Filled => SharedOrderStatus.Filled,
                Enums.OrderStatusV5.PartiallyCanceled => SharedOrderStatus.Canceled,
                Enums.OrderStatusV5.Canceled => SharedOrderStatus.Canceled,
                Enums.OrderStatusV5.Rejected => SharedOrderStatus.Canceled,
                _ => SharedOrderStatus.Unknown
            };
        }
        private SharedOrderType ParseOrderType(HTXOrderUpdateV5 data)
        {
            return data.Type switch
            {
                Enums.OrderTypeV5.Market => SharedOrderType.Market,
                Enums.OrderTypeV5.PostOnly => SharedOrderType.Limit,
                _ => SharedOrderType.Limit
            };
        }

        #endregion

    }
}
