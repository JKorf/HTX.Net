using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects;

namespace Huobi.Net.Interfaces
{
    public interface IHuobiSocketClient
    {
        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        CallResult<HuobiSocketResponse<List<HuobiMarketData>>> QueryMarketKlines(string symbol, HuobiPeriod period);

        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketResponse<List<HuobiMarketData>>>> QueryMarketKlinesAsync(string symbol, HuobiPeriod period);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToMarketKlineUpdates(string symbol, HuobiPeriod period, Action<HuobiSocketUpdate<HuobiMarketData>> onData);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketKlineUpdatesAsync(string symbol, HuobiPeriod period, Action<HuobiSocketUpdate<HuobiMarketData>> onData);

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        CallResult<HuobiSocketResponse<HuobiMarketDepth>> QueryMarketDepth(string symbol, int mergeStep);

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketResponse<HuobiMarketDepth>>> QueryMarketDepthAsync(string symbol, int mergeStep);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToMarketDepthUpdates(string symbol, int mergeStep, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToDepthUpdatesAsync(string symbol, int mergeStep, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData);

        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>> QueryMarketTrades(string symbol);

        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>> QueryMarketTradesAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToMarketTradeUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketTradeUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData);

        /// <summary>
        /// Gets details for a market
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        CallResult<HuobiSocketResponse<HuobiMarketData>> QueryMarketDetails(string symbol);

        /// <summary>
        /// Gets details for a market
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketResponse<HuobiMarketData>>> QueryMarketDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to market detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToMarketDetailUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData);

        /// <summary>
        /// Subscribes to market detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketDetailUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData);

        /// <summary>
        /// Subscribes to updates for all market tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToMarketTickerUpdates(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData);

        /// <summary>
        /// Subscribes to updates for all market tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketTickerUpdatesAsync(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData);

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>> QueryAccounts();

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        Task<CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>> QueryAccountsAsync();

        /// <summary>
        /// Subscribe to account/wallet updates
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToAccountUpdates(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData);

        /// <summary>
        /// Subscribe to account/wallet updates
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData);

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="accountId">The account id to get orders for</param>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        CallResult<HuobiSocketAuthDataResponse<List<HuobiOrder>>> QueryOrders(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="accountId">The account id to get orders for</param>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketAuthDataResponse<List<HuobiOrder>>>> QueryOrdersAsync(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// Subscribe to updates when any order changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToOrderUpdates(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData);

        /// <summary>
        /// Subscribe to updates when any order changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData);

        /// <summary>
        /// Subscribe to updates when a order for a symbol changes
        /// </summary> 
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToOrderUpdates(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData);

        /// <summary>
        /// Subscribe to updates when a order for a symbol changes
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData);

        /// <summary>
        /// Gets data for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        CallResult<HuobiSocketAuthDataResponse<HuobiOrder>> QueryOrderDetails(long orderId);

        /// <summary>
        /// Gets data for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        Task<CallResult<HuobiSocketAuthDataResponse<HuobiOrder>>> QueryOrderDetailsAsync(long orderId);

        /// <summary>
        /// The factory for creating sockets. Used for unit testing
        /// </summary>
        IWebsocketFactory SocketFactory { get; set; }

        /// <summary>
        /// Unsubscribe from a stream
        /// </summary>
        /// <param name="subscription">The subscription to unsubscribe</param>
        /// <returns></returns>
        Task Unsubscribe(UpdateSubscription subscription);

        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        /// <returns></returns>
        Task UnsubscribeAll();

        void Dispose();
    }
}