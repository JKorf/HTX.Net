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
        Task<HttpResult<HTXOrderIdV5>> PlaceOrderAsync(string contractCode, MarginMode marginMode, OrderSide side, OrderTypeV5 type, decimal quantity, FuturesPositionSide? positionSide = null, decimal? price = null, PriceMatch? priceMatch = null, string? clientOrderId = null, bool? reduceOnly = null, TimeInForce? timeInForce = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, bool? priceProtect = null, SelfMatchPrevent? selfMatchPrevent = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1958947efe6" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/cancel_order
        /// </para>
        /// </summary>
        Task<HttpResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-196a8401f83" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/order
        /// </para>
        /// </summary>
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
        Task<HttpResult<HTXOrderV5[]>> GetOrderHistoryAsync(string contractCode, MarginMode marginMode, IEnumerable<OrderStatusV5>? states = null, OrderTypeV5? type = null, PriceMatch? priceMatch = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Place algo order
        /// </summary>
        Task<HttpResult<HTXAlgoOrderIdV5>> PlaceAlgoOrderAsync(string contractCode, AlgoOrderType type, FuturesPositionSide positionSide, OrderSide side, MarginMode marginMode, string? algoClientOrderId = null, decimal? quantity = null, decimal? takeProfitTriggerPrice = null, decimal? takeProfitOrderPrice = null, OrderTypeV5? takeProfitType = null, TriggerPriceType? takeProfitTriggerPriceType = null, decimal? stopLossTriggerPrice = null, decimal? stopLossOrderPrice = null, OrderTypeV5? stopLossType = null, TriggerPriceType? stopLossTriggerPriceType = null, decimal? price = null, PriceMatch? priceType = null, decimal? triggerPrice = null, TriggerPriceType? triggerPriceType = null, decimal? activationPrice = null, PriceMatch? orderPriceType = null, decimal? callbackRate = null, bool? reduceOnly = null, CancellationToken ct = default);
        /// <summary>
        /// Get open algo orders
        /// </summary>
        Task<HttpResult<HTXAlgoOrderV5[]>> GetOpenAlgoOrdersAsync(AlgoOrderType type, string? contractCode = null, string? algoId = null, string? algoClientOrderId = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Get algo order history
        /// </summary>
        Task<HttpResult<HTXAlgoOrderV5[]>> GetAlgoOrderHistoryAsync(AlgoOrderType type, string? contractCode = null, MarginMode? marginMode = null, IEnumerable<AlgoOrderStatus>? states = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel algo orders
        /// </summary>
        Task<HttpResult<HTXAlgoOrderIdV5>> CancelAlgoOrdersAsync(string contractCode, string? algoId = null, string? algoClientOrderId = null, CancellationToken ct = default);
    }
}
