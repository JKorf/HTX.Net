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
        public async Task<WebCallResult<HTXAssetModeV5>> GetAssetModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/account/asset_mode", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAssetModeV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Asset Mode

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAssetModeUpdateV5>> SetAssetModeAsync(AssetMode assetMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "assets_mode", EnumConverter.GetString(assetMode) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/account/asset_mode", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXAssetModeUpdateV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Balance

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAccountBalanceV5>> GetAccountBalanceAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/account/balance", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXAccountBalanceV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Bills

        /// <inheritdoc />
        public async Task<WebCallResult<HTXBillV5[]>> GetBillsAsync(string? contractCode = null, MarginMode? marginMode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("type", types == null ? null : string.Join(",", types.Select(EnumConverter.GetString)));
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptionalParameter("from", fromId);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/account/bills", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXBillV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionModeV5>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/position/mode", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionModeV5>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionModeV5>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "position_mode", EnumConverter.GetString(positionMode) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/position/mode", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXPositionModeV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLeverageV5[]>> GetLeverageAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("position_side", EnumConverter.GetString(positionSide));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/position/lever", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXLeverageV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLeverageV5>> SetLeverageAsync(string contractCode, MarginMode marginMode, int leverageRate, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "contract_code", contractCode },
                { "margin_mode", EnumConverter.GetString(marginMode) },
                { "lever_rate", leverageRate }
            };
            parameters.AddOptionalParameter("position_side", EnumConverter.GetString(positionSide));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v5/position/lever", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendAsync<HTXLeverageV5>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Positions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionV5[]>> GetOpenPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/trade/position/opens", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limits

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionRiskLimitV5[]>> GetRiskLimitsAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("margin_mode", EnumConverter.GetString(marginMode));
            parameters.AddOptionalParameter("position_side", EnumConverter.GetString(positionSide));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v5/position/risk/limit", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendAsync<HTXPositionRiskLimitV5[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
