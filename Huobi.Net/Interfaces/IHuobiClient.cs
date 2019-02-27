using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiter;
using Huobi.Net.Objects;

namespace Huobi.Net.Interfaces
{
    public interface IHuobiClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Gets the latest ticker for all markets
        /// </summary>
        /// <returns></returns>
        WebCallResult<HuobiMarketTicks> GetMarketTickers();

        /// <summary>
        /// Gets the latest ticker for all markets
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketTicks>> GetMarketTickersAsync();

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <returns></returns>
        WebCallResult<HuobiMarketTickMerged> GetMarketTickerMerged(string symbol);

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketTickMerged>> GetMarketTickerMergedAsync(string symbol);

        /// <summary>
        /// Get candlestick data for a market
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="size">The amount of candlesticks</param>
        /// <returns></returns>
        WebCallResult<List<HuobiMarketKline>> GetMarketKlines(string symbol, HuobiPeriod period, int size);

        /// <summary>
        /// Get candlestick data for a market
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="size">The amount of candlesticks</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiMarketKline>>> GetMarketKlinesAsync(string symbol, HuobiPeriod period, int size);

        /// <summary>
        /// Gets the market depth for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        WebCallResult<HuobiMarketDepth> GetMarketDepth(string symbol, int mergeStep);

        /// <summary>
        /// Gets the market depth for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketDepth>> GetMarketDepthAsync(string symbol, int mergeStep);

        /// <summary>
        /// Gets the last trade for a market
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <returns></returns>
        WebCallResult<HuobiMarketTrade> GetMarketLastTrade(string symbol);

        /// <summary>
        /// Gets the last trade for a market
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketTrade>> GetMarketLastTradeAsync(string symbol);

        /// <summary>
        /// Get the last x trades for a market
        /// </summary>
        /// <param name="symbol">The market to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        WebCallResult<List<HuobiMarketTrade>> GetMarketTradeHistory(string symbol, int limit);

        /// <summary>
        /// Get the last x trades for a market
        /// </summary>
        /// <param name="symbol">The market to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiMarketTrade>>> GetMarketTradeHistoryAsync(string symbol, int limit);

        /// <summary>
        /// Gets 24h stats for a market
        /// </summary>
        /// <param name="symbol">The market to get the data for</param>
        /// <returns></returns>
        WebCallResult<HuobiMarketDetails> GetMarketDetails24H(string symbol);

        /// <summary>
        /// Gets 24h stats for a market
        /// </summary>
        /// <param name="symbol">The market to get the data for</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketDetails>> GetMarketDetails24HAsync(string symbol);

        /// <summary>
        /// Gets a list of supported symbols
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<HuobiSymbol>> GetSymbols();

        /// <summary>
        /// Gets a list of supported symbols
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiSymbol>>> GetSymbolsAsync();

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<string>> GetCurrencies();

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<string>>> GetCurrenciesAsync();

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns></returns>
        WebCallResult<DateTime> GetServerTime();

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync();

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<HuobiAccount>> GetAccounts();

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiAccount>>> GetAccountsAsync();

        /// <summary>
        /// Gets a list of balances for a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <returns></returns>
        WebCallResult<List<HuobiBalance>> GetBalances(long accountId);

        /// <summary>
        /// Gets a list of balances for a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiBalance>>> GetBalancesAsync(long accountId);

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <returns></returns>
        WebCallResult<List<HuobiBalance>> GetSubAccountBalances(long subAccountId);

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId);

        /// <summary>
        /// Transfer asset between parent and sub account
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="currency">The crypto currency to transfer</param>
        /// <param name="amount">The amount of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <returns>Unique transfer id</returns>
        WebCallResult<long> TransferWithSubAccount(long subAccountId, string currency, decimal amount, HuobiTransferType transferType);

        /// <summary>
        /// Transfer asset between parent and sub account
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="currency">The crypto currency to transfer</param>
        /// <param name="amount">The amount of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <returns>Unique transfer id</returns>
        Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string currency, decimal amount, HuobiTransferType transferType);

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="amount">The amount of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <returns></returns>
        WebCallResult<long> PlaceOrder(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null);

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="amount">The amount of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null);

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        WebCallResult<List<HuobiOpenOrder>> GetOpenOrders(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null);

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null);

        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns></returns>
        WebCallResult<long> CancelOrder(long orderId);

        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderAsync(long orderId);

        /// <summary>
        /// Cancel multiple open orders
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <returns></returns>
        WebCallResult<HuobiBatchCancelResult> CancelOrders(IEnumerable<long> orderIds);

        /// <summary>
        /// Cancel multiple open orders
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long> orderIds);

        /// <summary>
        /// Get details of an order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        WebCallResult<HuobiOrder> GetOrderInfo(long orderId);

        /// <summary>
        /// Get details of an order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrder>> GetOrderInfoAsync(long orderId);

        /// <summary>
        /// Gets a list of trades made for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <returns></returns>
        WebCallResult<List<HuobiOrderTrade>> GetOrderTrades(long orderId);

        /// <summary>
        /// Gets a list of trades made for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId);

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        WebCallResult<List<HuobiOrder>> GetOrders(string symbol, IEnumerable<HuobiOrderState> states, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiOrder>>> GetOrdersAsync(string symbol, IEnumerable<HuobiOrderState> states, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// Gets a list of trades for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to retrieve trades for</param>
        /// <param name="types">The type of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        WebCallResult<List<HuobiOrderTrade>> GetSymbolTrades(string symbol, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// Gets a list of trades for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to retrieve trades for</param>
        /// <param name="types">The type of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        Task<WebCallResult<List<HuobiOrderTrade>>> GetSymbolTradesAsync(string symbol, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null);

        /// <summary>
        /// The factory for creating requests. Used for unit testing
        /// </summary>
        IRequestFactory RequestFactory { get; set; }

        RateLimitingBehaviour RateLimitBehaviour { get; }
        IEnumerable<IRateLimiter> RateLimiters { get; }
        int TotalRequestsMade { get; }
        string BaseAddress { get; }

        /// <summary>
        /// Adds a rate limiter to the client. There are 2 choices, the <see cref="RateLimiterTotal"/> and the <see cref="RateLimiterPerEndpoint"/>.
        /// </summary>
        /// <param name="limiter">The limiter to add</param>
        void AddRateLimiter(IRateLimiter limiter);

        /// <summary>
        /// Removes all rate limiters from this client
        /// </summary>
        void RemoveRateLimiters();

        /// <summary>
        /// Ping to see if the server is reachable
        /// </summary>
        /// <returns>The roundtrip time of the ping request</returns>
        CallResult<long> Ping();

        /// <summary>
        /// Ping to see if the server is reachable
        /// </summary>
        /// <returns>The roundtrip time of the ping request</returns>
        Task<CallResult<long>> PingAsync();

        void Dispose();
    }
}