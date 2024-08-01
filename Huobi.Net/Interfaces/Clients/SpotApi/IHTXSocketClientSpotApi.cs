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
        /// Gets candlestick data for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string symbol, KlineInterval period);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53241-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HTXKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HTXOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);

        /// <summary>
        /// Gets the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="levels">The amount of rows. 5, 20, 150 or 400</param>
        /// <returns></returns>
        Task<CallResult<HTXIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5378b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 10 or 20</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5342e-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HTXOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol, 
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5362b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 20, 150 or 400</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HTXIncementalOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HTXSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53b69-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolTrade>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets details for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        Task<CallResult<HTXSymbolDetails>> GetSymbolDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec53561-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HTXSymbolDetails>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec538cf-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
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
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<IEnumerable<HTXSymbolTicker>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5333f-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
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
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order canceled event</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null,
            Action<DataEvent<HTXTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default);
    }
}