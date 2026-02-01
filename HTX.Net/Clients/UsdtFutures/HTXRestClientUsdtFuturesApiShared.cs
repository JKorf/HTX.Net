using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

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
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(AccountTypeFilter.Futures)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)).ToArray());
            }
            else
            {
                var result = await Account.GetIsolatedMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)
                {
                    IsolatedMarginSymbol = x.ContractCode
                }).ToArray());
            }
        }

        #endregion

        #region Ticker client

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
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

            return resultTicker.Result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, resultTicker.Result.Data.ClosePrice, resultTicker.Result.Data.HighPrice, resultTicker.Result.Data.LowPrice, resultTicker.Result.Data.Volume ?? 0, resultTicker.Result.Data.OpenPrice == null ? null : Math.Round((resultTicker.Result.Data.ClosePrice ?? 0) / resultTicker.Result.Data.OpenPrice.Value * 100 - 100, 2))
            {
                IndexPrice = resultIndex.Result.Data.Single().IndexPrice,
                FundingRate = resultFunding.Result.Data.FundingRate,
                NextFundingTime = resultFunding.Result.Data.FundingTime
            });
        }

        GetTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ct: ct);
            var resultFunding = ExchangeData.GetFundingRatesAsync(ct: ct);
            await Task.WhenAll(resultTickers, resultFunding).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);
            if (!resultFunding.Result)
                return resultFunding.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            IEnumerable<HTXListTicker> data = resultTickers.Result.Data;
            if (request.TradingMode != null)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? x.ContractCode!.Count(x => x == '-') == 1 : x.ContractCode!.Count(x => x == '-') == 2);

            return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, SupportedTradingModes, data.Select(x =>
            {
                var funding = resultFunding.Result.Data.SingleOrDefault(p => p.ContractCode == x.ContractCode);
                return new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode!, x.ClosePrice, x.HighPrice, x.LowPrice, x.Volume ?? 0, x.OpenPrice == null ? null : Math.Round((x.ClosePrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                    FundingRate = funding?.FundingRate,
                    NextFundingTime = funding?.FundingTime
                };
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetBookTickerAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            var bookTicker = resultTicker.Data.Single();
            return resultTicker.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
                symbol,
                bookTicker.Ask.Price,
                bookTicker.Ask.Quantity,
                bookTicker.Bid.Price,
                bookTicker.Bid.Quantity));
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            IEnumerable<HTXContractInfo> data = result.Data;
            if (request.TradingMode.HasValue)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? x.BusinessType == BusinessType.Swap : x.BusinessType == BusinessType.Futures);

            var response = result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange,
                request.TradingMode == null ? SupportedTradingModes : new[] { request.TradingMode.Value },
                data.Select(s => new SharedFuturesSymbol(
                s.BusinessType == BusinessType.Futures ? TradingMode.DeliveryLinear : TradingMode.PerpetualLinear,
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

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }
        public async Task<ExchangeResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        public async Task<ExchangeResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        public async Task<ExchangeResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        string IFuturesOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomLong(10).ToString();

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(true)
        {
            RequestNotes = "ClientOrderId can only be an integer",
            OptionalExchangeParameters = new List<ParameterDescription>
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
                request.TradingMode,
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

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.PlaceIsolatedMarginOrderAsync(
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
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.ContractCode),
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
                var orders = await Trading.GetIsolatedMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.ContractCode),
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

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var orders = await Trading.GetCrossMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), 
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
                    return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, ArgumentError.Missing(nameof(GetOpenOrdersRequest.Symbol), "Symbol parameter required for isolated margin request"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var orders = await Trading.GetIsolatedMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, SupportedTradingModes ,orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), 
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

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 1000, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromToken)
                fromId = long.Parse(fromToken.FromToken);

            // Get data
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol!.GetSymbol(FormatSymbol);
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
                    return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

                var result = orders.Data.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), 
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
                }).ToArray();

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Min(x => x.OrderIdStr)!);

                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol.TradingMode, result, nextToken);
            }
            else
            {
                var symbol = request.Symbol!.GetSymbol(FormatSymbol);
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
                    return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

                var result = orders.Data.Select(x => new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), 
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
                }).ToArray();

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(x => x.OrderIdStr)!);

                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol.TradingMode, result, nextToken);
            }

        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Trades.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, symbol), 
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
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
                var orders = await Trading.GetIsolatedMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Trades.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, symbol), 
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
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromIdToken)
                fromId = long.Parse(fromIdToken.FromToken);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
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
                    return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Min(o => o.Id)!.ToString());

                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, symbol), 
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
                    return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(o => o.Id)!.ToString());

                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
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
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
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
        async Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPosition[]>(Exchange, validationError);
            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedPosition[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, default)
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
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedPosition[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.ContractCode), x.ContractCode, x.Quantity, default)
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
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Trading.CloseCrossMarginPositionAsync(
                    request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()!));
            }
            else
            {
                var result = await Trading.CloseIsolatedMarginPositionAsync(
                    direction: request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                    contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()!));
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

        #region Futures Client Id Order Client

        EndpointOptions<GetOrderRequest> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.ContractCode),
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
                var orders = await Trading.GetIsolatedMarginOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.ContractCode),
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

        EndpointOptions<CancelOrderRequest> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: long.Parse(request.OrderId)).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
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

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

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

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                startTime,
                endTime,
                //limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedKline[]>(Exchange, request.Symbol.TradingMode, result.Data.AsEnumerable().Reverse().Select(x => 
                new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0, x.Volume ?? 0)).ToArray(), nextToken);
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.NotSupported, false, 2000, false);

        async Task<ExchangeWebResult<SharedFuturesKline[]>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                symbol,
                interval,
                request.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, request.Symbol.TradingMode, result.Data.AsEnumerable().Reverse().Select(x =>
                new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)).ToArray());
        }

        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, false, 2000, false);

        async Task<ExchangeWebResult<SharedFuturesKline[]>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                symbol,
                interval,
                request.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, request.Symbol.TradingMode, result.Data.AsEnumerable().Reverse().Select(x => 
                new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)).ToArray());
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 150 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(2000, false);
        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetRecentTradesAsync(
                symbol,
                limit: request.Limit ?? 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.AsEnumerable().Reverse().Select(x => 
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Direction == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationSupport.Descending, true, 50, false);

        async Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFundingRate[]>(Exchange, validationError);

            int page = 1;
            int pageSize = 50;
            if (pageToken is PageToken token)
            {
                page = token.Page;
                pageSize = token.PageSize;
            }

            // Get data
            var result = await ExchangeData.GetHistoricalFundingRatesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                page: page,
                pageSize: pageSize,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFundingRate[]>(Exchange, null, default);

            PageToken? nextToken = null;
            if (result.Data.Rates.Any())
                nextToken = new PageToken(page + 1, pageSize);

            // Return
            return result.AsExchangeResult<SharedFundingRate[]>(Exchange, request.Symbol.TradingMode,result.Data.Rates.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)).ToArray(), nextToken);
        }
        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetSwapOpenInterestAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
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
                    return new ExchangeWebResult<SharedPositionModeResult>(Exchange, ArgumentError.Missing(nameof(GetPositionModeRequest.Symbol), "Symbol parameter required for isolated mode"));

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
                    return new ExchangeWebResult<SharedPositionModeResult>(Exchange, ArgumentError.Missing(nameof(SetPositionModeRequest.Symbol), "Symbol parameter required for isolated mode"));

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
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            var fees = result.Data.First();

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFee(fees.OpenMakerFee * 100, fees.OpenTakerFee * 100));
        }
        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(false)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            },
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.Leverage), typeof(int), "The leverage to use", 3),
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetOrderSide(request);
            var validationError = ((IFuturesTriggerOrderRestClient)this).PlaceFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes, side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = request.MarginMode ?? ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Trading.PlaceCrossMarginTriggerOrderAsync(
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
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                // Return
                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.PlaceIsolatedMarginTriggerOrderAsync(
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
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                // Return
                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).GetFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTriggerOrder>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOpenTriggerOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var triggerOrder = orders.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (triggerOrder != null)
                {
                    return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, triggerOrder.ContractCode),
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

                var orderHist = await Trading.GetCrossMarginTriggerOrderHistoryAsync(                    
                    MarginTradeType.All,
                    90,
                    OrderStatusFilter.All,
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orderHist)
                    return orderHist.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var closedOrder = orderHist.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (closedOrder == null)
                    return orders.AsExchangeError<SharedFuturesTriggerOrder>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Not found")));

                if (string.IsNullOrEmpty(closedOrder.RelationOrderId) && closedOrder.RelationOrderId != "-1")
                {
                    return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, closedOrder.ContractCode),
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

                var placedOrderResult = await Trading.GetCrossMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: long.Parse(closedOrder.RelationOrderId!), ct: ct).ConfigureAwait(false);
                if (!placedOrderResult)
                    return placedOrderResult.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var placedOrder = placedOrderResult.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, closedOrder.ContractCode),
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
                var orders = await Trading.GetIsolatedMarginOpenTriggerOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var triggerOrder = orders.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (triggerOrder != null)
                {
                    return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, triggerOrder.ContractCode),
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

                var orderHist = await Trading.GetIsolatedMarginTriggerOrderHistoryAsync(
                    contractCode: request.Symbol.GetSymbol(FormatSymbol),
                    MarginTradeType.All,
                    90,
                    OrderStatusFilter.All,
                    page: 1,
                    pageSize: 50,
                    ct: ct).ConfigureAwait(false);
                if (!orderHist)
                    return orderHist.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var closedOrder = orderHist.Data.Orders.SingleOrDefault(x => x.OrderIdStr == request.OrderId);
                if (closedOrder == null)
                    return orders.AsExchangeError<SharedFuturesTriggerOrder>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Not found")));

                if (string.IsNullOrEmpty(closedOrder.RelationOrderId) && closedOrder.RelationOrderId != "-1")
                {
                    return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, closedOrder.ContractCode),
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

                var placedOrderResult = await Trading.GetIsolatedMarginOrderAsync(contractCode: request.Symbol.GetSymbol(FormatSymbol), orderId: long.Parse(closedOrder.RelationOrderId!), ct: ct).ConfigureAwait(false);
                if (!placedOrderResult)
                    return placedOrderResult.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                var placedOrder = placedOrderResult.Data.Single();
                return orders.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, closedOrder.ContractCode),
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

            return SharedTriggerOrderStatus.Active;
        }

        EndpointOptions<CancelOrderRequest> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).CancelFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = ExchangeParameters.GetValue<SharedMarginMode>(request.ExchangeParameters, Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await Trading.CancelCrossMarginTriggerOrderAsync(
                request.OrderId,
                contractCode: request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await Trading.CancelIsolatedMarginTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
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

        #region Tp/SL Client
        EndpointOptions<SetTpSlRequest> IFuturesTpSlRestClient.SetFuturesTpSlOptions { get; } = new EndpointOptions<SetTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetTpSlRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay),
                new ParameterDescription(nameof(SetTpSlRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross),
                new ParameterDescription(nameof(SetTpSlRequest.Quantity), typeof(decimal), "The quantity to close", 0.123m)
            }
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesTpSlRestClient.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).SetFuturesTpSlOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            WebCallResult<HTXTpSlResult> result;
            if (request.MarginMode == SharedMarginMode.Cross)
            {
                result = await Trading.SetCrossMarginTpSlAsync(
                    request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell: OrderSide.Buy,
                    request.Quantity!.Value,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    takeProfitTriggerPrice: request.TpSlSide == SharedTpSlSide.TakeProfit ? request.TriggerPrice : null,
                    takeProfitOrderPriceType: request.TpSlSide == SharedTpSlSide.TakeProfit ? OrderPriceType.Optimal20 : null,
                    stopLossTriggerPrice: request.TpSlSide == SharedTpSlSide.StopLoss ? request.TriggerPrice : null,
                    stopLossOrderPriceType: request.TpSlSide == SharedTpSlSide.StopLoss ? OrderPriceType.Optimal20 : null,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                result = await Trading.SetIsolatedMarginTpSlAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                    request.Quantity!.Value,
                    takeProfitTriggerPrice: request.TpSlSide == SharedTpSlSide.TakeProfit ? request.TriggerPrice : null,
                    takeProfitOrderPriceType: request.TpSlSide == SharedTpSlSide.TakeProfit ? OrderPriceType.Optimal20 : null,
                    stopLossTriggerPrice: request.TpSlSide == SharedTpSlSide.StopLoss ? request.TriggerPrice : null,
                    stopLossOrderPriceType: request.TpSlSide == SharedTpSlSide.StopLoss ? OrderPriceType.Optimal20 : null,
                    ct: ct).ConfigureAwait(false);
            }

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.TpOrder?.OrderIdStr ?? result.Data.SlOrder!.OrderIdStr));
        }

        EndpointOptions<CancelTpSlRequest> IFuturesTpSlRestClient.CancelFuturesTpSlOptions { get; } = new EndpointOptions<CancelTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123"),
                new ParameterDescription(nameof(SetTpSlRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };

        async Task<ExchangeWebResult<bool>> IFuturesTpSlRestClient.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).CancelFuturesTpSlOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<bool>(Exchange, validationError);

            WebCallResult<HTXTriggerOrderResult> result;
            if (request.MarginMode == SharedMarginMode.Cross)
            {
                result = await Trading.CancelCrossMarginTpSlAsync(
                    request.OrderId!,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                result = await Trading.CancelIsolatedMarginTpSlAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.OrderId!,
                    ct: ct).ConfigureAwait(false);
            }
            if (!result)
                return result.AsExchangeResult<bool>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, true);
        }

        #endregion
    }
}
