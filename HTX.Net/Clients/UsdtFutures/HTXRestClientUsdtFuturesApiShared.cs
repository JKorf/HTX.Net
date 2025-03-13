using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Enums;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesApi : IHTXRestClientUsdtFuturesApiShared
    {
        private const string _topicId = "HTXFutures";
        public string Exchange => HTXExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Balance Client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, null, default);

                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)).ToArray());
            }
            else
            {
                var result = await Account.GetIsolatedMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, null, default);

                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)
                {
                    IsolatedMarginSymbol = x.ContractCode
                }).ToArray());
            }
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var resultTicker = ExchangeData.GetTickerAsync(symbol, ct);
            var resultIndex = ExchangeData.GetSwapIndexPriceAsync(symbol, ct);
            var resultFunding = ExchangeData.GetFundingRateAsync(request.Symbol.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultTicker, resultFunding, resultIndex).ConfigureAwait(false);

            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);
            if (!resultFunding.Result)
                return resultFunding.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);
            if (!resultIndex.Result)
                return resultIndex.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            return resultTicker.Result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTicker(symbol, resultTicker.Result.Data.ClosePrice, resultTicker.Result.Data.HighPrice, resultTicker.Result.Data.LowPrice, resultTicker.Result.Data.Volume ?? 0, resultTicker.Result.Data.OpenPrice == null ? null : Math.Round((resultTicker.Result.Data.ClosePrice ?? 0) / resultTicker.Result.Data.OpenPrice.Value * 100 - 100, 2))
            {
                IndexPrice = resultIndex.Result.Data.Single().IndexPrice,
                FundingRate = resultFunding.Result.Data.FundingRate,
                NextFundingTime = resultFunding.Result.Data.FundingTime
            });
        }

        EndpointOptions<GetTickersRequest> IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ct: ct);
            var resultFunding = ExchangeData.GetFundingRatesAsync(ct: ct);
            await Task.WhenAll(resultTickers, resultFunding).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, null, default);
            if (!resultFunding.Result)
                return resultFunding.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, null, default);

            var data = resultTickers.Result.Data;
            if (request.TradingMode != null)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? x.ContractCode.Count(x => x == '-') == 1 : x.ContractCode.Count(x => x == '-') == 2);

            return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, SupportedTradingModes, data.Select(x =>
            {
                var funding = resultFunding.Result.Data.SingleOrDefault(p => p.ContractCode == x.ContractCode);
                return new SharedFuturesTicker(x.ContractCode!, x.ClosePrice, x.HighPrice, x.LowPrice, x.Volume ?? 0, x.OpenPrice == null ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                    FundingRate = funding?.FundingRate,
                    NextFundingTime = funding?.FundingTime
                };
            }).ToArray());
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, null, default);

            var data = result.Data;
            if (request.TradingMode.HasValue)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? x.BusinessType == BusinessType.Swap : x.BusinessType == BusinessType.Futures);

            return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange,
                request.TradingMode == null ? SupportedTradingModes : new[] { request.TradingMode.Value },
                data.Select(s => new SharedFuturesSymbol(
                s.BusinessType == BusinessType.Futures ? SharedSymbolType.DeliveryLinear : SharedSymbolType.PerpetualLinear,
                s.Asset,
                "USDT", 
                s.Symbol,
                s.Status == ContractStatus.Listing)
            {
                PriceStep = s.PriceTick,
                ContractSize = s.ContractSize,
                DeliveryTime = s.DeliveryDate,
                QuantityStep = 1
            }).ToArray());
        }

        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;
        IEnumerable<SharedOrderType> IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        IEnumerable<SharedTimeInForce> IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions()
        {
            RequestNotes = "ClientOrderId can only be an integer",
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            },
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.Leverage), typeof(int), "The leverage to use", 3)
            }
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol.TradingMode,
                SupportedTradingModes,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderTypes,
                ((IFuturesOrderRestClient)this).FuturesSupportedTimeInForce,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = request.MarginMode ?? ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Trading.PlaceCrossMarginOrderAsync(
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    quantity: (long)(request.Quantity ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId == null ? null : long.Parse(request.ClientOrderId),
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.PlaceIsolatedMarginOrderAsync(
                    request.Symbol.GetSymbol(FormatSymbol),
                    quantity: (long)(request.Quantity ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId == null ? null :long.Parse(request.ClientOrderId),
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, new ArgumentError("Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
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
                    Quantity = order.Quantity,
                    QuantityFilled = order.QuantityFilled,
                    QuoteQuantityFilled = order.ValueFilled,
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
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
                    Quantity = order.Quantity,
                    QuantityFilled = order.QuantityFilled,
                    QuoteQuantityFilled = order.ValueFilled,
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly,
                    Leverage = order.LeverageRate
                });
            }
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var orders = await Trading.GetCrossMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, orders.Data.Orders.Select(x => new SharedFuturesOrder(
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
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
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
                    return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, new ArgumentError("Symbol parameter required for isolated margin request"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var orders = await Trading.GetIsolatedMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, SupportedTradingModes ,orders.Data.Orders.Select(x => new SharedFuturesOrder(
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
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly,
                    Leverage = x.LeverageRate
                }).ToArray());
            }
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 1000, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromToken)
                fromId = long.Parse(fromToken.FromToken);

            // Get data
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var orders = await Trading.GetCrossMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All,
                    allOrders: false,
                    new[] { OrderStatusFilter.All },
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    fromId: fromId,
                    direction: FilterDirection.Previous,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

                var result = orders.Data.Select(x => new SharedFuturesOrder(
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
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly,
                    Leverage = x.LeverageRate
                }).ToArray();

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Min(x => x.OrderIdStr));

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, request.Symbol.TradingMode, result, nextToken);
            }
            else
            {
                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var orders = await Trading.GetIsolatedMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All, 
                    allOrders: false, 
                    new[] { OrderStatusFilter.All },
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    direction: FilterDirection.Previous,
                    fromId: fromId,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

                var result = orders.Data.Select(x => new SharedFuturesOrder(
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
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly,
                    Leverage = x.LeverageRate
                }).ToArray();

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(x => x.OrderIdStr));

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, request.Symbol.TradingMode, result, nextToken);
            }

        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.TradingMode,orders.Data.Trades.Select(x => new SharedUserTrade(
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray());
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.TradingMode,orders.Data.Trades.Select(x => new SharedUserTrade(
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray());
            }
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 1000, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromIdToken)
                fromId = long.Parse(fromIdToken.FromToken);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginUserTradesAsync(
                    symbol,
                    MarginTradeType.All,
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    filterDirection: FilterDirection.Previous,
                    fromId: fromId,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Min(o => o.Id).ToString());

                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                    symbol,
                    x.OrderIdStr,
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray(), nextToken);
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginUserTradesAsync(symbol,
                    MarginTradeType.All,
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    filterDirection: FilterDirection.Previous,
                    fromId: fromId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(o => o.Id).ToString());

                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                    symbol,
                    x.OrderIdStr,
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray(), nextToken);
            }
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, null, default);

                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, result.Data.Select(x => new SharedPosition(x.ContractCode, x.Quantity, default)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageOpenPrice = x.CostOpen,
                    Leverage = x.LeverageRate,
                    PositionSide = x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Account.GetIsolatedMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, null, default);

                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, result.Data.Select(x => new SharedPosition(x.ContractCode, x.Quantity, default)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageOpenPrice = x.CostOpen,
                    Leverage = x.LeverageRate,
                    PositionSide = x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Trading.CloseCrossMarginPositionAsync(
                    request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.CloseIsolatedMarginPositionAsync(
                    direction: request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
        }

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
            return SharedOrderStatus.Filled;
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
        #endregion

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 1000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 1000;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * (limit - 1);
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                //limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0, x.Volume ?? 0)).ToArray(), nextToken);
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.NotSupported, false, 2000, false);

        async Task<ExchangeWebResult<IEnumerable<SharedFuturesKline>>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedFuturesKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesKline>>(Exchange, validationError);

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                request.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesKline>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedFuturesKline>>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedFuturesKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)).ToArray());
        }

        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, false, 2000, false);

        async Task<ExchangeWebResult<IEnumerable<SharedFuturesKline>>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedFuturesKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesKline>>(Exchange, validationError);

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                request.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesKline>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedFuturesKline>>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedFuturesKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)).ToArray());
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 150 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(2000, false);
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetRecentTradesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Direction == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationSupport.Descending, true, 50, false);

        async Task<ExchangeWebResult<IEnumerable<SharedFundingRate>>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFundingRate>>(Exchange, validationError);

            int page = 1;
            int pageSize = 50;
            if (pageToken is PageToken token)
            {
                page = token.Page;
                pageSize = token.PageSize;
            }

            // Get data
            var result = await ExchangeData.GetHistoricalFundingRatesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                page: page,
                pageSize: pageSize,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, null, default);

            PageToken? nextToken = null;
            if (result.Data.Rates.Any())
                nextToken = new PageToken(page + 1, pageSize);

            // Return
            return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, request.Symbol.TradingMode,result.Data.Rates.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)).ToArray(), nextToken);
        }
        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetSwapOpenInterestAsync(request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOpenInterest(result.Data.Single().Volume));
        }

        #endregion

        #region Position Mode client

        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;
        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "Margin mode to get position mode for", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginPositionModeAsync("USDT", ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.DualSide ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
            }
            else
            {
                if (request.Symbol == null)
                    return new ExchangeWebResult<SharedPositionModeResult>(Exchange, new ArgumentError("Symbol parameter required for isolated mode"));

                var result = await Account.GetIsolatedMarginPositionModeAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.DualSide ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
            }
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "Margin mode to get position mode for", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.SetCrossMarginPositionModeAsync("USDT", request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.DualSide : PositionMode.SingleSide, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(request.PositionMode));
            }
            else
            {
                if (request.Symbol == null)
                    return new ExchangeWebResult<SharedPositionModeResult>(Exchange, new ArgumentError("Symbol parameter required for isolated mode"));

                var result = await Account.SetIsolatedMarginPositionModeAsync(request.Symbol.GetSymbol(FormatSymbol), request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.DualSide : PositionMode.SingleSide, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedPositionModeResult(request.PositionMode));
            }
        }
        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            var fees = result.Data.First();

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(fees.OpenMakerFee * 100, fees.OpenTakerFee * 100));
        }
        #endregion
    }
}
