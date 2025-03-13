using HTX.Net.Objects.Models.UsdtMarginSwap;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtFuturesApiSubAccount : IHTXRestClientUsdtFuturesApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientUsdtFuturesApi _baseClient;

        internal HTXRestClientUsdtFuturesApiSubAccount(HTXRestClientUsdtFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Set Trading Permissions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountResult>> SetTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "sub_uid", string.Join(",", subAccountUids) },
                { "sub_auth", enabled ? "1": "0" }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_sub_auth", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXSubAccountResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade Permissions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubTradePermissions>> GetTradePermissionsAsync(IEnumerable<string>? subAccountUids = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("sub_uid", subAccountUids == null ? null : string.Join(",", subAccountUids));
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptionalEnum("direction", filterDirection);
            parameters.AddOptional("from_id", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/linear-swap-api/v1/swap_sub_auth_list", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXSubTradePermissions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Isolated Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginSubAccountAssets[]>> GetIsolatedMarginAssetsAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_sub_account_list", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginSubAccountAssets[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<HTXIsolatedMarginSubAccountAssets[]>> GetCrossMarginAssetsAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_cross_sub_account_list", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXIsolatedMarginSubAccountAssets[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Asset Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountAssetInfoPage>> GetIsolatedMarginAssetInfoAsync(string? contractCode = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_sub_account_info_list", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXSubAccountAssetInfoPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Asset Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountCrossAssetInfoPage>> GetCrossMarginAssetInfoAsync(string? marginAccount = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("margin_account", marginAccount);
            parameters.AddOptional("page_index", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_sub_account_info_list", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXSubAccountCrossAssetInfoPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        // /linear-swap-api/v1/swap_sub_account_info
        // /linear-swap-api/v1/swap_cross_sub_account_info

        #region Get Isolated Margin Positions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPosition[]>> GetIsolatedMarginPositionsAsync(long subUserId, string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("sub_uid", subUserId);
            parameters.AddOptional("contract_code", contractCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_sub_position_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Positions

        /// <inheritdoc />
        public async Task<WebCallResult<HTXCrossPosition[]>> GetCrossMarginPositionsAsync(long subUserId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("sub_uid", subUserId);
            parameters.AddOptional("contract_code", contractCode);
            parameters.AddOptional("pair", pair);
            parameters.AddOptionalEnum("contract_type", contractType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/linear-swap-api/v1/swap_cross_sub_position_info", HTXExchange.RateLimiter.UsdtRead, 1, true);
            var result = await _baseClient.SendBasicAsync<HTXCrossPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Master Sub

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSwapOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default)
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_master_sub_transfer", HTXExchange.RateLimiter.UsdtTrade, 1, true);
            return await _baseClient.SendBasicAsync<HTXSwapOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Master Sub Transfer Records

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMasterSubTransferPage>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "margin_account", marginAccount },
                { "create_date", daysInHistory }
            };
            parameters.AddOptionalParameter("transfer_type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "linear-swap-api/v1/swap_master_sub_transfer_record", HTXExchange.RateLimiter.UsdtRead, 1, true);
            return await _baseClient.SendBasicAsync<HTXMasterSubTransferPage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
