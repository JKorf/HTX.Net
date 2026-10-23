using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Models.UsdtFuturesV5;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using static CryptoExchange.Net.SharedApis.SharedCapabilities;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXRestClientUsdtFuturesV5SharedApi
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
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Margin mode priority: request parameter > exchange parameter > default (cross)
            var marginMode = 
                request.MarginMode != null 
                    ? (request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated)
                    : GetProvidedMarginMode(request) ?? MarginMode.Cross;
            var result = await _api.Trading.PlaceOrderAsync(
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
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
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

        #region Get Futures Order

        async Task<IExchangeCallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var marginMode = GetProvidedMarginMode(request);
            var order = await _api.Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), marginMode, orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.ContractCode),
                order.Data.ContractCode,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId.ToString(),
                AveragePrice = order.Data.AverageTradePrice,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.TradeTurnover, contractQuantity: order.Data.TradeQuantity),
                TimeInForce = ParseTimeInForce(order.Data),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = ParsePositionSide(order.Data),
                ReduceOnly = order.Data.ReduceOnly,
                Leverage = order.Data.LeverageRate
            });
        }

        async Task<IExchangeCallResult<SharedFuturesOrder>> IGetFuturesOrderByClientOrderId.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var marginMode = GetProvidedMarginMode(request);
            var order = await _api.Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), marginMode, clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.ContractCode),
                order.Data.ContractCode,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId.ToString(),
                AveragePrice = order.Data.AverageTradePrice,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.TradeTurnover, contractQuantity: order.Data.TradeQuantity),
                TimeInForce = ParseTimeInForce(order.Data),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = ParsePositionSide(order.Data),
                ReduceOnly = order.Data.ReduceOnly,
                Leverage = order.Data.LeverageRate
            });
        }

        #endregion


        #region Get Open Futures Orders

        async Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
        };
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var marginMode = GetProvidedMarginMode(request);
            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await _api.Trading.GetOpenOrdersAsync(symbol, marginMode, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.ContractCode),
                x.ContractCode,
                x.OrderId.ToString(),
                ParseOrderType(x),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId.ToString(),
                AveragePrice = x.AverageTradePrice,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.TradeTurnover, contractQuantity: x.TradeQuantity),
                TimeInForce = ParseTimeInForce(x),
                UpdateTime = x.UpdateTime,
                PositionSide = ParsePositionSide(x),
                ReduceOnly = x.ReduceOnly,
                Leverage = x.LeverageRate
            }).ToArray());
        }

        #endregion

        #region Get Closed Futures Orders

        async Task<IExchangeCallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000)
        {
            MaxAge = TimeSpan.FromDays(88),
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("MarginMode", "The margin mode", SharedMarginMode.Cross)
            ]
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
            var marginMode = GetProvidedMarginMode(request);
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.Trading.GetOrderHistoryAsync(
                symbol,
                marginMode ?? MarginMode.Cross,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                direction: FilterDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromId(result.Data.Min(x => long.Parse(x.OrderId)) - 1),
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
                        ParseOrderType(x),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId.ToString(),
                        AveragePrice = x.AverageTradePrice,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.TradeTurnover, contractQuantity: x.TradeQuantity),
                        TimeInForce = ParseTimeInForce(x),
                        UpdateTime = x.UpdateTime,
                        PositionSide = ParsePositionSide(x),
                        ReduceOnly = x.ReduceOnly,
                        Leverage = x.LeverageRate
                    })
                .ToArray(), nextPageRequest);
        }

        #endregion


        #region Get Futures Order Trades

        async Task<IExchangeCallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null) 
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var orders = await _api.Trading.GetOrderDetailsAsync(symbol, orderId: request.OrderId).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                symbol,
                request.OrderId,
                x.TradeId!,
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(quoteAssetQuantity: x.Turnover, contractQuantity: x.Quantity),
                x.Price ?? 0,
                x.CreateTime ?? default)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeCurrency,
                Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        #endregion


        #region Cancel Futures Order

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel Futures Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel All Futures Symbol Orders

        async Task<IExchangeCallResult> ICancelAllFuturesSymbolOrders.CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesSymbolOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesSymbolOrdersOptions CancelAllFuturesSymbolOrdersOptions { get; } = new CancelAllFuturesSymbolOrdersOptions(_exchangeName, true);
        public async Task<HttpResult> CancelAllFuturesSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesSymbolOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail(Exchange, validationError);

            var order = await _api.Trading.CancelAllOrdersAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail(order);

            return HttpResult.Ok(order);
        }

        #endregion

        #region Cancel All Futures Orders

        async Task<IExchangeCallResult> ICancelAllFuturesOrders.CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
            => await CancelAllFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllFuturesOrdersOptions CancelAllFuturesOrdersOptions { get; } = new CancelAllFuturesOrdersOptions(_exchangeName);
        public async Task<HttpResult> CancelAllFuturesOrdersAsync(CancelAllOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail(Exchange, validationError);

            var order = await _api.Trading.CancelAllOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail(order);

            return HttpResult.Ok(order);
        }

        #endregion

        private SharedOrderType ParseOrderType(HTXOrderV5 data)
        {
            if (data.Type == OrderTypeV5.Market)
                return SharedOrderType.Market;
            if (data.Type == OrderTypeV5.PostOnly)
                return SharedOrderType.LimitMaker;
            return SharedOrderType.Limit;
        }

        private SharedPositionSide? ParsePositionSide(HTXOrderV5 data)
        {
            if (data.PositionSide == FuturesPositionSide.Both)
                return null;

            if (data.PositionSide == FuturesPositionSide.Long)
                return SharedPositionSide.Long;

            return SharedPositionSide.Short;
        }
        private SharedTimeInForce? ParseTimeInForce(HTXOrderV5 data)
        {
            return data.TimeInForce switch
            {
                TimeInForce.GoodTillCancel => SharedTimeInForce.GoodTillCanceled,
                TimeInForce.ImmediateOrCancel => SharedTimeInForce.ImmediateOrCancel,
                TimeInForce.FillOrKill => SharedTimeInForce.FillOrKill,
                _ => null
            };
        }

        private SharedOrderStatus ParseOrderStatus(HTXOrderV5 data)
        {
            return data.Status switch
            {
                OrderStatusV5.New => SharedOrderStatus.Open,
                OrderStatusV5.PartiallyFilled => SharedOrderStatus.Open,
                OrderStatusV5.Filled => SharedOrderStatus.Filled,
                OrderStatusV5.Canceled => SharedOrderStatus.Canceled,
                OrderStatusV5.PartiallyCanceled => SharedOrderStatus.Canceled,
                OrderStatusV5.Rejected => SharedOrderStatus.Canceled,
                _ => SharedOrderStatus.Unknown
            };
        }

    }
}
