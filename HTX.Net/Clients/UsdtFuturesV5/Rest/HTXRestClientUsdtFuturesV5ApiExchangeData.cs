using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesV5ApiExchangeData : IHTXRestClientUsdtFuturesV5ApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesV5Api _baseClient;

        internal HTXRestClientUsdtFuturesV5ApiExchangeData(HTXRestClientUsdtFuturesV5Api baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<HTXFundingRateV5>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/market/funding_rate", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendAsync<HTXFundingRateV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<HTXFundingRateHistoryV5[]>> GetFundingRateHistoryAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/market/funding_rate_history", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendAsync<HTXFundingRateHistoryV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<HTXOpenInterestV5>> GetOpenInterestAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/market/open_interest", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendAsync<HTXOpenInterestV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Price Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXPriceLimitV5[]>> GetPriceLimitsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/market/price_limit", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendAsync<HTXPriceLimitV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXRiskLimitV5[]>> GetRiskLimitsAsync(string contractCode, MarginMode? marginMode = null, string? tier = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("margin_mode", marginMode);
            parameters.Add("tier", tier);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/market/risk/limit", HTXExchange.RateLimiter.UsdtRead, 1, false);
            return await _baseClient.SendAsync<HTXRiskLimitV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
