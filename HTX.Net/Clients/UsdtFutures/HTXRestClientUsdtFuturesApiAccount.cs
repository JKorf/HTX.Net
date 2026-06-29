using HTX.Net.Objects.Models.UsdtMarginSwap;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtMarginSwapApiAccount : IHTXRestClientUsdtFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesApi _baseClient;

        internal HTXRestClientUsdtMarginSwapApiAccount(HTXRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Asset Valuation

        /// <inheritdoc />
        public async Task<HttpResult<HTXAssetValue[]>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("valuation_asset", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_balance_valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXAssetValue[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Account Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginAccountInfo[]>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_account_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginAccountInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Account Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginAccountInfo[]>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_account_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginAccountInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Positions

        /// <inheritdoc />
        public async Task<HttpResult<HTXPosition[]>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Positions

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossPosition[]>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Assets And Positions

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginAssetsAndPositions[]>> GetIsolatedMarginAssetsAndPositionsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_account_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginAssetsAndPositions[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Assets And Positions

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginAssetsAndPositions>> GetCrossMarginAssetsAndPositionsAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "margin_account", marginAccount }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_account_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginAssetsAndPositions>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Financial Records

        /// <inheritdoc />
        public async Task<HttpResult<HTXFinancialRecord[]>> GetFinancialRecordsAsync(
            string marginAccount,
            string? contractCode = null, 
            IEnumerable<FinancialRecordType>? types = null, 
            DateTime? startTime = null, 
            DateTime? endTime = null,
            FilterDirection? direction = null, 
            long? fromId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("mar_acct", marginAccount);
            parameters.Add("from_id", fromId);
            parameters.Add("contract", contractCode);
            parameters.Add("type", types?.Any() == true ? string.Join(",", types.Select(x => EnumConverter.GetString(x))) : null);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v3/swap_financial_record", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendAsync<HTXFinancialRecord[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        // /linear-swap-api/v3/swap_financial_record_exact

        #region Get Isolated Margin Available Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginLeverageAvailable[]>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_available_level_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginLeverageAvailable[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Available Leverage

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginLeverageAvailable[]>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_available_level_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginLeverageAvailable[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderLimit>> GetOrderLimitsAsync(OrderPriceType orderType, string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("order_price_type", orderType);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("business_type", businessType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_order_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXOrderLimit>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<HttpResult<HTXTradingFee[]>> GetTradingFeesAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", symbol);
            parameters.Add("contract_type", EnumConverter.GetString(contractType));
            parameters.Add("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_fee", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXTradingFee[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Transfer Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXTransferLimit[]>> GetIsolatedMarginTransferLimitsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_transfer_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTransferLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Transfer Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossTransferLimit[]>> GetCrossMarginTransferLimitsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_transfer_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossTransferLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Position Limit

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionLimit[]>> GetIsolatedMarginPositionLimitAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_position_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXPositionLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Position Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginPositionLimit[]>> GetCrossMarginPositionLimitsAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("pair", pair);
            parameters.Add("contract_type", contractType);
            parameters.Add("business_type", businessType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_position_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossMarginPositionLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Leverage Position Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXLeveragePositionLimit[]>> GetIsolatedMarginLeveragePositionLimitsAsync(string? contractCode = null, int? leverageRate = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("contract_code", contractCode);
            parameters.Add("lever_rate", leverageRate);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_lever_position_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXLeveragePositionLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Leverage Position Limits

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossLeveragePositionLimit[]>> GetCrossMarginLeveragePositionLimitsAsync(BusinessType businessType, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("business_type", businessType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_lever_position_limit", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossLeveragePositionLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Margin Accounts

        /// <inheritdoc />
        public async Task<HttpResult<HTXSwapOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "asset", asset },
                { "from_margin_account", fromMarginAccount },
                { "to_margin_account", toMarginAccount },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_transfer_inner", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXSwapOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Status

        /// <inheritdoc />
        public async Task<HttpResult<HTXTradingStatus>> GetTradingStatusAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_api_trading_status", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXTradingStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Isolated Margin Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionMode>> SetIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_switch_position_mode", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Cross Margin Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionMode>> SetCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_switch_position_mode", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionMode>> GetIsolatedMarginPositionModeAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_position_side", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<HTXPositionMode>> GetCrossMarginPositionModeAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings);
            parameters.Add("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/linear-swap-api/v1/swap_cross_position_side", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Settlement Records

        /// <inheritdoc />
        public async Task<HttpResult<HTXIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "contract_code", contractCode }
            };
            parameters.Add("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_user_settlement_records", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginUserSettlementRecordPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Settlement Records

        /// <inheritdoc />
        public async Task<HttpResult<HTXCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._futuresParameterSerializationSettings)
            {
                { "margin_account", marginAccount }
            };
            parameters.Add("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("page_index", page);
            parameters.Add("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "linear-swap-api/v1/swap_cross_user_settlement_records", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginUserSettlementRecordPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
