using CryptoExchange.Net.Objects;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using System.Collections.Generic;
using System.Drawing;
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
        Task<WebCallResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(OrderSide direction, string? contractCode = null, string? pair = null, ContractType? contractType = null, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new isolated margin trigger order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86c95-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="triggerType">Trigger type</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="side">Order side</param>
        /// <param name="offset">Offset</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="orderPrice">Order price</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderId>> PlaceIsolatedMarginTriggerOrderAsync(string contractCode, TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new isolated margin trigger order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86dfe-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="triggerType">Trigger type</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="side">Order side</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="offset">Offset</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="orderPrice">Order price</param>
        /// <param name="orderPriceType">Order price type</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderId>> PlaceCrossMarginTriggerOrderAsync(TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, string? contractCode = null, string? pair = null, ContractType? contractType = null, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel isolated margin trigger order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86f61-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTriggerOrderAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel cross margin trigger order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87056-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTriggerOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all isolated margin trigger orders matching the parameters
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87161-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Filter by side</param>
        /// <param name="offset">Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTriggerOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all cross margin trigger orders matching the parameters
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb872c3-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Filter by side</param>
        /// <param name="offset">Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTriggerOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin open trigger orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb873a8-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderPage>> GetIsolatedMarginOpenTriggerOrdersAsync(string contractCode, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin open trigger orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb874fd-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTriggerOrderPage>> GetCrossMarginOpenTriggerOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, Enums.MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin trigger order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87658-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="status">Status</param>
        /// <param name="page">Page</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderPage>> GetIsolatedMarginTriggerOrderHistoryAsync(string contractCode, MarginTradeType tradeType, int daysPast, OrderStatusFilter? status = null, int? page = null, int? pageIndex = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin trigger order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb877ac-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="status">Status</param>
        /// <param name="page">Page</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTriggerOrderPage>> GetCrossMarginTriggerOrderHistoryAsync(MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderStatusFilter? status = null, int? page = null, int? pageIndex = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Set isolated margin order take profit / stop loss for an existing position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87911-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="takeProfitTriggerPrice">Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">Stop loss order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlResult>> SetIsolatedMarginTpSlAsync(string contractCode, OrderSide side, decimal quantity, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Set cross margin order take profit / stop loss for an existing position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87a6f-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="takeProfitTriggerPrice">Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">Stop loss order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlResult>> SetCrossMarginTpSlAsync(OrderSide side, decimal quantity, string? contractCode = null, string? pair = null, ContractType? contractType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel isolated margin take profit / stop loss orders for an existing position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87bc0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTpSlAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel isolated margin take profit / stop loss orders for an existing position
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87bc0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTpSlAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all isolated margin take profit / stop loss orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87d94-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTpSlAsync(string contractCode, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all cross margin take profit / stop loss orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87edb-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTpSlAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Get open isolated margin take profit / stop loss orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87fb0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlOrderPage>> GetIsolatedMarginOpenTpSlOrdersAsync(string? contractCode = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get open cross margin take profit / stop loss orders
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTpSlOrderPage>> GetCrossMarginOpenTpSlOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin take profit / stop loss order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88253-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tpSlOrderStatus">Status</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlClosedOrderPage>> GetIsolatedMarginTpSlHistoryAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross maring take profit / stop loss order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb883a0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="tpSlOrderStatus">Status</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTpSlClosedOrderPage>> GetCrossMarginTpSlHistoryAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin position open info with attached tp/sl orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb884f1-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPositionOpenTpSlOrders>> GetIsolatedMarginPositionOpenTpSlInfoAsync(string contractCode, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin position open info with attached tp/sl orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8864d-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossPositionOpenTpSlOrders>> GetCrossMarginPositionOpenTpSlInfoAsync(long orderId, string? contractCode = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Place isolated margin trailing order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88960-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="callbackRate">Callback rate, 0.01 means 1%</param>
        /// <param name="activePrice">Active price</param>
        /// <param name="orderPriceType">Price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderId>> PlaceIsolatedMarginTrailingOrderAsync(string contractCode, bool reduceOnly, OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, CancellationToken ct = default);

        /// <summary>
        /// Place cross margin trailing order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88bb5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="side">Side</param>
        /// <param name="offset">Offset</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="callbackRate">Callback rate, 0.01 means 1%</param>
        /// <param name="activePrice">Active price</param>
        /// <param name="orderPriceType">Price type</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTriggerOrderId>> PlaceCrossMarginTrailingOrderAsync(OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, string? contractCode = null, string? pair = null, ContractType? contractType = null, bool? reduceOnly = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an isolated margin trailing order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88f2c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTrailingOrderAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a cross margin trailing order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTrailingOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open isolated margin trailing orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8924f-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="side">Filter by side</param>
        /// <param name="offset">Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTrailingOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open cross margin trailing orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="side">Filter by side</param>
        /// <param name="offset">Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get open isolated margin trailing orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb894b7-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTrailingOrderPage>> GetOpenIsolatedMarginTrailingOrdersAsync(string contractCode, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get open cross margin trailing orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89614-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTrailingOrderPage>> GetOpenCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin trailing order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89781-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="tpSlOrderStatus">Status</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTrailingClosedOrderPage>> GetClosedIsolatedMarginTrailingOrdersAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin trailing order history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8996a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="tpSlOrderStatus">Status</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="daysPast">Amount of days ago. Max 90</param>
        /// <param name="contractCode"></param>
        /// <param name="pair">Pair</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortBy">Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossTrailingClosedOrderPage>> GetClosedCrossMarginTrailingOrdersAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
    }
}