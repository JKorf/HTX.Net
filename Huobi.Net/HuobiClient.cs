using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Interfaces;
using Huobi.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Huobi.Net
{
    public class HuobiClient: RestClient, IHuobiClient
    {
        #region fields
        private static HuobiClientOptions defaultOptions = new HuobiClientOptions();
        private static HuobiClientOptions DefaultOptions => defaultOptions.Copy<HuobiClientOptions>();


        private const string MarketTickerEndpoint = "market/tickers";
        private const string MarketTickerMergedEndpoint = "market/detail/merged";
        private const string MarketKlineEndpoint = "market/history/kline";
        private const string MarketDepthEndpoint = "market/depth";
        private const string MarketLastTradeEndpoint = "market/trade";
        private const string MarketTradeHistoryEndpoint = "market/history/trade";
        private const string MarketDetailsEndpoint = "market/detail";

        private const string CommonSymbolsEndpoint = "common/symbols";
        private const string CommonCurrenciesEndpoint = "common/currencys";
        private const string ServerTimeEndpoint = "common/timestamp";

        private const string GetAccountsEndpoint = "account/accounts";
        private const string GetBalancesEndpoint = "account/accounts/{}/balance";

        private const string GetSubAccountBalancesEndpoint = "account/accounts/{}";
        private const string TransferWithSubAccountEndpoint = "subuser/transfer";

        private const string PlaceOrderEndpoint = "order/orders/place";
        private const string OpenOrdersEndpoint = "order/openOrders";
        private const string OrdersEndpoint = "order/orders";
        private const string CancelOrderEndpoint = "order/orders/{}/submitcancel";
        private const string CancelOrdersEndpoint = "order/orders/batchcancel";
        private const string OrderInfoEndpoint = "order/orders/{}";
        private const string OrderTradesEndpoint = "order/orders/{}/matchresults";
        private const string SymbolTradesEndpoint = "order/matchresults";

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of HuobiClient using the default options
        /// </summary>
        public HuobiClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiClient with the provided options
        /// </summary>
        public HuobiClient(HuobiClientOptions options) : base(options, options.ApiCredentials == null ? null : new HuobiAuthenticationProvider(options.ApiCredentials))
        {
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(HuobiClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new HuobiAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

        /// <summary>
        /// Gets the latest ticker for all markets
        /// </summary>
        /// <returns></returns>
        public WebCallResult<HuobiMarketTicks> GetMarketTickers() => GetMarketTickersAsync().Result;
        /// <summary>
        /// Gets the latest ticker for all markets
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiMarketTicks>> GetMarketTickersAsync()
        {
            var result = await ExecuteRequest<HuobiTimestampResponse<List<HuobiMarketTick>>>(GetUrl(MarketTickerEndpoint)).ConfigureAwait(false);     
            if(!result.Success)
                return new WebCallResult<HuobiMarketTicks>(result.ResponseStatusCode, null, result.Error);

            return new WebCallResult<HuobiMarketTicks>(result.ResponseStatusCode, new HuobiMarketTicks(){ Ticks = result.Data.Data, Timestamp = result.Data.Timestamp}, null);
        }

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <returns></returns>
        public WebCallResult<HuobiMarketTickMerged> GetMarketTickerMerged(string symbol) => GetMarketTickerMergedAsync(symbol).Result;

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiMarketTickMerged>> GetMarketTickerMergedAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<HuobiMarketTickMerged>>(GetUrl(MarketTickerMergedEndpoint), parameters: parameters, checkResult:false).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<HuobiMarketTickMerged>(result.ResponseStatusCode, null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new WebCallResult<HuobiMarketTickMerged>(result.ResponseStatusCode, result.Data.Data, null);
        }

        /// <summary>
        /// Get candlestick data for a market
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="size">The amount of candlesticks</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiMarketKline>> GetMarketKlines(string symbol, HuobiPeriod period, int size) => GetMarketKlinesAsync(symbol, period, size).Result;

        /// <summary>
        /// Get candlestick data for a market
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="size">The amount of candlesticks</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiMarketKline>>> GetMarketKlinesAsync(string symbol, HuobiPeriod period, int size)
        {
            if (size <= 0 || size > 2000)
                return new WebCallResult<List<HuobiMarketKline>>(null, null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
                { "size", size }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<List<HuobiMarketKline>>>(GetUrl(MarketKlineEndpoint), parameters: parameters).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<List<HuobiMarketKline>>(result.ResponseStatusCode, null, result.Error);

            return new WebCallResult<List<HuobiMarketKline>>(result.ResponseStatusCode, result.Data.Data, null);
        }

        /// <summary>
        /// Gets the market depth for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public WebCallResult<HuobiMarketDepth> GetMarketDepth(string symbol, int mergeStep) => GetMarketDepthAsync(symbol, mergeStep).Result;
        /// <summary>
        /// Gets the market depth for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiMarketDepth>> GetMarketDepthAsync(string symbol, int mergeStep)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new WebCallResult<HuobiMarketDepth>(null, null, new ArgumentError("MergeStep should be between 0 and 5"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", "step"+mergeStep }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<HuobiMarketDepth>>(GetUrl(MarketDepthEndpoint), parameters: parameters, checkResult: false).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<HuobiMarketDepth>(result.ResponseStatusCode, null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new WebCallResult<HuobiMarketDepth>(result.ResponseStatusCode, result.Data.Data, null);
        }

        /// <summary>
        /// Gets the last trade for a market
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <returns></returns>
        public WebCallResult<HuobiMarketTrade> GetMarketLastTrade(string symbol) => GetMarketLastTradeAsync(symbol).Result;
        /// <summary>
        /// Gets the last trade for a market
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiMarketTrade>> GetMarketLastTradeAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<HuobiMarketTrade>>(GetUrl(MarketLastTradeEndpoint), parameters: parameters, checkResult: false).ConfigureAwait(false);
            return new WebCallResult<HuobiMarketTrade>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Get the last x trades for a market
        /// </summary>
        /// <param name="symbol">The market to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiMarketTrade>> GetMarketTradeHistory(string symbol, int limit) => GetMarketTradeHistoryAsync(symbol, limit).Result;
        /// <summary>
        /// Get the last x trades for a market
        /// </summary>
        /// <param name="symbol">The market to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiMarketTrade>>> GetMarketTradeHistoryAsync(string symbol, int limit)
        {
            if (limit <= 0 || limit > 2000)
                return new WebCallResult<List<HuobiMarketTrade>>(null, null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "size", limit }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<List<HuobiMarketTrade>>>(GetUrl(MarketTradeHistoryEndpoint), parameters: parameters).ConfigureAwait(false);
            return new WebCallResult<List<HuobiMarketTrade>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Gets 24h stats for a market
        /// </summary>
        /// <param name="symbol">The market to get the data for</param>
        /// <returns></returns>
        public WebCallResult<HuobiMarketDetails> GetMarketDetails24H(string symbol) => GetMarketDetails24HAsync(symbol).Result;
        /// <summary>
        /// Gets 24h stats for a market
        /// </summary>
        /// <param name="symbol">The market to get the data for</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiMarketDetails>> GetMarketDetails24HAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await ExecuteRequest<HuobiChannelResponse<HuobiMarketDetails>>(GetUrl(MarketDetailsEndpoint), parameters: parameters, checkResult: false).ConfigureAwait(false);
            if(!result.Success)
                return new WebCallResult<HuobiMarketDetails>(result.ResponseStatusCode, null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new WebCallResult<HuobiMarketDetails>(result.ResponseStatusCode, result.Data.Data, null);
        }

        /// <summary>
        /// Gets a list of supported symbols
        /// </summary>
        /// <returns></returns>
        public WebCallResult<List<HuobiSymbol>> GetSymbols() => GetSymbolsAsync().Result;
        /// <summary>
        /// Gets a list of supported symbols
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiSymbol>>> GetSymbolsAsync()
        {
            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiSymbol>>>(GetUrl(CommonSymbolsEndpoint, "1")).ConfigureAwait(false);
            return new WebCallResult<List<HuobiSymbol>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns></returns>
        public WebCallResult<List<string>> GetCurrencies() => GetCurrenciesAsync().Result;
        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<List<string>>> GetCurrenciesAsync()
        {
            var result = await ExecuteRequest<HuobiBasicResponse<List<string>>>(GetUrl(CommonCurrenciesEndpoint, "1")).ConfigureAwait(false);
            return new WebCallResult<List<string>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns></returns>
        public WebCallResult<DateTime> GetServerTime() => GetServerTimeAsync().Result;
        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync()
        {
            var result = await ExecuteRequest<HuobiBasicResponse<string>>(GetUrl(ServerTimeEndpoint, "1")).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<DateTime>(result.ResponseStatusCode, default(DateTime), result.Error);
            var time = (DateTime)JsonConvert.DeserializeObject(result.Data.Data, typeof(DateTime), new TimestampConverter());
            return new WebCallResult<DateTime>(result.ResponseStatusCode, time, null);
        }

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        public WebCallResult<List<HuobiAccount>> GetAccounts() => GetAccountsAsync().Result;
        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiAccount>>> GetAccountsAsync()
        {
            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiAccount>>>(GetUrl(GetAccountsEndpoint, "1"), signed: true).ConfigureAwait(false);
            return new WebCallResult<List<HuobiAccount>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Gets a list of balances for a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiBalance>> GetBalances(long accountId) => GetBalancesAsync(accountId).Result;
        /// <summary>
        /// Gets a list of balances for a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiBalance>>> GetBalancesAsync(long accountId)
        {
            var result = await ExecuteRequest<HuobiBasicResponse<HuobiAccountBalances>>(GetUrl(FillPathParameter(GetBalancesEndpoint, accountId.ToString()), "1"), signed: true).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<List<HuobiBalance>>(result.ResponseStatusCode, null, result.Error);
            
            return new WebCallResult<List<HuobiBalance>>(result.ResponseStatusCode, result.Data.Data.Data, result.Error);
        }

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiBalance>> GetSubAccountBalances(long subAccountId) => GetSubAccountBalancesAsync(subAccountId).Result;
        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId)
        {
            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiAccountBalances>>>(GetUrl(FillPathParameter(GetSubAccountBalancesEndpoint, subAccountId.ToString()), "1"), signed: true).ConfigureAwait(false);
            if (!result.Success)
                return new WebCallResult<List<HuobiBalance>>(result.ResponseStatusCode, null, result.Error);

            return new WebCallResult<List<HuobiBalance>>(result.ResponseStatusCode, result.Data.Data[0].Data, result.Error);
        }

        /// <summary>
        /// Transfer asset between parent and sub account
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="currency">The crypto currency to transfer</param>
        /// <param name="amount">The amount of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <returns>Unique transfer id</returns>
        public WebCallResult<long> TransferWithSubAccount(long subAccountId, string currency, decimal amount, HuobiTransferType transferType) => TransferWithSubAccountAsync(subAccountId, currency, amount, transferType).Result;
        /// <summary>
        /// Transfer asset between parent and sub account
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="currency">The crypto currency to transfer</param>
        /// <param name="amount">The amount of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <returns>Unique transfer id</returns>
        public async Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string currency, decimal amount, HuobiTransferType transferType)
        {
            var parameters = new Dictionary<string, object>
            {
                { "sub-uid", subAccountId },
                { "currency", currency },
                { "amount", amount },
                { "type", JsonConvert.SerializeObject(transferType, new TransferTypeConverter(false)) }
            };

            var result = await ExecuteRequest<HuobiBasicResponse<long>>(GetUrl(TransferWithSubAccountEndpoint, "1"), "POST", parameters, true).ConfigureAwait(false);
            return new WebCallResult<long>(result.ResponseStatusCode, result.Data?.Data ?? 0, result.Error);
        }

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="amount">The amount of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <returns></returns>
        public WebCallResult<long> PlaceOrder(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null) => PlaceOrderAsync(accountId, symbol, orderType, amount, price).Result;
        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="amount">The amount of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <returns></returns>
        public async Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "account-id", accountId },
                { "amount", amount },
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)) }
            };

            // If precision of the symbol = 1 (eg has to use whole amounts, 1,2,3 etc) Huobi doesn't except the .0 postfix (1.0) for amount
            // Issue at the Huobi side
            if (amount % 1 == 0)
                parameters["amount"] = amount.ToString(CultureInfo.InvariantCulture);

            parameters.AddOptionalParameter("price", price);
            var result = await ExecuteRequest<HuobiBasicResponse<long>>(GetUrl(PlaceOrderEndpoint, "1"), "POST", parameters, true).ConfigureAwait(false);
            return new WebCallResult<long>(result.ResponseStatusCode, result.Data?.Data ?? 0, result.Error);
        }

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiOpenOrder>> GetOpenOrders(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null) => GetOpenOrdersAsync(accountId, symbol, side, limit).Result;
        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null)
        {
            if (accountId != null && symbol == null)
                return new WebCallResult<List<HuobiOpenOrder>>(null, null, new ArgumentError("Can't request open orders based on only the account id"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("account-id", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null: JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiOpenOrder>>>(GetUrl(OpenOrdersEndpoint, "1"), "GET", parameters, true).ConfigureAwait(false);
            return new WebCallResult<List<HuobiOpenOrder>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns></returns>
        public WebCallResult<long> CancelOrder(long orderId) => CancelOrderAsync(orderId).Result;
        /// <summary>
        /// Cancels an open order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns></returns>
        public async Task<WebCallResult<long>> CancelOrderAsync(long orderId)
        {
            var result = await ExecuteRequest<HuobiBasicResponse<long>>(GetUrl(FillPathParameter(CancelOrderEndpoint, orderId.ToString()), "1"), "POST", signed: true).ConfigureAwait(false);
            return new WebCallResult<long>(result.ResponseStatusCode, result.Data?.Data ?? 0, result.Error);
        }

        /// <summary>
        /// Cancel multiple open orders
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <returns></returns>
        public WebCallResult<HuobiBatchCancelResult> CancelOrders(IEnumerable<long> orderIds) => CancelOrdersAsync(orderIds).Result;
        /// <summary>
        /// Cancel multiple open orders
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long> orderIds)
        {
            var parameters = new Dictionary<string, object>
            {
                { "order-ids", orderIds.Select(s => s.ToString()) }
            };

            var result = await ExecuteRequest<HuobiBasicResponse<HuobiBatchCancelResult>>(GetUrl(CancelOrdersEndpoint, "1"), "POST", parameters, true).ConfigureAwait(false);
            return new WebCallResult<HuobiBatchCancelResult>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Get details of an order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        public WebCallResult<HuobiOrder> GetOrderInfo(long orderId) => GetOrderInfoAsync(orderId).Result;
        /// <summary>
        /// Get details of an order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        public async Task<WebCallResult<HuobiOrder>> GetOrderInfoAsync(long orderId)
        {
            var result = await ExecuteRequest<HuobiBasicResponse<HuobiOrder>>(GetUrl(FillPathParameter(OrderInfoEndpoint, orderId.ToString()), "1"), "GET", signed: true).ConfigureAwait(false);
            return new WebCallResult<HuobiOrder>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Gets a list of trades made for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <returns></returns>
        public WebCallResult<List<HuobiOrderTrade>> GetOrderTrades(long orderId) => GetOrderTradesAsync(orderId).Result;
        /// <summary>
        /// Gets a list of trades made for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <returns></returns>
        public async Task<WebCallResult<List<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId)
        {
            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiOrderTrade>>>(GetUrl(FillPathParameter(OrderTradesEndpoint, orderId.ToString()), "1"), "GET", signed: true).ConfigureAwait(false);
            return new WebCallResult<List<HuobiOrderTrade>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

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
        public WebCallResult<List<HuobiOrder>> GetOrders(string symbol, IEnumerable<HuobiOrderState> states, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => GetOrdersAsync(symbol, states, types, startTime, endTime, fromId, limit).Result;
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
        public async Task<WebCallResult<List<HuobiOrder>>> GetOrdersAsync(string symbol, IEnumerable<HuobiOrderState> states, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
        {
            var stateConverter = new OrderStateConverter(false);
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "states", string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))) }
            };
            parameters.AddOptionalParameter("start-date", startTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("end-date", endTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("size", limit);

            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiOrder>>>(GetUrl(OrdersEndpoint, "1"), "GET", parameters, true).ConfigureAwait(false);
            return new WebCallResult<List<HuobiOrder>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

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
        public WebCallResult<List<HuobiOrderTrade>> GetSymbolTrades(string symbol, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => GetSymbolTradesAsync(symbol, types, startTime, endTime, fromId, limit).Result;
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
        public async Task<WebCallResult<List<HuobiOrderTrade>>> GetSymbolTradesAsync(string symbol, IEnumerable<HuobiOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
        {
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("start-date", startTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("end-date", endTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("size", limit);

            var result = await ExecuteRequest<HuobiBasicResponse<List<HuobiOrderTrade>>>(GetUrl(SymbolTradesEndpoint, "1"), "GET", parameters, true).ConfigureAwait(false);
            return new WebCallResult<List<HuobiOrderTrade>>(result.ResponseStatusCode, result.Data?.Data, result.Error);
        }

        protected override IRequest ConstructRequest(Uri uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var uriString = uri.ToString();
            if (authProvider != null)
                parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed);

            if ((method == Constants.GetMethod || method == Constants.DeleteMethod || postParametersPosition == PostParameters.InUri) && parameters?.Any() == true)
                uriString += "?" + parameters.CreateParamString(true);

            if (method == Constants.PostMethod && signed)
            {
                var uriParamNames = new[] { "AccessKeyId", "SignatureMethod", "SignatureVersion", "Timestamp", "Signature" };
                var uriParams = parameters.Where(p => uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
                uriString += "?" + uriParams.CreateParamString(true);
                parameters = parameters.Where(p => !uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
            }

            var request = RequestFactory.Create(uriString);
            request.ContentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
            request.Accept = Constants.JsonContentHeader;
            request.Method = method;

            var headers = new Dictionary<string, string>();
            if (authProvider != null)
                headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters, signed);

            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);

            if ((method == Constants.PostMethod || method == Constants.PutMethod) && postParametersPosition != PostParameters.InUri)
            {
                if (parameters?.Any() == true)
                    WriteParamBody(request, parameters);
                else
                    WriteParamBody(request, "{}");
            }

            return request;
        }

        protected override bool IsErrorResponse(JToken data)
        {
            return data["status"] != null && (string)data["status"] != "ok";
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if(error["err-code"] == null || error["err-msg"]==null)
                return new ServerError(error.ToString());

            return new ServerError($"{(string)error["err-code"]}, {(string)error["err-msg"]}");
        }

        protected Uri GetUrl(string endpoint, string version=null)
        {
            return version == null ? new Uri($"{BaseAddress}/{endpoint}") : new Uri($"{BaseAddress}/v{version}/{endpoint}");
        }
        #endregion
    }
}
