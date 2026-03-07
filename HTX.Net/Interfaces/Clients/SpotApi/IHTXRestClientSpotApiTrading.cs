using HTX.Net.Enums;
using HTX.Net.Objects.Models;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// HTX trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IHTXRestClientSpotApiTrading
    {
        /// <summary>
        /// Places an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ee16-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/orders/place
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>account-id</c>"] The account to place the order for, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to place the order for, for example `ETHUSDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity of the order</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Should be omitted for market orders</param>
        /// <param name="clientOrderId">["<c>client-order-id</c>"] The clientOrderId the order should get</param>
        /// <param name="source">["<c>source</c>"] Source. defaults to SpotAPI</param>
        /// <param name="stopPrice">["<c>stop-price</c>"] Stop price</param>
        /// <param name="stopOperator">["<c>operator</c>"] Operator of the stop price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, SourceType? source = null, decimal? stopPrice = null, Operator? stopOperator = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4d8cc-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/batch-orders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CallResult<HTXBatchPlaceResult>[]>> PlaceMultipleOrderAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000066-77b7-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/auto/place
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>account-id</c>"] The account to place the order for, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to place the order for, for example `ETHUSDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="purpose">["<c>trade-purpose</c>"] Transaction purpose</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity of the order in base asset</param>
        /// <param name="quoteQuantity">["<c>market-amount</c>"] The quantity of the order in quote asset</param>
        /// <param name="borrowQuantity">["<c>borrow-amount</c>"] The quantity that needs to be borrowed</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Should be omitted for market orders</param>
        /// <param name="source">["<c>source</c>"] Source. defaults to SpotAPI</param>
        /// <param name="stopPrice">["<c>stop-price</c>"] Stop price</param>
        /// <param name="stopOperator">["<c>operator</c>"] Operator of the stop price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderId>> PlaceMarginOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            Enums.MarginPurpose purpose,
            SourceType source,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            decimal? borrowQuantity = null,
            decimal? price = null,
            decimal? stopPrice = null,
            Operator? stopOperator = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a list of open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4e04b-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/openOrders
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>account-id</c>"] The account id for which to get the orders for, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol for which to get the orders for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Only get buy or sell orders</param>
        /// <param name="orderTypes">["<c>types</c>"] Filter by order types</param>
        /// <param name="fromId">["<c>from</c>"] Return results after this id</param>
        /// <param name="direction">["<c>direct</c>"] Direction of results</param>
        /// <param name="limit">["<c>size</c>"] The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOpenOrder[]>> GetOpenOrdersAsync(
            long? accountId = null,
            string? symbol = null,
            OrderSide? side = null,
            IEnumerable<OrderType>? orderTypes = null,
            string? fromId = null,
            FilterDirection? direction = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4e938-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/orders/{orderId}/submitcancel
        /// </para>
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ef06-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/orders/submitCancelClientOrder
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>client-order-id</c>"] The client id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ea21-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/orders/batchcancel
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order-ids</c>"] The ids of the orders to cancel</param>
        /// <param name="clientOrderIds">["<c>client-order-ids</c>"] The client ids of the orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ef06-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/cancelAllOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, multiple symbols can be provided comma separated</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4eb66-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/order/orders/batchCancelOpenOrders
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>account-id</c>"] The account id used for this cancel</param>
        /// <param name="symbols">["<c>symbol</c>"] The trading symbol list (maximum 10 symbols, default value all symbols)</param>
        /// <param name="side">["<c>side</c>"] Filter on the direction of the trade</param>
        /// <param name="limit">["<c>size</c>"] The number of orders to cancel [1, 100]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, OrderSide? side = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4e31c-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order by client order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ec26-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/orders/getClientOrder
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] The client id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get a list of trades made for a specific order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4e708-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/orders/{orderId}/matchresults
        /// </para>
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderTrade[]>> GetOrderTradesAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get a list of orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4e1c4-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="states">["<c>states</c>"] The states of orders to return</param>
        /// <param name="types">["<c>types</c>"] The types of orders to return</param>
        /// <param name="startTime">["<c>start-time</c>"] Only get orders after this date</param>
        /// <param name="endTime">["<c>end-time</c>"] Only get orders before this date</param>
        /// <param name="fromId">["<c>from</c>"] Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">["<c>direct</c>"] Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">["<c>size</c>"] The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrder[]>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderStatus>? states = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4de21-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/matchresults
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to retrieve trades for, for example `ETHUSDT`</param>
        /// <param name="types">["<c>types</c>"] The type of orders to return</param>
        /// <param name="startTime">["<c>start-time</c>"] Only get orders after this date</param>
        /// <param name="endTime">["<c>end-time</c>"] Only get orders before this date</param>
        /// <param name="fromId">["<c>from</c>"] Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">["<c>direct</c>"] Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">["<c>size</c>"] The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderTrade[]>> GetUserTradesAsync(string? symbol = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4db3d-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/order/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>start-time</c>"] Only get orders after this date</param>
        /// <param name="endTime">["<c>end-time</c>"] Only get orders before this date</param>
        /// <param name="direction">["<c>direct</c>"] Direction of the results to return</param>
        /// <param name="limit">["<c>size</c>"] The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrder[]>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new conditional order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50918-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v2/algo-orders
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>accountId</c>"] The account the order should be placed from, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>orderSide</c>"] Side of the order</param>
        /// <param name="type">["<c>orderType</c>"] Type of the order</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Stop price of the order</param>
        /// <param name="quantity">["<c>orderSize</c>"] Quantity of the order</param>
        /// <param name="price">["<c>orderPrice</c>"] Price of the order</param>
        /// <param name="quoteQuantity">["<c>orderValue</c>"] Quote quantity of the order</param>
        /// <param name="trailingRate">["<c>trailingRate</c>"] Trailing rate of the order</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXPlacedConditionalOrder>> PlaceConditionalOrderAsync(
            long accountId,
            string symbol,
            OrderSide side,
            ConditionalOrderType type,
            decimal stopPrice,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            decimal? trailingRate = null,
            TimeInForce? timeInForce = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel conditional orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50be1-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v2/algo-orders/cancellation
        /// </para>
        /// </summary>
        /// <param name="clientOrderIds">["<c>clientOrderIds</c>"] Client order ids of the conditional orders to cancels</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default);

        /// <summary>
        /// Get open conditional orders based on the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51082-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/algo-orders/opening
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>accountId</c>"] Filter by account id, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>orderSide</c>"] Filter by side</param>
        /// <param name="type">["<c>orderType</c>"] Filter by type</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="limit">["<c>limit</c>"] Max results</param>
        /// <param name="fromId">["<c>fromId</c>"] Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXConditionalOrder[]>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed conditional orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50dcf-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/algo-orders/history
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>accountId</c>"] Filter by account id, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>orderSide</c>"] Filter by side</param>
        /// <param name="type">["<c>orderType</c>"] Filter by type</param>
        /// <param name="sort">["<c>sort</c>"] Sort direction</param>
        /// <param name="limit">["<c>limit</c>"] Max results</param>
        /// <param name="fromId">["<c>fromId</c>"] Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <param name="status">["<c>orderStatus</c>"] Filter by status</param>
        /// <param name="startTime">["<c>startTime</c>"] Return only entries after this time</param>
        /// <param name="endTime">["<c>endTime</c>"] Return only entries before this time</param>
        /// <returns></returns>
        Task<WebCallResult<HTXConditionalOrder[]>> GetClosedConditionalOrdersAsync(
            string symbol,
            ConditionalOrderStatus status,
            long? accountId = null,
            OrderSide? side = null,
            ConditionalOrderType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            string? sort = null,
            int? limit = null,
            long? fromId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a conditional order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5121b-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/algo-orders/specific
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default);
    }
}
