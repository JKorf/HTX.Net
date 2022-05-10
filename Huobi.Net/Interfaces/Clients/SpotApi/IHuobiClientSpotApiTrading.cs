using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Huobi trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IHuobiClientSpotApiTrading
    {
        /// <summary>
        /// Places an order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#place-a-new-order" /></para>
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <param name="clientOrderId">The clientOrderId the order should get</param>
        /// <param name="source">Source. defaults to SpotAPI</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="stopOperator">Operator of the stop price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, SourceType? source = null, decimal? stopPrice = null, Operator? stopOperator = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-open-orders" /></para>
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels an open order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancels an open order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order-based-on-client-order-id" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-ids" /></para>
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <param name="clientOrderIds">The client ids of the orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-criteria" /></para>
        /// </summary>
        /// <param name="accountId">The account id used for this cancel</param>
        /// <param name="symbols">The trading symbol list (maximum 10 symbols, default value all symbols)</param>
        /// <param name="side">Filter on the direction of the trade</param>
        /// <param name="limit">The number of orders to cancel [1, 100]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, OrderSide? side = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order by client order id
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order-based-on-client-order-id" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades made for a specific order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-match-result-of-an-order" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrder>>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderState>? states = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades for a specific symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-match-results" /></para>
        /// </summary>
        /// <param name="states">Only return trades with specific states</param>
        /// <param name="symbol">The symbol to retrieve trades for</param>
        /// <param name="types">The type of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetUserTradesAsync(IEnumerable<OrderState>? states = null, string? symbol = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of history orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-historical-orders-within-48-hours" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="direction">Direction of the results to return</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrder>>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new conditional order
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#place-a-conditional-order" /></para>
        /// </summary>
        /// <param name="accountId">The account the order should be placed from</param>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of the order</param>
        /// <param name="stopPrice">Stop price of the order</param>
        /// <param name="quantity">Quantity of the order</param>
        /// <param name="price">Price of the order</param>
        /// <param name="quoteQuantity">Quote quantity of the order</param>
        /// <param name="trailingRate">Trailing rate of the order</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPlacedConditionalOrder>> PlaceConditionalOrderAsync(
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
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#cancel-conditional-orders-before-triggering" /></para>
        /// </summary>
        /// <param name="clientOrderIds">Client order ids of the conditional orders to cancels</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default);

        /// <summary>
        /// Get open conditional orders based on the parameters
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-open-conditional-orders-before-triggering" /></para>
        /// </summary>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max results</param>
        /// <param name="fromId">Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed conditional orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-conditional-order-history" /></para>
        /// </summary>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max results</param>
        /// <param name="fromId">Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Return only entries after this time</param>
        /// <param name="endTime">Return only entries before this time</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetClosedConditionalOrdersAsync(
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
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-a-specific-conditional-order" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default);
    }
}
