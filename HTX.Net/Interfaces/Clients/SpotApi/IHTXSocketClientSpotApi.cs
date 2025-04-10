using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot streams
    /// </summary>
    public interface IHTXSocketClientSpotApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IHTXSocketClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Get kline/candlestick data for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<HTXKline[]>> GetKlinesAsync(string symbol, KlineInterval period);

        /// <summary>
        /// Subscribes to kline/candlestick updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53241-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HTXOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);

        /// <summary>
        /// Get the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="levels">The amount of rows. 5, 20, 150 or 400</param>
        /// <returns></returns>
        Task<CallResult<HTXIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5378b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="levels">The number of price levels. 5, 10 or 20</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MillisecondAsync(string symbol, int levels, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5342e-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol, 
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5362b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="levels">The number of price levels. 5, 20, 150 or 400</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HTXIncementalOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Get a list of trades for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for, for example `ETHUSDT`</param>
        /// <returns></returns>
        Task<CallResult<HTXSymbolTradeDetails[]>> GetTradeHistoryAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53b69-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTrade>> onData, CancellationToken ct = default);

        /// <summary>
        /// Get details for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get data for, for example `ETHUSDT`</param>
        /// <returns></returns>
        Task<CallResult<HTXSymbolDetails>> GetSymbolDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53561-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolDetails>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec538cf-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `ETHUSDT`</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for all tickers
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec538cf-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HTXSymbolTicker[]>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5333f-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<HTXBestOffer>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53c8f-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Subscribe on a specific symbol</param>
        /// <param name="onOrderSubmitted">Event handler for the order submitted event</param>
        /// <param name="onOrderMatched">Event handler for the order matched event</param>
        /// <param name="onOrderCancelation">Event handler for the order cancelled event</param>
        /// <param name="onConditionalOrderTriggerFailure">Event handler for the conditional order trigger failed event</param>
        /// <param name="onConditionalOrderCanceled">Event handler for the condition order canceled event</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HTXSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HTXMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HTXCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HTXTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HTXOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of account balances
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52e28-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="onAccountUpdate">Event handler</param>
        /// <param name="updateMode">The update mode. Defaults to 1, see API docs for more info</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HTXAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to detailed order matched/canceled updates
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53dd5-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol, for example `ETHUSDT`</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order canceled event</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null,
            Action<DataEvent<HTXTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1928f079ab6" /></para>
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="symbol"></param>
        /// <param name="side"></param>
        /// <param name="type"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="clientOrderId"></param>
        /// <param name="source"></param>
        /// <param name="stopPrice"></param>
        /// <param name="stopOperator"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<string>> PlaceOrderAsync(
            long accountId,
            string symbol,
            Enums.OrderSide side,
            Enums.OrderType type,
            decimal quantity,
            decimal? price = null,
            string? clientOrderId = null,
            SourceType? source = null,
            decimal? stopPrice = null,
            Operator? stopOperator = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call. Make sure to check each order response to see if placement succeeded.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1928f115372" /></para>
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<CallResult<HTXBatchPlaceResult>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<HTXOrderRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new margin order
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1928f216d9e" /></para>
        /// </summary>
        /// <param name="accountId">The account to place the order for, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbol">The symbol to place the order for, for example `ETHUSDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="purpose">Transaction purpose</param>
        /// <param name="quantity">The quantity of the order in base asset</param>
        /// <param name="quoteQuantity">The quantity of the order in quote asset</param>
        /// <param name="borrowQuantity">The quantity that needs to be borrowed</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <param name="source">Source</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="stopOperator">Operator of the stop price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<HTXOrderId>> PlaceMarginOrderAsync(
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
        /// Cancel all orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1928f2962a5" /></para>
        /// </summary>
        /// <param name="accountId">The account to place the order for, account ids can be retrieved with <see cref="IHTXRestClientSpotApiAccount.GetAccountsAsync">SpotApi.Account.GetAccountsAsync</see>.</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<HTXByCriteriaCancelResult>> CancelAllOrdersAsync(
           long accountId,
           IEnumerable<string>? symbols = null,
           CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1928f2fa07f" /></para>
        /// </summary>
        /// <param name="orderIds">Order ids to cancel</param>
        /// <param name="clientOrderIds">Client order ids to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<HTXBatchCancelResult>> CancelOrdersAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? clientOrderIds = null,
            CancellationToken ct = default);
    }
}
