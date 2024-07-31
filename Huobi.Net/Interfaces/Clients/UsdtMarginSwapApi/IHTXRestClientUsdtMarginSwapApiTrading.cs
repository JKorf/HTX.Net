using CryptoExchange.Net.Objects;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HTX.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// HTX usdt margin swap trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IHTXRestClientUsdtMarginSwapApiTrading
    {
        /// <summary>
        /// Cancel all cross margin orders fitting the parameters
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84ea6-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel all isolated margin order fitting the parameters
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84dae-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel cross margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84bb2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel cross margin orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84bb2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel isolated margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84a62-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel isolated margin orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84a62-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract  code</param>
        /// <param name="orderId">Order ids</param>
        /// <param name="clientOrderId">Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default);
        /// <summary>
        /// Change cross margin leverage
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb850d7-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginLeverageRate>> SetCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Change isolated margin leverage
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84ff2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginLeverageRate>> SetIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin closed orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85ba1-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="allOrders">All orders (true), or only orders in finished status (false)</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="direction">Direction</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, string? pair = null, bool? allOrders = null, IEnumerable<OrderStatusFilter>? status = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin open orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb858fe-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85379-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order details
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8562d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85379-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin user trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86121-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin closed orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85a53-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="allOrders">All orders (true), or only orders in finished status (false)</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="direction">Direction</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool? allOrders = null, IEnumerable<OrderStatusFilter>? status = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin open orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85791-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get isoalted margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85222-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin order details
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb854d2-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85222-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderIds">Order ids</param>
        /// <param name="clientOrderIds">Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin user trades
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85fa3-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Place a new cross margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84611-77b5-11ed-9966-0242ac110003" /></para>
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
        Task<WebCallResult<HTXPlacedOrderId>> PlaceCrossMarginOrderAsync(decimal quantity, OrderSide side, int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Place a new isolated margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb844bb-77b5-11ed-9966-0242ac110003" /></para>
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
        Task<WebCallResult<HTXPlacedOrderId>> PlaceIsolatedMarginOrderAsync(string contractCode, decimal quantity, OrderSide side, int leverageRate, decimal? price = null, Offset? offset = null, OrderPriceType? orderPriceType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders after the timeout elapses. Can be called again to extend the timeout. Set enable to false to disable the timeout
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000068-77b7-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="enable">Enabled or disable cancelation</param>
        /// <param name="timeout">The timeout after which all order are canceled</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCancelAfter>> CancelOrdersAfterAsync(bool enable, TimeSpan? timeout = null, CancellationToken ct = default);

        /// <summary>
        /// Lightning close position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86944-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="direction">Direction</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosePositionResult>> CloseIsolatedMarginPositionAsync(string contractCode, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Lightning close position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86944-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="direction">Direction</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(string contractCode, string pair, ContractType? contractType, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default);

    }
}