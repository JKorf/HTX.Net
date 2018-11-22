using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huobi.Net
{
    public class HuobiClient: RestClient
    {
        #region fields
        private static HuobiClientOptions defaultOptions = new HuobiClientOptions();
        private static HuobiClientOptions DefaultOptions
        {
            get
            {
                var result = new HuobiClientOptions()
                {
                    LogVerbosity = defaultOptions.LogVerbosity,
                    BaseAddress = defaultOptions.BaseAddress,
                    LogWriters = defaultOptions.LogWriters,
                    Proxy = defaultOptions.Proxy,
                    RateLimiters = defaultOptions.RateLimiters,
                    RateLimitingBehaviour = defaultOptions.RateLimitingBehaviour
                };

                if (defaultOptions.ApiCredentials != null)
                    result.ApiCredentials = new ApiCredentials(defaultOptions.ApiCredentials.Key.GetString(), defaultOptions.ApiCredentials.Secret.GetString());

                return result;
            }
        }


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
            Configure(options);
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

        public CallResult<HuobiTimestampResponse<List<HuobiMarketTick>>> GetMarketTickers() => GetMarketTickersAsync().Result;
        public async Task<CallResult<HuobiTimestampResponse<List<HuobiMarketTick>>>> GetMarketTickersAsync()
        {
            return GetResult(await ExecuteRequest<HuobiTimestampResponse<List<HuobiMarketTick>>>(GetUrl(MarketTickerEndpoint)));            
        }

        public CallResult<HuobiChannelResponse<HuobiMarketTickMerged>> GetMarketTickerMerged(string symbol) => GetMarketTickerMergedAsync(symbol).Result;
        public async Task<CallResult<HuobiChannelResponse<HuobiMarketTickMerged>>> GetMarketTickerMergedAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<HuobiMarketTickMerged>>(GetUrl(MarketTickerMergedEndpoint), parameters: parameters));
        }

        public CallResult<HuobiChannelResponse<List<HuobiMarketData>>> GetMarketKlines(string symbol, HuobiPeriod period, int size) => GetMarketKlinesAsync(symbol, period, size).Result;
        public async Task<CallResult<HuobiChannelResponse<List<HuobiMarketData>>>> GetMarketKlinesAsync(string symbol, HuobiPeriod period, int size)
        {
            if (size <= 0 || size > 2000)
                return new CallResult<HuobiChannelResponse<List<HuobiMarketData>>>(null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
                { "size", size },
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<List<HuobiMarketData>>>(GetUrl(MarketKlineEndpoint), parameters: parameters));
        }

        public CallResult<HuobiChannelResponse<HuobiMarketDepth>> GetMarketDepth(string symbol, int mergeStep) => GetMarketDepthAsync(symbol, mergeStep).Result;
        public async Task<CallResult<HuobiChannelResponse<HuobiMarketDepth>>> GetMarketDepthAsync(string symbol, int mergeStep)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new CallResult<HuobiChannelResponse<HuobiMarketDepth>>(null, new ArgumentError("MergeStep should be between 0 and 5"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", "step"+mergeStep },
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<HuobiMarketDepth>>(GetUrl(MarketDepthEndpoint), parameters: parameters));
        }

        public CallResult<HuobiChannelResponse<HuobiMarketTrade>> GetMarketLastTrade(string symbol) => GetMarketLastTradeAsync(symbol).Result;
        public async Task<CallResult<HuobiChannelResponse<HuobiMarketTrade>>> GetMarketLastTradeAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<HuobiMarketTrade>>(GetUrl(MarketLastTradeEndpoint), parameters: parameters));
        }

        public CallResult<HuobiChannelResponse<List<HuobiMarketTrade>>> GetMarketTradeHistory(string symbol, int size) => GetMarketTradeHistoryAsync(symbol, size).Result;
        public async Task<CallResult<HuobiChannelResponse<List<HuobiMarketTrade>>>> GetMarketTradeHistoryAsync(string symbol, int size)
        {
            if (size <= 0 || size > 2000)
                return new CallResult<HuobiChannelResponse<List<HuobiMarketTrade>>>(null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "size", size },
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<List<HuobiMarketTrade>>>(GetUrl(MarketTradeHistoryEndpoint), parameters: parameters));
        }

        public CallResult<HuobiChannelResponse<HuobiMarketData>> GetMarketDetails24h(string symbol) => GetMarketDetails24hAsync(symbol).Result;
        public async Task<CallResult<HuobiChannelResponse<HuobiMarketData>>> GetMarketDetails24hAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return GetResult(await ExecuteRequest<HuobiChannelResponse<HuobiMarketData>>(GetUrl(MarketDetailsEndpoint), parameters: parameters));
        }

        public CallResult<HuobiBasicResponse<List<HuobiSymbol>>> GetSymbols() => GetSymbolsAsync().Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiSymbol>>>> GetSymbolsAsync()
        {

            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiSymbol>>>(GetUrl(CommonSymbolsEndpoint, "1")));
        }

        public CallResult<HuobiBasicResponse<List<string>>> GetCurrencies() => GetCurrenciesAsync().Result;
        public async Task<CallResult<HuobiBasicResponse<List<string>>>> GetCurrenciesAsync()
        {

            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<string>>>(GetUrl(CommonCurrenciesEndpoint, "1")));
        }

        public CallResult<DateTime> GetServerTime() => GetServerTimeAsync().Result;
        public async Task<CallResult<DateTime>> GetServerTimeAsync()
        {
            var result = GetResult(await ExecuteRequest<HuobiBasicResponse<string>>(GetUrl(ServerTimeEndpoint, "1")));
            if (!result.Success)
                return new CallResult<DateTime>(default(DateTime), result.Error);
            var time = (DateTime)JsonConvert.DeserializeObject(result.Data.Data, typeof(DateTime), new TimestampConverter());
            return new CallResult<DateTime>(time, null);
        }

        public CallResult<HuobiBasicResponse<List<HuobiAccount>>> GetAccounts() => GetAccountsAsync().Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiAccount>>>> GetAccountsAsync()
        {
            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiAccount>>>(GetUrl(GetAccountsEndpoint, "1"), signed: true));
        }

        public CallResult<HuobiBasicResponse<HuobiAccountBalances>> GetBalances(long accountId) => GetBalancesAsync(accountId).Result;
        public async Task<CallResult<HuobiBasicResponse<HuobiAccountBalances>>> GetBalancesAsync(long accountId)
        {
            return GetResult(await ExecuteRequest<HuobiBasicResponse<HuobiAccountBalances>>(GetUrl(FillPathParameter(GetBalancesEndpoint, accountId.ToString()), "1"), signed: true));
        }

        public CallResult<HuobiBasicResponse<long>> PlaceOrder(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null) => PlaceOrderAsync(accountId, symbol, orderType, amount, price).Result;
        public async Task<CallResult<HuobiBasicResponse<long>>> PlaceOrderAsync(long accountId, string symbol, HuobiOrderType orderType, decimal amount, decimal? price = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "account-id", accountId },
                { "amount", amount },
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("price", price);
            return GetResult(await ExecuteRequest<HuobiBasicResponse<long>>(GetUrl(PlaceOrderEndpoint, "1"), "POST", parameters, true));
        }

        public CallResult<HuobiBasicResponse<List<HuobiOrder>>> GetOpenOrders(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null) => GetOpenOrdersAsync(accountId, symbol, side, limit).Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiOrder>>>> GetOpenOrdersAsync(long? accountId = null, string symbol = null, HuobiOrderSide? side = null, int? limit = null)
        {
            if (accountId != null && symbol == null)
                return new CallResult<HuobiBasicResponse<List<HuobiOrder>>>(null, new ArgumentError("Can't request open orders based on only the account id"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("account-id", accountId);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null: JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("size", limit);

            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiOrder>>>(GetUrl(OpenOrdersEndpoint, "1"), "GET", parameters, signed: true));
        }

        public CallResult<HuobiBasicResponse<long>> CancelOrder(long orderId) => CancelOrderAsync(orderId).Result;
        public async Task<CallResult<HuobiBasicResponse<long>>> CancelOrderAsync(long orderId)
        {
            return GetResult(await ExecuteRequest<HuobiBasicResponse<long>>(GetUrl(FillPathParameter(CancelOrderEndpoint, orderId.ToString()), "1"), "POST", signed: true));
        }

        public CallResult<HuobiBasicResponse<HuobiBatchCancelResult>> CancelOrders(long[] orderIds) => CancelOrdersAsync(orderIds).Result;
        public async Task<CallResult<HuobiBasicResponse<HuobiBatchCancelResult>>> CancelOrdersAsync(long[] orderIds)
        {
            var parameters = new Dictionary<string, object>
            {
                { "order-ids", orderIds.Select(s => s.ToString()) }
            };

            return GetResult(await ExecuteRequest<HuobiBasicResponse<HuobiBatchCancelResult>>(GetUrl(CancelOrdersEndpoint, "1"), "POST", parameters, true));
        }

        public CallResult<HuobiBasicResponse<HuobiOrder>> GetOrderInfo(long orderId) => GetOrderInfoAsync(orderId).Result;
        public async Task<CallResult<HuobiBasicResponse<HuobiOrder>>> GetOrderInfoAsync(long orderId)
        {
            return GetResult(await ExecuteRequest<HuobiBasicResponse<HuobiOrder>>(GetUrl(FillPathParameter(OrderInfoEndpoint, orderId.ToString()), "1"), "GET", signed: true));
        }

        public CallResult<HuobiBasicResponse<List<HuobiOrderTrade>>> GetOrderTrades(long orderId) => GetOrderTradesAsync(orderId).Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiOrderTrade>>>> GetOrderTradesAsync(long orderId)
        {
            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiOrderTrade>>>(GetUrl(FillPathParameter(OrderTradesEndpoint, orderId.ToString()), "1"), "GET", signed: true));
        }

        public CallResult<HuobiBasicResponse<List<HuobiOrder>>> GetOrders(string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => GetOrdersAsync(symbol, states, types, startTime, endTime, fromId, limit).Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiOrder>>>> GetOrdersAsync(string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
        {
            var stateConverter = new OrderStateConverter(false);
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "states", string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))) }
            };
            parameters.AddOptionalParameter("start-date", startTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("end-date", endTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("size", limit);

            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiOrder>>>(GetUrl(OrdersEndpoint, "1"), "GET", parameters, signed: true));
        }

        public CallResult<HuobiBasicResponse<List<HuobiOrderTrade>>> GetSymbolTrades(string symbol, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => GetSymbolTradesAsync(symbol, types, startTime, endTime, fromId, limit).Result;
        public async Task<CallResult<HuobiBasicResponse<List<HuobiOrderTrade>>>> GetSymbolTradesAsync(string symbol, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
        {
            var stateConverter = new OrderStateConverter(false);
            var typeConverter = new OrderTypeConverter(false);
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("start-date", startTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("end-date", endTime?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("size", limit);

            return GetResult(await ExecuteRequest<HuobiBasicResponse<List<HuobiOrderTrade>>>(GetUrl(SymbolTradesEndpoint, "1"), "GET", parameters, signed: true));
        }

        protected override IRequest ConstructRequest(Uri uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var uriString = uri.ToString();
            if (authProvider != null)
                parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed);

            if ((method == Constants.GetMethod || method == Constants.DeleteMethod || (postParametersPosition == PostParameters.InUri)) && parameters?.Any() == true)
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


        protected override Error ParseErrorResponse(string error)
        {
            var des = Deserialize<HuobiTimestampResponse<object>>(error);
            if (!des.Success)
                return new UnknownError("Failed to deserialize error: " + des.Error.Message);

            return new ServerError($"{des.Data.ErrorCode}: {des.Data.ErrorMessage}");
        }


        private static CallResult<T> GetResult<T>(CallResult<T> result) where T: HuobiApiResponse
        {
            if (result.Error != null || result.Data == null)
                return new CallResult<T>(null, result.Error);

            if (result.Data.Status == "ok")
                return new CallResult<T>(result.Data, null);

            return new CallResult<T>(null, new ServerError($"{result.Data.ErrorCode}: {result.Data.ErrorMessage}"));
        }        

        protected Uri GetUrl(string endpoint, string version=null)
        {
            if(version == null)
                return new Uri($"{baseAddress}/{endpoint}");
            else
                return new Uri($"{baseAddress}/v{version}/{endpoint}");
        }
        #endregion
    }
}
