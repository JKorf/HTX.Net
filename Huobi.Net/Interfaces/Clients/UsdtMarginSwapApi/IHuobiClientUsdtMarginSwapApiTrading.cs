using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt margin swap trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApiTrading
    {
        /// <summary>
        /// Cancel all cross margin orders fitting the parameters
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel all isolated margin order fitting the parameters
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel cross margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel cross margin orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel isolated margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel isolated margin orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract  code</param>
        /// <param name="orderId">Order ids</param>
        /// <param name="clientOrderId">Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default);
        /// <summary>
        /// Change cross margin leverage
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-leverage" /></para>
        /// </summary>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Change isolated margin leverage
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-leverage" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin closed orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-orders-new" /></para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="allOrders">All orders</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin open orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-current-unfilled-order-acquisition" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order details
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-detail-information-of-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order" /></para>
        /// </summary>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin user trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-match-results" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin closed orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-history-orders" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="allOrders">All orders</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin open orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-current-unfilled-order-acquisition" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get isoalted margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin order details
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-order-details-acquisition" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin user trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-acquire-history-match-results-new" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Place a new cross margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-place-an-order" /></para>
        /// </summary>
        /// <param name="quantity">Order quantity</param>
        /// <param name="side">Order side</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="price">Price</param>
        /// <param name="offset">Offset</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="takeProfitTriggerPrice">Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">Stop loss order price type</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPlacedOrderId>> PlaceCrossMarginOrderAsync(decimal quantity, OrderSide side, int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Place a new isolated margin order
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-place-an-order" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="side">Order side</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="price">Price</param>
        /// <param name="offset">Offset</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="takeProfitTriggerPrice">Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">Stop loss order price type</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPlacedOrderId>> PlaceIsolatedMarginOrderAsync(string contractCode, decimal quantity, OrderSide side, int leverageRate, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
    }
}