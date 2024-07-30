using HTX.Net.Clients.FuturesApi;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace HTX.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtMarginSwapApiAccount : IHTXRestClientUsdtMarginSwapApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtMarginSwapApi _baseClient;

        internal HTXRestClientUsdtMarginSwapApiAccount(HTXRestClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Asset Valuation

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("valuation_asset", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_balance_valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAssetValue>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_account_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginAccountInfo>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_account_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXCrossMarginAccountInfo>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXPosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXPosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Iosalted Margin Assets And Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetsAndPositionsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_account_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginAssetsAndPositions>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Assets And Positions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginAssetsAndPositions>> GetCrossMarginAssetsAndPositionsAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_account_position_info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginAssetsAndPositions>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Sub Accounts Trading Permissions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "sub_uid", string.Join(",", subAccountUids) },
                { "sub_auth", enabled ? "1": "0" }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_sub_auth", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXSubAccountResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_sub_auth_list

        #region Get Isolated Margin Sub Accounts Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_sub_account_list", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginSubAccountAssets>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Sub Accounts Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_sub_account_list", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginSubAccountAssets>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_sub_account_info_list
        // /linear-swap-api/v1/swap_cross_sub_account_info_list
        // /linear-swap-api/v1/swap_sub_account_info
        // /linear-swap-api/v1/swap_cross_sub_account_info
        // /linear-swap-api/v1/swap_sub_position_info
        // /linear-swap-api/v1/swap_cross_sub_position_info

        #region Get Financial Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFinancialRecordsPage>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? createDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
#warning Update to V3
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("type", types == null ? null : string.Join(",", types.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("create_date", DateTimeConverter.ConvertToMilliseconds(createDate));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_financial_record", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXFinancialRecordsPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v3/swap_financial_record_exact

        #region Get Isolated Margin Available Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_available_level_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXIsolatedMarginLeverageAvailable>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Available Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_available_level_rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXCrossMarginLeverageAvailable>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_order_limit

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXTradingFee>>> GetTradingFeesAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_fee", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXTradingFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_transfer_limit
        // /linear-swap-api/v1/swap_cross_transfer_limit
        // /linear-swap-api/v1/swap_position_limit
        // /linear-swap-api/v1/swap_cross_position_limit
        // /linear-swap-api/v1/swap_lever_position_limit
        // /linear-swap-api/v1/swap_cross_lever_position_limit

        #region Transfer Master Sub

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "sub_uid", subUid },
                { "asset", asset },
                { "from_margin_account", fromMarginAccount },
                { "to_margin_account", toMarginAccount },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", type == MasterSubTransferType.SubToMaster ? "sub_to_master": "master_to_sub" },
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_master_sub_transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Master Sub Transfer Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMasterSubTransfer>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "create_date", daysInHistory }
            };
            parameters.AddOptionalParameter("transfer_type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_master_sub_transfer_record", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXMasterSubTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Margin Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", asset },
                { "from_margin_account", fromMarginAccount },
                { "to_margin_account", toMarginAccount },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_transfer_inner", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_api_trading_status

        #region Set Isolated Margin Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionMode>> SetIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_switch_position_mode", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Cross Margin Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionMode>> SetCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_switch_position_mode", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /linear-swap-api/v1/swap_position_side
        // /linear-swap-api/v1/swap_cross_position_side

        // USDT-M Unified Account
        // /linear-swap-api/v3/unified_account_info
        // /linear-swap-api/v3/linear_swap_overview_account_info
        // /linear-swap-api/v3/linear_swap_fee_switch
        // /linear-swap-api/v3/fix_position_margin_change
        // /linear-swap-api/v3/fix_position_margin_change_record
        // /linear-swap-api/v3/swap_unified_account_type
        // /linear-swap-api/v3/swap_switch_account_type

        #region Get Isolated Margin Settlement Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_user_settlement_records", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginUserSettlementRecordPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Settlement Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_user_settlement_records", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXCrossMarginUserSettlementRecordPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
