using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net
{
    public class HuobiClient: ExchangeClient
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

        public CallResult<HuobiTimestampListResponse<HuobiMarketTick>> GetMarketTickers() => GetMarketTickersAsync().Result;
        public async Task<CallResult<HuobiTimestampListResponse<HuobiMarketTick>>> GetMarketTickersAsync()
        {
            return GetResult(await ExecuteRequest<HuobiTimestampListResponse<HuobiMarketTick>>(GetUrl(MarketTickerEndpoint)));            
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

        public CallResult<HuobiChannelListResponse<HuobiMarketData>> GetMarketKlines(string symbol, HuobiPeriod period, int size) => GetMarketKlinesAsync(symbol, period, size).Result;
        public async Task<CallResult<HuobiChannelListResponse<HuobiMarketData>>> GetMarketKlinesAsync(string symbol, HuobiPeriod period, int size)
        {
            if (size <= 0 || size > 2000)
                return new CallResult<HuobiChannelListResponse<HuobiMarketData>>(null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
                { "size", size },
            };

            return GetResult(await ExecuteRequest<HuobiChannelListResponse<HuobiMarketData>>(GetUrl(MarketKlineEndpoint), parameters: parameters));
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

        public CallResult<HuobiChannelListResponse<HuobiMarketTrade>> GetMarketTradeHistory(string symbol, int size) => GetMarketTradeHistoryAsync(symbol, size).Result;
        public async Task<CallResult<HuobiChannelListResponse<HuobiMarketTrade>>> GetMarketTradeHistoryAsync(string symbol, int size)
        {
            if (size <= 0 || size > 2000)
                return new CallResult<HuobiChannelListResponse<HuobiMarketTrade>>(null, new ArgumentError("Size should be between 1 and 2000"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "size", size },
            };

            return GetResult(await ExecuteRequest<HuobiChannelListResponse<HuobiMarketTrade>>(GetUrl(MarketTradeHistoryEndpoint), parameters: parameters));
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

        public CallResult<HuobiBasicListResponse<HuobiSymbol>> GetSymbols() => GetSymbolsAsync().Result;
        public async Task<CallResult<HuobiBasicListResponse<HuobiSymbol>>> GetSymbolsAsync()
        {

            return GetResult(await ExecuteRequest<HuobiBasicListResponse<HuobiSymbol>>(GetUrl(CommonSymbolsEndpoint, "1")));
        }

        public CallResult<HuobiBasicListResponse<string>> GetCurrencies() => GetCurrenciesAsync().Result;
        public async Task<CallResult<HuobiBasicListResponse<string>>> GetCurrenciesAsync()
        {

            return GetResult(await ExecuteRequest<HuobiBasicListResponse<string>>(GetUrl(CommonCurrenciesEndpoint, "1")));
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

        public CallResult<HuobiBasicListResponse<HuobiAccount>> GetAccounts() => GetAccountsAsync().Result;
        public async Task<CallResult<HuobiBasicListResponse<HuobiAccount>>> GetAccountsAsync()
        {
            return GetResult(await ExecuteRequest<HuobiBasicListResponse<HuobiAccount>>(GetUrl(GetAccountsEndpoint, "1"), signed: true));
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
