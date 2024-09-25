using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Internal;
using CryptoExchange.Net.RateLimiting.Guards;

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
        public async Task<WebCallResult<IEnumerable<HTXAccount>>> GetAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/account/accounts", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAccount>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances
        
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/accounts/{accountId}/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<HTXAccountBalances>(request, null, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<IEnumerable<HTXBalance>>(result.Error!);

            return result.As(result.Data.Data);
        }

        #endregion

        #region Get Platform Valuation

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPlatformValuation>> GetPlatformValuationAsync(AccountType? accountType = null, string? valuationAsset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("accountType", accountType);
            parameters.AddOptionalParameter("valuationCurrency", valuationAsset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(3, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXPlatformValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Valuation

        /// <inheritdoc />
        public async Task<WebCallResult<HTXAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("accountType", accountType);
            parameters.AddOptionalParameter("valuationCurrency", valuationCurrency);
            parameters.AddOptionalParameter("subUid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/asset-valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXAccountValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Internal Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTransactionResult>> InternalTransferAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "from-account-id", fromAccountId.ToString(CultureInfo.InvariantCulture)},
                { "from-user", fromUserId.ToString(CultureInfo.InvariantCulture)},

                { "to-account-id", toAccountId.ToString(CultureInfo.InvariantCulture)},
                { "to-user", toUserId.ToString(CultureInfo.InvariantCulture)},

                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddEnum("from-account-type", fromAccountType);
            parameters.AddEnum("to-account-type", toAccountType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/account/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXTransactionResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new ParameterCollection()
            {
                { "account-id", accountId }
            };
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("transact-types", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => EnumConverter.GetString(s))));
            parameters.AddOptionalParameter("start-time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end-time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalEnum("sort", sort);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/history", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAccountHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new ParameterCollection()
            {
                { "accountId", accountId }
            };
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("transactTypes", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => EnumConverter.GetString(s))));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("sort", sort);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/ledger", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<IEnumerable<HTXLedgerEntry>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferAsync(TransferAccount fromAccount, TransferAccount toAccount, string asset, decimal quantity, string marginAccount, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddEnum("from", fromAccount);
            parameters.AddEnum("to", toAccount);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("margin-account", marginAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/account/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Point Balance

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPointBalance>> GetPointBalanceAsync(string? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("subUid", subUserId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/point/account", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXPointBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Points

        /// <inheritdoc />
        public async Task<WebCallResult<HTXPointTransfer>> TransferPointsAsync(string fromUserId, string toUserId, string groupId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("fromUid", fromUserId);
            parameters.Add("toUid", toUserId);
            parameters.Add("groupId", groupId);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/point/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXPointTransfer>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Deduction Info

        /// <inheritdoc />
        public async Task<WebCallResult<HTXDeductInfo>> GetUserDeductionInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v1/account/switch/user/info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXDeductInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deduct Assets

        /// <inheritdoc />
        public async Task<WebCallResult<HTXDeductionAssets>> GetDeductAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v1/account/overview/info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXDeductionAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Deduction Switch

        /// <inheritdoc />
        public async Task<WebCallResult> SetDeductionSwitchAsync(DeductionSwitchType switchType, string? deductionAsset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnumAsInt("switchType", switchType);
            parameters.AddOptional("deductionCurrency", deductionAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v1/account/fee/switch", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<object>(request, parameters, ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection() { { "currency", asset } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/deposit/address", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<IEnumerable<HTXDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawal Quotas

        /// <inheritdoc />
        public async Task<WebCallResult<HTXWithdrawalQuota>> GetWithdrawalQuotasAsync(string? asset = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/account/withdraw/quota", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXWithdrawalQuota>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXWithdrawalAddress>>> GetWithdrawalAddressesAsync(string asset, string? network = null, string? note = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddOptional("chain", network);
            parameters.AddOptional("note", note);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("fromId", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/account/withdraw/address", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXWithdrawalAddress>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "address", address },
                { "currency", asset },
                { "amount", quantity },
                { "fee", fee },
            };

            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("addr-tag", addressTag);
            parameters.AddOptionalParameter("client-order-id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/dw/withdraw/api/create", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawal By Client Order Id

        /// <inheritdoc />
        public async Task<WebCallResult<HTXWithdrawDeposit>> GetWithdrawalByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v1/query/withdraw/client-order-id", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<HTXWithdrawDeposit>(request, parameters, ct).ConfigureAwait(false);
            if (result.Data == null)
                return new WebCallResult<HTXWithdrawDeposit>(new ServerError("Not found"));

            return result;
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<long>> CancelWithdrawalAsync(long id, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v1/dw/withdraw-virtual/{id}/cancel", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendToAddressRawAsync<HTXApiResponseV2<long>>(_baseClient.BaseAddress, request, parameters, ct).ConfigureAwait(false);
            return result.As<long>(result.Data?.Data ?? default);
        }

        #endregion

        #region Get Withdraw Deposit History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXWithdrawDeposit>>> GetWithdrawDepositHistoryAsync(WithdrawDepositType type, string? asset = null, long? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("from", from?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/query/deposit-withdraw", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<IEnumerable<HTXWithdrawDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fees Rates

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXFeeRate>>> GetTradingFeesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbols", string.Join(",", symbols.Select(s => s.ToLowerInvariant())));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/reference/transact-fee-rate", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<IEnumerable<HTXFeeRate>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Api Key Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXApiKeyInfo>>> GetApiKeyInfoAsync(long userId, string? apiKey = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("accessKey", apiKey);
            parameters.Add("uid", userId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/user/api-key", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXApiKeyInfo>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Id

        /// <inheritdoc />
        public async Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/user/uid", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
