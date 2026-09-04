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

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomLong(10).ToString();
        #region Place Futures Order

        async Task<ICallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, true)
        {
            RequestNotes = "ClientOrderId can only be an integer",
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            },
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.Leverage), typeof(int), "The leverage to use", 3)
            }
        };

        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var marginMode = request.MarginMode ?? ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Trading.PlaceCrossMarginOrderAsync(
                    contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                    quantity: (long)(request.Quantity?.QuantityInContracts ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId == null ? null : long.Parse(request.ClientOrderId),
                    takeProfitTriggerPrice: request.TakeProfitPrice,
                    takeProfitOrderPriceType: request.TakeProfitPrice == null ? null : OrderPriceType.Market,
                    stopLossTriggerPrice: request.StopLossPrice,
                    stopLossOrderPriceType: request.StopLossPrice == null ? null : OrderPriceType.Market,
                    ct: ct).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await _api.Trading.PlaceIsolatedMarginOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    quantity: (long)(request.Quantity?.QuantityInContracts ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId == null ? null :long.Parse(request.ClientOrderId),
                    takeProfitTriggerPrice: request.TakeProfitPrice,
                    takeProfitOrderPriceType: request.TakeProfitPrice == null ? null : OrderPriceType.Market,
                    stopLossTriggerPrice: request.StopLossPrice,
                    stopLossOrderPriceType: request.StopLossPrice == null ? null : OrderPriceType.Market,
                    ct: ct).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            }
        }

        #endregion
        #region Get Futures Order

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await _api.Trading.GetCrossMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder>(orders);

                var order = orders.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.ContractCode),
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    OrderPrice = order.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.ValueFilled, contractQuantity: order.QuantityFilled),
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
            else
            {
                var orders = await _api.Trading.GetIsolatedMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder>(orders);

                var order = orders.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.ContractCode),
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    OrderPrice = order.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.ValueFilled, contractQuantity: order.QuantityFilled),
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
        }

        #endregion
        #region Get Open Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var orders = await _api.Trading.GetCrossMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder[]>(orders);

                return HttpResult.Ok(orders, orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                    x.ContractCode,
                    x.OrderId.ToString(),
                    ParseOrderType(x.OrderPriceType),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.Status),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId.ToString(),
                    AveragePrice = x.AverageFillPrice,
                    OrderPrice = x.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ValueFilled, contractQuantity: x.QuantityFilled),
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly,
                    Leverage = x.LeverageRate
                }).ToArray());
            }
            else
            {
                if (request.Symbol == null)
                    return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, ArgumentError.Missing(nameof(GetOpenOrdersRequest.Symbol), "Symbol parameter required for isolated margin request"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var orders = await _api.Trading.GetIsolatedMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder[]>(orders);

                return HttpResult.Ok(orders, orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                    x.ContractCode,
                    x.OrderId.ToString(),
                    ParseOrderType(x.OrderPriceType),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.Status),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId.ToString(),
                    AveragePrice = x.AverageFillPrice,
                    OrderPrice = x.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ValueFilled, contractQuantity: x.QuantityFilled),
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly,
                    Leverage = x.LeverageRate
                }).ToArray());
            }
        }

        #endregion
        #region Get Closed Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000)
        {
            MaxAge = TimeSpan.FromDays(88),
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(2));

            // Get data
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                var result = await _api.Trading.GetCrossMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All,
                    allOrders: false,
                    new[] { OrderStatusFilter.All },
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    direction: FilterDirection.Next,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedFuturesOrder[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(88));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                        .Select(x => 
                            new SharedFuturesOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                                x.ContractCode,
                                x.OrderId.ToString(),
                                ParseOrderType(x.OrderPriceType),
                                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                ParseOrderStatus(x.Status),
                                x.CreateTime)
                            {
                                ClientOrderId = x.ClientOrderId.ToString(),
                                AveragePrice = x.AverageFillPrice,
                                OrderPrice = x.Price,
                                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ValueFilled, contractQuantity: x.QuantityFilled),
                                TimeInForce = ParseTimeInForce(x.OrderPriceType),
                                UpdateTime = x.UpdateTime,
                                PositionSide = ParsePositionSide(x.Offset, x.Side),
                                ReduceOnly = x.ReduceOnly,
                                Leverage = x.LeverageRate
                            })
                        .ToArray(), nextPageRequest);
            }
            else
            {
                var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                var result = await _api.Trading.GetIsolatedMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All, 
                    allOrders: false, 
                    new[] { OrderStatusFilter.All },
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    direction: FilterDirection.Next,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedFuturesOrder[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(88));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                        .Select(x => 
                            new SharedFuturesOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode), 
                                x.ContractCode,
                                x.OrderId.ToString(),
                                ParseOrderType(x.OrderPriceType),
                                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                ParseOrderStatus(x.Status),
                                x.CreateTime)
                            {
                                ClientOrderId = x.ClientOrderId.ToString(),
                                AveragePrice = x.AverageFillPrice,
                                OrderPrice = x.Price,
                                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ValueFilled, contractQuantity: x.QuantityFilled),
                                TimeInForce = ParseTimeInForce(x.OrderPriceType),
                                UpdateTime = x.UpdateTime,
                                PositionSide = ParsePositionSide(x.Offset, x.Side),
                                ReduceOnly = x.ReduceOnly,
                                Leverage = x.LeverageRate
                            }).ToArray(), nextPageRequest);
            }

        }

        #endregion
        #region Get Futures Order Trades

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await _api.Trading.GetCrossMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(orders);

                return HttpResult.Ok(orders, orders.Data.Trades.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    new SharedOrderQuantity(quoteAssetQuantity: x.Value, contractQuantity: x.Quantity),
                    x.Price,
                    x.CreateTime)
                {
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray());
            }
            else
            {
                var orders = await _api.Trading.GetIsolatedMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(orders);

                return HttpResult.Ok(orders, orders.Data.Trades.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    new SharedOrderQuantity(quoteAssetQuantity: x.Value, contractQuantity: x.Quantity),
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray());
            }
        }

        #endregion
        #region Get Futures User Trade History

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesUserTradeHistory.GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFuturesUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000)
        {
            MaxAge = TimeSpan.FromDays(88),
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Trading.GetCrossMarginUserTradesAsync(
                    symbol,
                    MarginTradeType.All,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    filterDirection: FilterDirection.Next,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(88));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                        .Select(x => 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                                symbol,
                                x.OrderIdStr,
                                x.Id.ToString(),
                                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(quoteAssetQuantity: x.Value, contractQuantity: x.Quantity),
                                x.Price,
                                x.CreateTime)
                            {
                                Price = x.Price,
                                Fee = x.Fee,
                                FeeAsset = x.FeeAsset,
                                Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                            })
                        .ToArray(), nextPageRequest);
            }
            else
            {
                var result = await _api.Trading.GetIsolatedMarginUserTradesAsync(symbol,
                    MarginTradeType.All,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    filterDirection: FilterDirection.Next,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId) - 1),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(2),
                     TimeSpan.FromDays(88));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                            .Select(x => 
                                new SharedUserTrade(
                                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                                    symbol,
                                    x.OrderIdStr,
                                    x.Id.ToString(),
                                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                    new SharedOrderQuantity(quoteAssetQuantity: x.Value, contractQuantity: x.Quantity),
                                    x.Price,
                                    x.CreateTime)
                                {
                                    Price = x.Price,
                                    Fee = x.Fee,
                                    FeeAsset = x.FeeAsset,
                                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                                })
                            .ToArray(), nextPageRequest);
            }
        }

        #endregion
        #region Cancel Futures Order

        async Task<ICallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await _api.Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await _api.Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

        #endregion
        #region Get Positions

        async Task<ICallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Account.GetCrossMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        default)
                    {
                        UnrealizedPnl = x.UnrealizedPnl,
                        AverageOpenPrice = x.CostOpen,
                        Leverage = x.LeverageRate,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                    }).ToArray());
            }
            else
            {
                var result = await _api.Account.GetIsolatedMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                        x.ContractCode,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        default)
                    {
                        UnrealizedPnl = x.UnrealizedPnl,
                        AverageOpenPrice = x.CostOpen,
                        Leverage = x.LeverageRate,
                        PositionMode = x.PositionMode == PositionMode.SingleSide ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                    }).ToArray());
            }
        }

        #endregion
        #region Close Position

        async Task<ICallResult<SharedId>> IClosePosition.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
            => await ClosePositionAsync(request, ct).ConfigureAwait(false);

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await _api.Trading.CloseCrossMarginPositionAsync(
                    request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
            }
            else
            {
                var result = await _api.Trading.CloseIsolatedMarginPositionAsync(
                    direction: request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
            }
        }

        #endregion

        private OrderPriceType GetOrderPriceType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.LimitMaker) return OrderPriceType.PostOnly;
            if (type == SharedOrderType.Market) return OrderPriceType.Market;

            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderPriceType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderPriceType.FillOrKill;

            return OrderPriceType.Limit;
        }

        private Offset? GetOffset(SharedOrderSide side, SharedPositionSide? posSide)
        {
            if (posSide == null)
                return null;

            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy) return Offset.Open;
                return Offset.Close;
            }

            if (side == SharedOrderSide.Sell) return Offset.Open;
            return Offset.Close;
        }

        private SharedOrderStatus ParseOrderStatus(SwapMarginOrderStatus status)
        {
            if (status == SwapMarginOrderStatus.Submitting || status == SwapMarginOrderStatus.Submitted || status == SwapMarginOrderStatus.ReadyToSubmit || status == SwapMarginOrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == SwapMarginOrderStatus.Cancelled || status == SwapMarginOrderStatus.Cancelling || status == SwapMarginOrderStatus.PartiallyCancelled) return SharedOrderStatus.Canceled;
            if (status == SwapMarginOrderStatus.Filled) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderPriceType type)
        {
            if (type == OrderPriceType.Market) return SharedOrderType.Market;
            if (type == OrderPriceType.Limit) return SharedOrderType.Limit;
            if (type == OrderPriceType.PostOnly) return SharedOrderType.LimitMaker;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderPriceType tif)
        {
            if (tif == OrderPriceType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == OrderPriceType.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private SharedPositionSide? ParsePositionSide(Offset offset, OrderSide side)
        {
            if (offset == Offset.Both)
                return null;

            if (offset == Offset.Open)
            {
                if (side == OrderSide.Buy) return SharedPositionSide.Long;
                return SharedPositionSide.Short;
            }

            if (side == OrderSide.Sell) return SharedPositionSide.Long;
            return SharedPositionSide.Short;
        }
        #region Get Futures Order By Client Order Id

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrderByClientOrderId.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await _api.Trading.GetCrossMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder>(orders);

                var order = orders.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.ContractCode),
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    OrderPrice = order.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.ValueFilled, contractQuantity: order.QuantityFilled),
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
            else
            {
                var orders = await _api.Trading.GetIsolatedMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!orders.Success)
                    return HttpResult.Fail<SharedFuturesOrder>(orders);

                var order = orders.Data.Single();
                return HttpResult.Ok(orders, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.ContractCode),
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    OrderPrice = order.Price,
                    OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                    QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.ValueFilled, contractQuantity: order.QuantityFilled),
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
        }

        #endregion
        #region Cancel Futures Order By Client Order Id

        async Task<ICallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await _api.Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await _api.Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

        #endregion
    }
}
