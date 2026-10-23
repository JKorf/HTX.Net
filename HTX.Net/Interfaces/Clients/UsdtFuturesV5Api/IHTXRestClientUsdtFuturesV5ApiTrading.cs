using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// HTX usdt futures V5 trading endpoints, placing and managing orders.
    /// </summary>
    public interface IHTXRestClientUsdtFuturesV5ApiTrading
    {
        /// <summary>
        /// Place an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19588768fe7" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/order
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="side">The order side</param>
        /// <param name="type">The order type</param>
        /// <param name="quantity">The order quantity</param>
        /// <param name="positionSide">The position side</param>
        /// <param name="price">The order price</param>
        /// <param name="priceMatch">The price match option</param>
        /// <param name="clientOrderId">The client order ID</param>
        /// <param name="reduceOnly">Whether the order is reduce only</param>
        /// <param name="timeInForce">The time in force</param>
        /// <param name="takeProfitTriggerPrice">The take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">The take profit order price</param>
        /// <param name="takeProfitType">The take profit order type</param>
        /// <param name="takeProfitTriggerPriceType">The take profit trigger price type</param>
        /// <param name="stopLossTriggerPrice">The stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">The stop loss order price</param>
        /// <param name="stopLossType">The stop loss order type</param>
        /// <param name="stopLossTriggerPriceType">The stop loss trigger price type</param>
        /// <param name="priceProtect">Whether to enable price protection</param>
        /// <param name="selfMatchPrevent">The self match prevent option</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderIdV5>> PlaceOrderAsync(
            string contractCode,
            MarginMode marginMode, 
            OrderSide side, 
            OrderTypeV5 type,
            decimal quantity, 
            FuturesPositionSide? positionSide = null,
            decimal? price = null, 
            PriceMatch? priceMatch = null,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            TimeInForce? timeInForce = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            OrderTypeV5? takeProfitType = null,
            TriggerPriceType? takeProfitTriggerPriceType = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            OrderTypeV5? stopLossType = null,
            TriggerPriceType? stopLossTriggerPriceType = null,
            bool? priceProtect = null,
            SelfMatchPrevent? selfMatchPrevent = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1958947efe6" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/cancel_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="orderId">The order ID</param>
        /// <param name="clientOrderId">The client order ID</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the filters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-195894f0cf6" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/cancel_all_orders
        /// </para>
        /// <param name="contractCode">Filter by symbol</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="positionSide">Filter by position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXOrderResultV5[]>> CancelAllOrdersAsync(string? contractCode = null, OrderSide? side = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-196a8401f83" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/order
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="orderId">The order ID</param>
        /// <param name="clientOrderId">The client order ID</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderV5>> GetOrderAsync(string contractCode, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get current orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19589587da5" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/order/opens
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="orderId">The order ID</param>
        /// <param name="clientOrderId">The client order ID</param>
        /// <param name="fromId">The from ID</param>
        /// <param name="limit">The limit</param>
        /// <param name="direction">The filter direction</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderV5[]>> GetOpenOrdersAsync(string? contractCode = null, MarginMode? marginMode = null, string? orderId = null, string? clientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Get execution details
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-195898804f0" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/order/details
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="orderId">The order ID</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="fromId">The from ID</param>
        /// <param name="limit">The limit</param>
        /// <param name="direction">The filter direction</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderTradeV5[]>> GetOrderDetailsAsync(string contractCode, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19589bc57bc" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/order/history
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="states">The order states</param>
        /// <param name="type">The order type</param>
        /// <param name="priceMatch">The price match</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="fromId">The from ID</param>
        /// <param name="limit">The limit</param>
        /// <param name="direction">The filter direction</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXOrderV5[]>> GetOrderHistoryAsync(string contractCode, MarginMode marginMode, IEnumerable<OrderStatusV5>? states = null, OrderTypeV5? type = null, PriceMatch? priceMatch = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Place algo order
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="type">The algo order type</param>
        /// <param name="positionSide">The position side</param>
        /// <param name="side">The order side</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="algoClientOrderId">The client order ID</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="takeProfitTriggerPrice">The take profit trigger price</param>
        /// <param name="takeProfitOrderPrice">The take profit order price</param>
        /// <param name="takeProfitType">The take profit type</param>
        /// <param name="takeProfitTriggerPriceType">The take profit trigger price type</param>
        /// <param name="stopLossTriggerPrice">The stop loss trigger price</param>
        /// <param name="stopLossOrderPrice">The stop loss order price</param>
        /// <param name="stopLossType">The stop loss type</param>
        /// <param name="stopLossTriggerPriceType">The stop loss trigger price type</param>
        /// <param name="price">The price</param>
        /// <param name="priceType">The price type</param>
        /// <param name="triggerPrice">The trigger price</param>
        /// <param name="triggerPriceType">The trigger price type</param>
        /// <param name="activationPrice">The activation price</param>
        /// <param name="orderPriceType">The order price type</param>
        /// <param name="callbackRate">The callback rate</param>
        /// <param name="reduceOnly">Whether to reduce only</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXAlgoOrderIdV5>> PlaceAlgoOrderAsync(string contractCode, AlgoOrderType type, FuturesPositionSide positionSide, OrderSide side, MarginMode marginMode, string? algoClientOrderId = null, decimal? quantity = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, decimal? price = null, PriceMatch? priceType = null, decimal? triggerPrice = null, TriggerPriceType? triggerPriceType = null, decimal? activationPrice = null, PriceMatch? orderPriceType = null, decimal? callbackRate = null, bool? reduceOnly = null, CancellationToken ct = default);
        /// <summary>
        /// Get open algo orders
        /// </summary>
        /// <param name="type">The algo order type</param>
        /// <param name="contractCode">The symbol</param>
        /// <param name="algoId">The algo ID</param>
        /// <param name="algoClientOrderId">The client order ID</param>
        /// <param name="fromId">The from ID</param>
        /// <param name="limit">The limit</param>
        /// <param name="direction">The filter direction</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXAlgoOrderV5[]>> GetOpenAlgoOrdersAsync(AlgoOrderType type, string? contractCode = null, string? algoId = null, string? algoClientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Get algo order history
        /// </summary>
        /// <param name="type">The algo order type</param>
        /// <param name="contractCode">The symbol</param>
        /// <param name="marginMode">The margin mode</param>
        /// <param name="states">The order states</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="fromId">The from ID</param>
        /// <param name="limit">The limit</param>
        /// <param name="direction">The filter direction</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXAlgoOrderV5[]>> GetAlgoOrderHistoryAsync(AlgoOrderType type, string? contractCode = null, MarginMode? marginMode = null, IEnumerable<AlgoOrderStatus>? states = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel algo orders
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="algoId">The algo ID</param>
        /// <param name="algoClientOrderId">The client order ID</param>
        /// <param name="ct">The cancellation token</param>
        Task<HttpResult<HTXAlgoOrderIdV5>> CancelAlgoOrdersAsync(string contractCode, string? algoId = null, string? algoClientOrderId = null, CancellationToken ct = default);
    }
}
