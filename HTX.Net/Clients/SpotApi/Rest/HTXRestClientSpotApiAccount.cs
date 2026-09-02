using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Internal;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApiAccount : IHTXRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientSpotApi _baseClient;

        internal HTXRestClientSpotApiAccount(HTXRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Accounts

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccount[]>> GetAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/account/accounts", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXAccount[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances
        
        /// <inheritdoc />
        public async Task<HttpResult<HTXBalance[]>> GetBalancesAsync(long accountId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/account/accounts/{accountId}/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<HTXAccountBalances>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXBalance[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Platform Valuation

        /// <inheritdoc />
        public async Task<HttpResult<HTXPlatformValuation>> GetPlatformValuationAsync(AccountType? accountType = null, string? valuationAsset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("accountType", accountType);
            parameters.Add("valuationCurrency", valuationAsset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/account/valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(3, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXPlatformValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Valuation

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("accountType", accountType);
            parameters.Add("valuationCurrency", valuationCurrency);
            parameters.Add("subUid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/account/asset-valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXAccountValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Internal Transfer

        /// <inheritdoc />
        public async Task<HttpResult<HTXTransactionResult>> InternalTransferAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "from-account", fromAccountId.ToString(CultureInfo.InvariantCulture)},
                { "from-user", fromUserId.ToString(CultureInfo.InvariantCulture)},

                { "to-account", toAccountId.ToString(CultureInfo.InvariantCulture)},
                { "to-user", toUserId.ToString(CultureInfo.InvariantCulture)},

                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.Add("from-account-type", fromAccountType);
            parameters.Add("to-account-type", toAccountType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/account/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXTransactionResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account History

        /// <inheritdoc />
        public async Task<HttpResult<HTXAccountHistory[]>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "account-id", accountId }
            };
            parameters.Add("currency", asset);
            parameters.Add("transact-types", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => EnumConverter.GetString(s))));
            parameters.Add("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("size", limit);
            parameters.Add("sort", sort);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/account/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXAccountHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<HttpResult<HTXLedgerEntry[]>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "accountId", accountId }
            };
            parameters.Add("currency", asset);
            parameters.Add("transactTypes", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => EnumConverter.GetString(s))));
            parameters.Add("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("limit", limit);
            parameters.Add("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("sort", sort);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/account/ledger", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferAsync(TransferAccount fromAccount, TransferAccount toAccount, string asset, decimal quantity, string marginAccount, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("from", fromAccount);
            parameters.Add("to", toAccount);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("margin-account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v2/account/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Point Balance

        /// <inheritdoc />
        public async Task<HttpResult<HTXPointBalance>> GetPointBalanceAsync(string? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("subUid", subUserId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/point/account", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXPointBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Points

        /// <inheritdoc />
        public async Task<HttpResult<HTXPointTransfer>> TransferPointsAsync(string fromUserId, string toUserId, string groupId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("fromUid", fromUserId);
            parameters.Add("toUid", toUserId);
            parameters.Add("groupId", groupId);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v2/point/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXPointTransfer>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Deduction Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXDeductInfo>> GetUserDeductionInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v1/account/switch/user/info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXDeductInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deduct Assets

        /// <inheritdoc />
        public async Task<HttpResult<HTXDeductionAssets>> GetDeductAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v1/account/overview/info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXDeductionAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Deduction Switch

        /// <inheritdoc />
        public async Task<HttpResult> SetDeductionSwitchAsync(DeductionSwitchType switchType, string? deductionAsset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("switchType", switchType, EnumSerialization.Number);
            parameters.Add("deductionCurrency", deductionAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v1/account/fee/switch", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<object>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<HttpResult<HTXDepositAddress[]>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings) { { "currency", asset } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/account/deposit/address", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawal Quotas

        /// <inheritdoc />
        public async Task<HttpResult<HTXWithdrawalQuota>> GetWithdrawalQuotasAsync(string? asset = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/account/withdraw/quota", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXWithdrawalQuota>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal Addresses

        /// <inheritdoc />
        public async Task<HttpResult<HTXWithdrawalAddress[]>> GetWithdrawalAddressesAsync(string asset, string? network = null, string? note = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            parameters.Add("note", note);
            parameters.Add("limit", limit);
            parameters.Add("fromId", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/account/withdraw/address", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXWithdrawalAddress[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "address", address },
                { "currency", asset },
                { "amount", quantity },
                { "fee", fee },
            };

            parameters.Add("chain", network);
            parameters.Add("addr-tag", addressTag);
            parameters.Add("client-order-id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/dw/withdraw/api/create", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Withdrawal By Client Order Id

        /// <inheritdoc />
        public async Task<HttpResult<HTXWithdrawDeposit>> GetWithdrawalByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v1/query/withdraw/client-order-id", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<HTXWithdrawDeposit>(request, parameters, ct).ConfigureAwait(false);
            if (result.Data == null)
                return HttpResult.Fail<HTXWithdrawDeposit>(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Not found")));

            return result;
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<HttpResult<long>> CancelWithdrawalAsync(long id, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/v1/dw/withdraw-virtual/{id}/cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendRawAsync<HTXApiResponseV2<long?>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data.Data ?? 0);
        }

        #endregion

        #region Get Withdraw Deposit History

        /// <inheritdoc />
        public async Task<HttpResult<HTXWithdrawDeposit[]>> GetWithdrawDepositHistoryAsync(WithdrawDepositType type, string? asset = null, long? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("type", type);
            parameters.Add("currency", asset);
            parameters.Add("from", from?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/query/deposit-withdraw", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXWithdrawDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fees Rates

        /// <inheritdoc />
        public async Task<HttpResult<HTXFeeRate[]>> GetTradingFeesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.AddParameter("symbols", string.Join(",", symbols.Select(s => s.ToLowerInvariant())));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/reference/transact-fee-rate", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXFeeRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Api Key Info

        /// <inheritdoc />
        public async Task<HttpResult<HTXApiKeyInfo[]>> GetApiKeyInfoAsync(long userId, string? apiKey = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("accessKey", apiKey);
            parameters.Add("uid", userId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/user/api-key", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXApiKeyInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Id

        /// <inheritdoc />
        public async Task<HttpResult<long>> GetUserIdAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/user/uid", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<long?>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion
    }
}
