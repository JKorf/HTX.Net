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

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("valuation_asset", asset);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXAssetValue>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_balance_valuation"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginAccountInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXPosition>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetAndPositionsAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginAssetsAndPositions>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossMarginAssetsAndPositions>> GetCrossMarginAssetAndPositionsAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount }
            };
            return await _baseClient.SendHTXRequest<HTXCrossMarginAssetsAndPositions>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_account_position_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountResult>> SetSubAccountsTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "sub_uid", string.Join(",", subAccountUids) },
                { "sub_auth", enabled ? "1": "0" }
            };
            return await _baseClient.SendHTXRequest<HTXSubAccountResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_auth"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetIsolatedMarginSubAccountsAssetsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetCrossMarginSubAccountsAssetsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginSubAccountAssets>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_sub_account_list"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXFinancialRecordsPage>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? createDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("type", types == null ? null : string.Join(",", types.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("create_date", DateTimeConverter.ConvertToMilliseconds(createDate));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHTXRequest<HTXFinancialRecordsPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_financial_record"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXIsolatedMarginUserSettlementRecordPage>(_baseClient.GetUrl("linear-swap-api/v1/swap_user_settlement_records"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXCrossMarginUserSettlementRecordPage>(_baseClient.GetUrl("linear-swap-api/v1/swap_cross_user_settlement_records"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHTXRequest<IEnumerable<HTXIsolatedMarginLeverageAvailable>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_available_level_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXCrossMarginLeverageAvailable>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_available_level_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXTradingFee>>> GetTradingFeesAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHTXRequest<IEnumerable<HTXTradingFee>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_fee"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_master_sub_transfer"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXMasterSubTransfer>(_baseClient.GetUrl("/linear-swap-api/v1/swap_master_sub_transfer_record"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            return await _baseClient.SendHTXRequest<HTXOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_transfer_inner"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionMode>> ModifyIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            return await _baseClient.SendHTXRequest<HTXPositionMode>(_baseClient.GetUrl("/linear-swap-api/v1/swap_switch_position_mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPositionMode>> ModifyCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "position_mode", EnumConverter.GetString(positionMode) },
            };
            return await _baseClient.SendHTXRequest<HTXPositionMode>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_switch_position_mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
