using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Models.Rest;
using HTX.Net.Enums;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System.Linq;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Models.EndpointOptions;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesApi : IHTXRestClientUsdtFuturesApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.DeliveryLinear };

        #region Balance Client
        EndpointOptions IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions("GetBalancesRequest", true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

                return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)));
            }
            else
            {
                var result = await Account.GetIsolatedMarginAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

                return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedBalance(x.MarginAsset, x.MarginBalance, x.MarginFrozen + x.MarginBalance)
                {
                    IsolatedMarginAsset = x.ContractCode
                }));
            }
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var resultTicker = ExchangeData.GetTickerAsync(symbol, ct);
            var resultIndex = ExchangeData.GetSwapIndexPriceAsync(symbol, ct);
            var resultFunding = ExchangeData.GetFundingRateAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)), ct);
            await Task.WhenAll(resultTicker, resultFunding, resultIndex).ConfigureAwait(false);

            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);
            if (!resultFunding.Result)
                return resultFunding.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);
            if (!resultIndex.Result)
                return resultIndex.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);

            return resultTicker.Result.AsExchangeResult(Exchange, new SharedFuturesTicker(symbol, resultTicker.Result.Data.ClosePrice ?? 0, resultTicker.Result.Data.HighPrice ?? 0, resultTicker.Result.Data.LowPrice ?? 0, resultTicker.Result.Data.Volume ?? 0)
            {
                IndexPrice = resultIndex.Result.Data.Single().IndexPrice,
                FundingRate = resultFunding.Result.Data.FundingRate,
                NextFundingTime = resultFunding.Result.Data.FundingTime
            });
        }

        EndpointOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions("GetTickersRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ct: ct);
            var resultFunding = ExchangeData.GetFundingRatesAsync(ct: ct);
            await Task.WhenAll(resultTickers, resultFunding).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);
            if (!resultFunding.Result)
                return resultFunding.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);

            return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, resultTickers.Result.Data.Select(x =>
            {
                var funding = resultFunding.Result.Data.SingleOrDefault(p => p.ContractCode == x.ContractCode);
                return new SharedFuturesTicker(x.ContractCode!, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0)
                {
                    FundingRate = funding?.FundingRate,
                    NextFundingTime = funding?.FundingTime
                };
            }).ToList());
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions("GetFuturesSymbolsRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, default);

            var data = result.Data.Where(x => apiType == ApiType.PerpetualLinear ? x.BusinessType == BusinessType.Swap : x.BusinessType == BusinessType.Futures);
            return result.AsExchangeResult(Exchange, data.Select(s => new SharedFuturesSymbol(
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
            }));
        }

        #endregion

        #region Futures Order Client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity))
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (request.MarginMode == SharedMarginMode.Cross)
            {
#warning is this correct for contractcode/pair/type?
                var result = await Trading.PlaceCrossMarginOrderAsync(
                    contractCode: request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                    quantity: (long)(request.Quantity ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: long.Parse(request.ClientOrderId),
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, default);

                return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.PlaceIsolatedMarginOrderAsync(
                    request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                    quantity: (long)(request.Quantity ?? 0),
                    side: request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    leverageRate: (int)(request.Leverage ?? 0),
                    orderPriceType: GetOrderPriceType(request.OrderType, request.TimeInForce),
                    price: request.Price,
                    offset: GetOffset(request.Side, request.PositionSide),
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: long.Parse(request.ClientOrderId),
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, default);

                return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, new ArgumentError("Invalid order id"));

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)), orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, new SharedFuturesOrder(
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    QuantityFilled = order.QuantityFilled,
                    QuoteQuantityFilled = order.ValueFilled,
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly
                });
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)), orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<SharedFuturesOrder>(Exchange, default);

                var order = orders.Data.Single();
                return orders.AsExchangeResult(Exchange, new SharedFuturesOrder(
                    order.ContractCode,
                    order.OrderId.ToString(),
                    ParseOrderType(order.OrderPriceType),
                    order.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Status),
                    order.CreateTime)
                {
                    ClientOrderId = order.ClientOrderId.ToString(),
                    AveragePrice = order.AverageFillPrice,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    QuantityFilled = order.QuantityFilled,
                    QuoteQuantityFilled = order.ValueFilled,
                    TimeInForce = ParseTimeInForce(order.OrderPriceType),
                    UpdateTime = order.UpdateTime,
                    PositionSide = ParsePositionSide(order.Offset, order.Side),
                    ReduceOnly = order.ReduceOnly
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
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol?.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
                var orders = await Trading.GetCrossMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

                return orders.AsExchangeResult(Exchange, orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    x.ContractCode,
                    x.OrderId.ToString(),
                    ParseOrderType(x.OrderPriceType),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.Status),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId.ToString(),
                    AveragePrice = x.AverageFillPrice,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly
                }));
            }
            else
            {
                var symbol = request.Symbol?.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
#warning required symbol, only for isolated
                var orders = await Trading.GetIsolatedMarginOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

                return orders.AsExchangeResult(Exchange, orders.Data.Orders.Select(x => new SharedFuturesOrder(
                    x.ContractCode,
                    x.OrderId.ToString(),
                    ParseOrderType(x.OrderPriceType),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.Status),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId.ToString(),
                    AveragePrice = x.AverageFillPrice,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly
                }));
            }
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(true, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromToken)
                fromId = long.Parse(fromToken.FromToken);

            // Get data
            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
                var orders = await Trading.GetCrossMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All,
                    allOrders: false,
                    new[] { OrderStatusFilter.All },
                    startTime: request.Filter?.StartTime,
                    endTime: request.Filter?.EndTime,
                    fromId: fromId,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

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
                    Price = x.Price,
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly
                });

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(x => x.OrderIdStr));

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, result, nextToken);
            }
            else
            {
                var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
                var orders = await Trading.GetIsolatedMarginClosedOrdersAsync(
                    symbol,
                    MarginTradeType.All, 
                    allOrders: false, 
                    new[] { OrderStatusFilter.All },
                    startTime: request.Filter?.StartTime,
                    endTime: request.Filter?.EndTime,
                    fromId: fromId,
                    ct: ct).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

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
                    Price = x.Price,
                    Quantity = x.Quantity,
                    QuantityFilled = x.QuantityFilled,
                    QuoteQuantityFilled = x.ValueFilled,
                    TimeInForce = ParseTimeInForce(x.OrderPriceType),
                    UpdateTime = x.UpdateTime,
                    PositionSide = ParsePositionSide(x.Offset, x.Side),
                    ReduceOnly = x.ReduceOnly
                });

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(x => x.OrderIdStr));

                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, result, nextToken);
            }

        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

                return orders.AsExchangeResult(Exchange, orders.Data.Trades.Select(x => new SharedUserTrade(
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }));
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginOrderDetailsAsync(symbol, orderId: orderId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

                return orders.AsExchangeResult(Exchange, orders.Data.Trades.Select(x => new SharedUserTrade(
                    symbol,
                    request.OrderId,
                    x.Id.ToString(),
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }));
            }
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(true, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromIdToken)
                fromId = long.Parse(fromIdToken.FromToken);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var orders = await Trading.GetCrossMarginUserTradesAsync(
                    symbol,
                    MarginTradeType.All,
                    startTime: request.Filter?.StartTime,
                    endTime: request.Filter?.EndTime,
                    fromId: fromId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(o => o.Id).ToString());

                return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedUserTrade(
                    symbol,
                    x.OrderIdStr,
                    x.Id.ToString(),
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }), nextToken);
            }
            else
            {
                var orders = await Trading.GetIsolatedMarginUserTradesAsync(symbol,
                    MarginTradeType.All,
                    startTime: request.Filter?.StartTime,
                    endTime: request.Filter?.EndTime,
                    fromId: fromId).ConfigureAwait(false);
                if (!orders)
                    return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

                // Get next token
                FromIdToken? nextToken = null;
                if (orders.Data.Any())
                    nextToken = new FromIdToken(orders.Data.Max(o => o.Id).ToString());

                return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedUserTrade(
                    symbol,
                    x.OrderIdStr,
                    x.Id.ToString(),
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.Role == OrderRole.Maker ? SharedRole.Maker : SharedRole.Taker
                }), nextToken);
            }
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var order = await Trading.CancelCrossMarginOrderAsync(contractCode: request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)), orderId: orderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, default);

                return order.AsExchangeResult(Exchange, new SharedId(request.OrderId));
            }
            else
            {
                var order = await Trading.CancelIsolatedMarginOrderAsync(contractCode: request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)), orderId: orderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, default);

                return order.AsExchangeResult(Exchange, new SharedId(request.OrderId));
            }
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);
            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Account.GetCrossMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);

                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, result.Data.Select(x => new SharedPosition(x.ContractCode, x.Quantity, default)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageEntryPrice = x.CostOpen,
                    Leverage = x.LeverageRate,
                    PositionSide = x.PositionMode == PositionMode.SingleSide ? SharedPositionSide.Both : x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToList());
            }
            else
            {
                var result = await Account.GetIsolatedMarginPositionsAsync(contractCode: request.Symbol?.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);

                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, result.Data.Select(x => new SharedPosition(x.ContractCode, x.Quantity, default)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageEntryPrice = x.CostOpen,
                    Leverage = x.LeverageRate,
                    PositionSide = x.PositionMode == PositionMode.SingleSide ? SharedPositionSide.Both : x.Side == OrderSide.Sell ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToList());
            }
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("MarginMode", typeof(SharedMarginMode), "The margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var marginMode = exchangeParameters.GetValue<SharedMarginMode>(Exchange, "MarginMode");
            if (marginMode == SharedMarginMode.Cross)
            {
                var result = await Trading.CloseCrossMarginPositionAsync(
                    request.PositionSide == SharedPositionSide.Short ? OrderSide.Sell : OrderSide.Buy,
                    contractCode: request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, default);

                return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
            }
            else
            {
                var result = await Trading.CloseIsolatedMarginPositionAsync(
                    direction: request.PositionSide == SharedPositionSide.Short ? OrderSide.Sell : OrderSide.Buy,
                    contractCode: request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                    ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, default);

                return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
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

        private Offset GetOffset(SharedOrderSide side, SharedPositionSide posSide)
        {
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

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(true, false)
        {
            MaxRequestDataPoints = 1000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                interval,
                fromTimestamp ?? request.Filter?.StartTime,
                request.Filter?.EndTime,
                request.Filter?.Limit ?? 1000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.Filter?.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.OpenTime);
                if (maxOpenTime < request.Filter.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0, x.Volume ?? 0)), nextToken);
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(false, false)
        {
            MaxTotalDataPoints = 2000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                interval,
                request.Filter?.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedMarkKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)));
        }

        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(false, false)
        {
            MaxTotalDataPoints = 2000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                interval,
                request.Filter?.Limit ?? 2000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedMarkKline(x.OpenTime, x.ClosePrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.OpenPrice ?? 0)));
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 150 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(2000, false);
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetRecentTradesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType)),
                limit: request.Limit ?? 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(false, false);

        async Task<ExchangeWebResult<IEnumerable<SharedFundingRate>>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
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
                request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)),
                page: page,
                pageSize: pageSize,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, default);

            var nextToken = new PageToken(page + 1, pageSize);

            // Return
            return result.AsExchangeResult(Exchange, result.Data.Rates.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)), nextToken);
        }
        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetSwapOpenInterestAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset)), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, default);

#warning what value do we always return from this? Contracts or quantity?
            return result.AsExchangeResult(Exchange, new SharedOpenInterest(result.Data.Single().Volume));
        }

        #endregion
    }
}
