using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects.V2;

namespace Huobi.Net.Interfaces
{
    /// <summary>
    /// Interface for the Huobi socket client
    /// </summary>
    public interface IHuobiSocketClient: ISocketClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, HuobiPeriod period);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, HuobiPeriod period, Action<DataEvent<HuobiKline>> onData);

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="levels">The amount of rows. 5, 20, 150 or 400</param>
        /// <returns></returns>
        Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 10 or 20</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData);

        /// <summary>
        /// Subscribes to order book updates for a symbol, 
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 20, 150 or 400</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData);        

        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData);

        /// <summary>
        /// Gets details for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData);

        /// <summary>
        /// Subscribes to updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData);

        /// <summary>
        /// Subscribes to updates for all tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData);

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onData">Data handler</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol,
            Action<DataEvent<HuobiBestOffer>> onData);

        /// <summary>
        /// Subscribe to updates of orders
        /// </summary>
        /// <param name="symbol">Subscribe on a specific symbol</param>
        /// <param name="onOrderSubmitted">Event handler for the order submitted event</param>
        /// <param name="onOrderMatched">Event handler for the order matched event</param>
        /// <param name="onOrderCancellation">Event handler for the order cancelled event</param>
        /// <param name="onConditionalOrderTriggerFailure">Event handler for the conditional order trigger failed event</param>
        /// <param name="onConditionalOrderCancelled">Event handler for the condition order cancelled event</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HuobiCancelledOrderUpdate>>? onOrderCancellation = null,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<Objects.SocketObjects.V2.HuobiOrderUpdate>>? onConditionalOrderCancelled = null);

        /// <summary>
        /// Subscribe to updates of account balances
        /// </summary>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate);

        /// <summary>
        /// Subscribe to detailed order matched/cancelled updates
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order cancelled event</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null,
            Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancellationUpdate>>? onOrderCancel = null);

    }
}