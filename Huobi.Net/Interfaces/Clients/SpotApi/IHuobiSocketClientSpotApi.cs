using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot streams
    /// </summary>
    public interface IHuobiSocketClientSpotApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Gets candlestick data for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);

        /// <summary>
        /// Gets the current order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="levels">The amount of rows. 5, 20, 150 or 400</param>
        /// <returns></returns>
        Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 10 or 20</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol, 
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 20, 150 or 400</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets details for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for all tickers
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-ticker" /></para>
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#best-bid-offer" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol,
            Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of orders
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-order-updates" /></para>
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
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of account balances
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-account-change" /></para>
        /// </summary>
        /// <param name="onAccountUpdate">Event handler</param>
        /// <param name="updateMode">The update mode. Defaults to 1, see API docs for more info</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to detailed order matched/canceled updates
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-trade-details-amp-order-cancellation-post-clearing" /></para>
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order canceled event</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null,
            Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default);

    }
}