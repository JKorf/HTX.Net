using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesV5ApiAccount : IHTXRestClientUsdtFuturesV5ApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesV5Api _baseClient;

        internal HTXRestClientUsdtFuturesV5ApiAccount(HTXRestClientUsdtFuturesV5Api baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Asset Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXAssetModeV5>> GetAssetModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/account/asset_mode", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAssetModeV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Asset Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXAssetModeUpdateV5>> SetAssetModeAsync(AssetMode assetMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "assets_mode", EnumConverter.GetString(assetMode) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/account/asset_mode", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAssetModeUpdateV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Balance

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccountBalanceV5>> GetAccountBalanceAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/account/balance", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAccountBalanceV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Bills

        /// <inheritdoc />
        public async Task<HttpResult<HTXBillV5[]>> GetBillsAsync(string? contractCode = null, MarginMode? marginMode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("type", types == null ? null : string.Join(",", types.Select(EnumConverter.GetString)));
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("from", fromId);
            parameters.Add("limit", limit);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/account/bills", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXBillV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionModeV5>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/position/mode", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionModeV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionModeV5>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "position_mode", EnumConverter.GetString(positionMode) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/position/mode", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXPositionModeV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXLeverageV5[]>> GetLeverageAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("position_side", positionSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/position/lever", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXLeverageV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXLeverageV5>> SetLeverageAsync(string contractCode, MarginMode marginMode, int leverageRate, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate }
            };
            parameters.Add("margin_mode", marginMode);
            parameters.Add("position_side", positionSide);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v5/position/lever", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXLeverageV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionV5[]>> GetOpenPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/trade/position/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionRiskLimitV5[]>> GetRiskLimitsAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("margin_mode", marginMode);
            parameters.Add("position_side", positionSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v5/position/risk/limit", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionRiskLimitV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
