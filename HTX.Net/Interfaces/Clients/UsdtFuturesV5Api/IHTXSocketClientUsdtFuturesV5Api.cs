using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// Usdt futures V5 streams
    /// </summary>
    public interface IHTXSocketClientUsdtFuturesV5Api : ISocketApiClient<HTXCredentials>
    {
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IHTXSocketClientUsdtFuturesV5SharedApi SharedApi { get; }

        /// <summary>
        /// Subscribe to account updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXDataEventV5<HTXAccountUpdateV5>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXOrderUpdateV5>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to position updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXPositionUpdateV5[]>>> onData, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to user trade updates
        /// </summary>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? contractCode, Action<DataEvent<HTXDataEventV5<HTXMatchOrderUpdateV5[]>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Place an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-196a9995f84" /><br />
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
        Task<QueryResult<HTXOrderIdV5>> PlaceOrderAsync(
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
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-196a9c5cdbb" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/cancel_order
        /// </para>
        /// </summary>
        /// <param name="contractCode">The symbol</param>
        /// <param name="orderId">The order ID, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">The client order ID, either this or orderId should be provided</param>
        /// <param name="ct">The cancellation token</param>
        Task<QueryResult<HTXOrderIdV5>> CancelOrderAsync(string contractCode, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the filters
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-196a9df9306" /><br />
        /// Endpoint:<br />
        /// POST /v5/trade/cancel_all_orders
        /// </para>
        /// <param name="contractCode">Filter by symbol</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="positionSide">Filter by position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult<HTXOrderResultV5[]>> CancelAllOrdersAsync(string? contractCode = null, OrderSide? side = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default);
    }
}
