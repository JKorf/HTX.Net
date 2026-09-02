using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesSharedApi
    {
        #region Futures Trigger Order Client
        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            },
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.Leverage), typeof(int), "The leverage to use", 3),
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay)
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var marginMode = request.MarginMode ?? ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Trading.PlaceCrossMarginTriggerOrderAsync(
                    request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? TriggerType.GreaterThanOrEqual : TriggerType.LesserThanOrEqual,
                    request.TriggerPrice,
                    request.Quantity.QuantityInContracts ?? 0,
                    side,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    offset: GetOffset(request),
                    reduceOnly: request.OrderDirection == SharedTriggerOrderDirection.Exit ? true: null,
                    orderPrice: request.OrderPrice,
                    orderPriceType: request.OrderPrice == null ? OrderPriceType.Optimal20 : OrderPriceType.Limit,
                    leverageRate: (int)request.Leverage!.Value,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                // Return
                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await _api.Trading.PlaceIsolatedMarginTriggerOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? TriggerType.GreaterThanOrEqual : TriggerType.LesserThanOrEqual,
                    request.TriggerPrice,
                    request.Quantity.QuantityInContracts ?? 0,
                    side,
                    offset: GetOffset(request),
                    reduceOnly: request.OrderDirection == SharedTriggerOrderDirection.Exit ? true : null,
                    orderPrice: request.OrderPrice,
                    orderPriceType: request.OrderPrice == null ? OrderPriceType.Optimal20 : OrderPriceType.Limit,
                    leverageRate: (int)request.Leverage!.Value,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                // Return
                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            }
        }

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await _api.Trading.GetCrossMarginOpenTriggerOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

                var triggerOrder = orders.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (triggerOrder != null)
                {
                    return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, triggerOrder.ContractCode),
                        triggerOrder.ContractCode,
                        triggerOrder.OrderId.ToString(),
                        triggerOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        triggerOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : triggerOrder.Offset == Offset.Close? SharedTriggerOrderDirection.Exit: null,
                        SharedTriggerOrderStatus.Active,
                        triggerPrice: triggerOrder.TriggerPrice,
                        null,
                        triggerOrder.CreateTime)
                    {
                        OrderPrice = triggerOrder.OrderPrice == 0 ? null: triggerOrder.OrderPrice,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: triggerOrder.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: 0),
                    });
                }

                var orderHist = await _api.Trading.GetCrossMarginTriggerOrderHistoryAsync(                    
                    MarginTradeType.All,
                    90,
                    OrderStatusFilter.All,
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orderHist.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderHist);

                var closedOrder = orderHist.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (closedOrder == null)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderHist, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Not found")));

                if (string.IsNullOrEmpty(closedOrder.RelationOrderId) && closedOrder.RelationOrderId != "-1")
                {
                    return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, closedOrder.ContractCode),
                        closedOrder.ContractCode,
                        closedOrder.OrderId.ToString(),
                        closedOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        closedOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : closedOrder.Offset == Offset.Close ? SharedTriggerOrderDirection.Exit : null,
                        SharedTriggerOrderStatus.CanceledOrRejected,
                        triggerPrice: closedOrder.TriggerPrice,
                        null,
                        closedOrder.CreateTime)
                    {
                        OrderPrice = closedOrder.OrderPrice,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: closedOrder.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: 0)
                    });
                }

                var placedOrderResult = await _api.Trading.GetCrossMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: long.Parse(closedOrder.RelationOrderId!), ct: ct).ConfigureAwait(false);
                if (!placedOrderResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(placedOrderResult);

                var placedOrder = placedOrderResult.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, closedOrder.ContractCode),
                        closedOrder.ContractCode,
                        closedOrder.OrderId.ToString(),
                        closedOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        closedOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : closedOrder.Offset == Offset.Close ? SharedTriggerOrderDirection.Exit : null,
                        ParseTriggerOrderStatus(placedOrder.Status),
                        triggerPrice: closedOrder.TriggerPrice,
                        null,
                        closedOrder.CreateTime)
                {
                    PlacedOrderId = closedOrder.RelationOrderId,
                    OrderPrice = closedOrder.OrderPrice,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: closedOrder.Quantity),
                    QuantityFilled = new SharedOrderQuantity(contractQuantity: placedOrder.QuantityFilled),
                    Fee = placedOrder.Fee,
                    FeeAsset = placedOrder.FeeAsset,
                    AveragePrice = placedOrder.AverageFillPrice,
                    UpdateTime = placedOrder.UpdateTime
                });
            }
            else
            {
                var orders = await _api.Trading.GetIsolatedMarginOpenTriggerOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

                var triggerOrder = orders.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (triggerOrder != null)
                {
                    return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, triggerOrder.ContractCode),
                        triggerOrder.ContractCode,
                        triggerOrder.OrderId.ToString(),
                        triggerOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        triggerOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : triggerOrder.Offset == Offset.Close ? SharedTriggerOrderDirection.Exit : null,
                        SharedTriggerOrderStatus.Active,
                        triggerPrice: triggerOrder.TriggerPrice,
                        null,
                        triggerOrder.CreateTime)
                    {
                        OrderPrice = triggerOrder.OrderPrice,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: triggerOrder.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: 0)
                    });
                }

                var orderHist = await _api.Trading.GetIsolatedMarginTriggerOrderHistoryAsync(
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    MarginTradeType.All,
                    90,
                    OrderStatusFilter.All,
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orderHist.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderHist);

                var closedOrder = orderHist.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (closedOrder == null)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderHist, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Not found")));

                if (string.IsNullOrEmpty(closedOrder.RelationOrderId) && closedOrder.RelationOrderId != "-1")
                {
                    return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, closedOrder.ContractCode),
                        closedOrder.ContractCode,
                        closedOrder.OrderId.ToString(),
                        closedOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        closedOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : closedOrder.Offset == Offset.Close ? SharedTriggerOrderDirection.Exit : null,
                        SharedTriggerOrderStatus.CanceledOrRejected,
                        triggerPrice: closedOrder.TriggerPrice,
                        null,
                        closedOrder.CreateTime)
                    {
                        OrderPrice = closedOrder.OrderPrice,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: closedOrder.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: 0)
                    });
                }

                var placedOrderResult = await _api.Trading.GetIsolatedMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: long.Parse(closedOrder.RelationOrderId!), ct: ct).ConfigureAwait(false);
                if (!placedOrderResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(placedOrderResult);

                var placedOrder = placedOrderResult.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, closedOrder.ContractCode),
                        closedOrder.ContractCode,
                        closedOrder.OrderId.ToString(),
                        closedOrder.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                        closedOrder.Offset == Offset.Open ? SharedTriggerOrderDirection.Enter : closedOrder.Offset == Offset.Close ? SharedTriggerOrderDirection.Exit : null,
                        ParseTriggerOrderStatus(placedOrder.Status),
                        triggerPrice: closedOrder.TriggerPrice,
                        null,
                        closedOrder.CreateTime)
                {
                    PlacedOrderId = closedOrder.RelationOrderId,
                    OrderPrice = closedOrder.OrderPrice,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: closedOrder.Quantity),
                    QuantityFilled = new SharedOrderQuantity(contractQuantity: placedOrder.QuantityFilled),
                    Fee = placedOrder.Fee,
                    FeeAsset = placedOrder.FeeAsset,
                    AveragePrice = placedOrder.AverageFillPrice,
                    UpdateTime = placedOrder.UpdateTime
                });
            }
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(SwapMarginOrderStatus status)
        {
            if (status == SwapMarginOrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (status == SwapMarginOrderStatus.Cancelled || status == SwapMarginOrderStatus.PartiallyCancelled)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (status == SwapMarginOrderStatus.PartiallyFilled
                || status == SwapMarginOrderStatus.Cancelling
                || status == SwapMarginOrderStatus.Submitting
                || status == SwapMarginOrderStatus.Submitted)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await _api.Trading.CancelCrossMarginTriggerOrderAsync(
                request.OrderId,
                contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await _api.Trading.CancelIsolatedMarginTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

        private Offset? GetOffset(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionMode == SharedPositionMode.OneWay)
                return null;

            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? Offset.Open : Offset.Close;
        }

        private OrderSide GetOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;

            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }
        #endregion
    }
}
