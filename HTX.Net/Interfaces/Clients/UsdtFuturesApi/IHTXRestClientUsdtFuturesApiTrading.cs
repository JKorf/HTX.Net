using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// HTX usdt futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApiTrading
    {
        /// <summary>
        /// Cancel all cross margin orders fitting the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84ea6-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel all isolated margin orders fitting the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84dae-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel a cross margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84bb2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_cancel
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>order_id</c>"] The order id</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] The client order id</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel cross margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84bb2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_cancel
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_id</c>"] Order ids</param>
        /// <param name="clientOrderIds">["<c>client_order_id</c>"] Client order ids</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel an isolated margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84a62-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel isolated margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84a62-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order ids</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default);
        /// <summary>
        /// Set cross margin leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb850d7-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_switch_lever_rate
        /// </para>
        /// </summary>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginLeverageRate>> SetCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Set isolated margin leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84ff2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_switch_lever_rate
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginLeverageRate>> SetIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85ba1-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v3/swap_cross_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="allOrders">["<c>type</c>"] All orders (true), or only orders in finished status (false)</param>
        /// <param name="pair">["<c>pair</c>"] Filter by pair, for example `ETH-USDT`</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="direction">["<c>direct</c>"] Direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossMarginOrder[]>> GetCrossMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, IEnumerable<OrderStatusFilter> status, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb858fe-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair</param>
        /// <param name="page">["<c>page_index</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85379-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_order_info
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginOrder[]>> GetCrossMarginOrderAsync(string? contractCode = null, string? pair = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin order details
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8562d-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_order_detail
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85379-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_order_info
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_id</c>"] Order ids</param>
        /// <param name="clientOrderIds">["<c>client_order_id</c>"] Client order ids</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginOrder[]>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? pair = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85fa3-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v3/swap_matchresults
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="filterDirection">["<c>direct</c>"] Filter direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXIsolatedMarginUserTrade[]>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86121-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v3/swap_cross_matchresults
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="filterDirection">["<c>direct</c>"] Filter direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossMarginUserTrade[]>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85a53-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v3/swap_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="allOrders">["<c>type</c>"] All orders (true), or only orders in finished status (false)</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="direction">["<c>direct</c>"] Direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, IEnumerable<OrderStatusFilter> status, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85791-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default);
        /// <summary>
        /// Get an isolated margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85222-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_order_info
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin order details
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb854d2-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_order_detail
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb85222-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_order_info
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderIds">["<c>order_id</c>"] Order ids</param>
        /// <param name="clientOrderIds">["<c>client_order_id</c>"] Client order ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginOrder[]>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default);
        /// <summary>
        /// Place a new cross margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb84611-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_order
        /// </para>
        /// </summary>
        /// <param name="quantity">["<c>volume</c>"] Order quantity</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="price">["<c>price</c>"] Price</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="takeProfitTriggerPrice">["<c>tp_trigger_price</c>"] Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">["<c>tp_order_price</c>"] Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">["<c>tp_order_price_type</c>"] Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">["<c>sl_trigger_price</c>"] Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">["<c>sl_order_price</c>"] Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">["<c>sl_order_price_type</c>"] Stop loss order price type</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderIds>> PlaceCrossMarginOrderAsync(long quantity, OrderSide side, int leverageRate, OrderPriceType orderPriceType, string? contractCode = null, string? pair = null, ContractType? contractType = null, decimal? price = null, Offset? offset = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Place a new isolated margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb844bb-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="quantity">["<c>volume</c>"] Quantity</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="price">["<c>price</c>"] Price</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="takeProfitTriggerPrice">["<c>tp_trigger_price</c>"] Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">["<c>tp_order_price</c>"] Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">["<c>tp_order_price_type</c>"] Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">["<c>sl_trigger_price</c>"] Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">["<c>sl_order_price</c>"] Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">["<c>sl_order_price_type</c>"] Stop loss order price type</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderIds>> PlaceIsolatedMarginOrderAsync(string contractCode, long quantity, OrderSide side, int leverageRate, OrderPriceType orderPriceType, decimal? price = null, Offset? offset = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, bool? reduceOnly = null, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders after the timeout elapses. Can be called again to extend the timeout. Set enable to false to disable the timeout
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000068-77b7-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/linear-cancel-after
        /// </para>
        /// </summary>
        /// <param name="enable">["<c>on_off</c>"] Enabled or disable cancelation</param>
        /// <param name="timeout">["<c>time_out</c>"] The timeout after which all order are canceled</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCancelAfter>> CancelOrdersAfterAsync(bool enable, TimeSpan? timeout = null, CancellationToken ct = default);

        /// <summary>
        /// Lightning close isolated margin position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86944-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_lightning_close_position
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="direction">["<c>direction</c>"] Direction</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosePositionResult>> CloseIsolatedMarginPositionAsync(string contractCode, OrderSide direction, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Lightning close cross margin position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86944-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_lightning_close_position
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="direction">["<c>direction</c>"] Direction</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contractType</c>"] Contract type</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosePositionResult>> CloseCrossMarginPositionAsync(OrderSide direction, string? contractCode = null, string? pair = null, ContractType? contractType = null, long? clientOrderId = null, LightningPriceType? orderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new isolated margin trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86c95-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_trigger_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="triggerType">["<c>trigger_type</c>"] Trigger type</param>
        /// <param name="triggerPrice">["<c>trigger_price</c>"] Trigger price</param>
        /// <param name="quantity">["<c>volume</c>"] Order quantity</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="orderPrice">["<c>order_price</c>"] Order price</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXOrderIds>> PlaceIsolatedMarginTriggerOrderAsync(string contractCode, TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new cross margin trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86dfe-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_trigger_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="triggerType">["<c>trigger_type</c>"] Trigger type</param>
        /// <param name="triggerPrice">["<c>trigger_price</c>"] Trigger price</param>
        /// <param name="quantity">["<c>volume</c>"] Order quantity</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="orderPrice">["<c>order_price</c>"] Order price</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Order price type</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXOrderIds>> PlaceCrossMarginTriggerOrderAsync(TriggerType triggerType, decimal triggerPrice, decimal quantity, OrderSide side, string? contractCode = null, string? pair = null, ContractType? contractType = null, Offset? offset = null, bool? reduceOnly = null, decimal? orderPrice = null, OrderPriceType? orderPriceType = null, int? leverageRate = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel isolated margin trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb86f61-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_trigger_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTriggerOrderAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel cross margin trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87056-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_trigger_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTriggerOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all isolated margin trigger orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87161-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_trigger_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="side">["<c>direction</c>"] Filter by side</param>
        /// <param name="offset">["<c>offset</c>"] Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTriggerOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all cross margin trigger orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb872c3-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_trigger_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="side">["<c>direction</c>"] Filter by side</param>
        /// <param name="offset">["<c>offset</c>"] Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTriggerOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin open trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb873a8-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_trigger_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderPage>> GetIsolatedMarginOpenTriggerOrdersAsync(string contractCode, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin open trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb874fd-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_trigger_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTriggerOrderPage>> GetCrossMarginOpenTriggerOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin trigger order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87658-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_trigger_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="status">["<c>status</c>"] Status</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size, max 50</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosedTriggerOrderPage>> GetIsolatedMarginTriggerOrderHistoryAsync(string contractCode, MarginTradeType tradeType, int daysPast, OrderStatusFilter status, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin trigger order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb877ac-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_trigger_hisorders
        /// </para>
        /// </summary>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="status">["<c>status</c>"] Status</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXClosedCrossTriggerOrderPage>> GetCrossMarginTriggerOrderHistoryAsync(MarginTradeType tradeType, int daysPast, OrderStatusFilter status, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Set isolated margin order take profit / stop loss for an existing position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87911-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_tpsl_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="quantity">["<c>volume</c>"] Quantity</param>
        /// <param name="takeProfitTriggerPrice">["<c>tp_trigger_price</c>"] Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">["<c>tp_order_price</c>"] Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">["<c>tp_order_price_type</c>"] Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">["<c>sl_trigger_price</c>"] Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">["<c>sl_order_price</c>"] Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">["<c>sl_order_price_type</c>"] Stop loss order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlResult>> SetIsolatedMarginTpSlAsync(string contractCode, OrderSide side, decimal quantity, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Set cross margin order take profit / stop loss for an existing position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87a6f-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_tpsl_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="side">["<c>direction</c>"] Order side</param>
        /// <param name="quantity">["<c>volume</c>"] Quantity</param>
        /// <param name="takeProfitTriggerPrice">["<c>tp_trigger_price</c>"] Take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">["<c>tp_order_price</c>"] Take profit order price</param>
        /// <param name="takeProfitOrderPriceType">["<c>tp_order_price_type</c>"] Take profit order price type</param>
        /// <param name="stopLossTriggerPrice">["<c>sl_trigger_price</c>"] Stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">["<c>sl_order_price</c>"] Stop loss order price</param>
        /// <param name="stopLossOrderPriceType">["<c>sl_order_price_type</c>"] Stop loss order price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlResult>> SetCrossMarginTpSlAsync(OrderSide side, decimal quantity, string? contractCode = null, string? pair = null, ContractType? contractType = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderPriceType? takeProfitOrderPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderPriceType? stopLossOrderPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel isolated margin take profit / stop loss orders for an existing position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87bc0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_tpsl_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTpSlAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel cross margin take profit / stop loss orders for an existing position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87bc0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_tpsl_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTpSlAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all isolated margin take profit / stop loss orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87d94-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_tpsl_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTpSlAsync(string contractCode, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all cross margin take profit / stop loss orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87edb-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_tpsl_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTpSlAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Get open isolated margin take profit / stop loss orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb87fb0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_tpsl_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page index</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlOrderPage>> GetIsolatedMarginOpenTpSlOrdersAsync(string? contractCode = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get open cross margin take profit / stop loss orders
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page index</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTpSlOrderPage>> GetCrossMarginOpenTpSlOrdersAsync(string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, MarginTradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin take profit / stop loss order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88253-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_tpsl_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tpSlOrderStatus">["<c>status</c>"] Status</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTpSlClosedOrderPage>> GetIsolatedMarginTpSlHistoryAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross maring take profit / stop loss order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb883a0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_tpsl_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="tpSlOrderStatus">["<c>status</c>"] Status</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTpSlClosedOrderPage>> GetCrossMarginTpSlHistoryAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, int daysPast, string? contractCode = null, string? pair = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin position open info with attached tp/sl orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb884f1-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_relation_tpsl_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPositionOpenTpSlOrders>> GetIsolatedMarginPositionOpenTpSlInfoAsync(string contractCode, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin position open info with attached tp/sl orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8864d-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_relation_tpsl_order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossPositionOpenTpSlOrders>> GetCrossMarginPositionOpenTpSlInfoAsync(long orderId, string? contractCode = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Place isolated margin trailing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88960-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_track_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="quantity">["<c>volume</c>"] Quantity</param>
        /// <param name="callbackRate">["<c>callback_rate</c>"] Callback rate, 0.01 means 1%</param>
        /// <param name="activePrice">["<c>active_price</c>"] Active price</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXOrderIds>> PlaceIsolatedMarginTrailingOrderAsync(string contractCode, bool reduceOnly, OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, CancellationToken ct = default);

        /// <summary>
        /// Place cross margin trailing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88bb5-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_track_order
        /// </para>
        /// </summary>
        /// <param name="side">["<c>direction</c>"] Side</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="leverageRate">["<c>lever_rate</c>"] Leverage rate</param>
        /// <param name="quantity">["<c>volume</c>"] Quantity</param>
        /// <param name="callbackRate">["<c>callback_rate</c>"] Callback rate, 0.01 means 1%</param>
        /// <param name="activePrice">["<c>active_price</c>"] Active price</param>
        /// <param name="orderPriceType">["<c>order_price_type</c>"] Price type</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Reduce only</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderIds>> PlaceCrossMarginTrailingOrderAsync(OrderSide side, Offset offset, int leverageRate, decimal quantity, decimal callbackRate, decimal activePrice, OrderPriceType orderPriceType, string? contractCode = null, string? pair = null, ContractType? contractType = null, bool? reduceOnly = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an isolated margin trailing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb88f2c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_track_cancel
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelIsolatedMarginTrailingOrderAsync(string contractCode, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a cross margin trailing order
        /// </summary>
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelCrossMarginTrailingOrderAsync(string orderId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open isolated margin trailing orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8924f-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_track_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="side">["<c>direction</c>"] Filter by side</param>
        /// <param name="offset">["<c>offset</c>"] Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllIsolatedMarginTrailingOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open cross margin trailing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_track_cancelall
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="side">["<c>direction</c>"] Filter by side</param>
        /// <param name="offset">["<c>offset</c>"] Filter by offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTriggerOrderResult>> CancelAllCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get open isolated margin trailing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb894b7-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_track_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXTrailingOrderPage>> GetOpenIsolatedMarginTrailingOrdersAsync(string contractCode, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get open cross margin trailing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89614-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_track_openorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossTrailingOrderPage>> GetOpenCrossMarginTrailingOrdersAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, MarginTradeType? tradeType = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin trailing order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89781-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_track_hisorders
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="tpSlOrderStatus">["<c>status</c>"] Status</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTrailingClosedOrderPage>> GetClosedIsolatedMarginTrailingOrdersAsync(string contractCode, IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin trailing order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8996a-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_track_hisorders
        /// </para>
        /// </summary>
        /// <param name="tpSlOrderStatus">["<c>status</c>"] Status</param>
        /// <param name="tradeType">["<c>trade_type</c>"] Trade type</param>
        /// <param name="daysPast">["<c>create_date</c>"] Amount of days ago. Max 90</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Contract type</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort by; 'created_at' or 'update_time'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossTrailingClosedOrderPage>> GetClosedCrossMarginTrailingOrdersAsync(IEnumerable<TpSlStatus> tpSlOrderStatus, MarginTradeType tradeType, int daysPast, string? contractCode = null, string? pair = null, ContractType? contractType = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default);
    }
}
