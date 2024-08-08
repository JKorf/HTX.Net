using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.RateLimiting.Guards;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApiSubAccount : IHTXRestClientSpotApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientSpotApi _baseClient;

        internal HTXRestClientSpotApiSubAccount(HTXRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Set Deduct Mode

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSubDeductMode>>> SetDeductModeAsync(IEnumerable<string> subUserIds, DeductMode deductMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUids", string.Join(",", subUserIds));
            parameters.AddEnum("deductMode", deductMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/deduct-mode", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXSubDeductMode>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Create Sub Account

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSubAccountInfo>>> CreateSubAccountsAsync(IEnumerable<HTXSubAccountRequest> accounts, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("userList", accounts);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/creation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXSubAccountInfo>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub User List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXUser>>> GetSubUserListAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/sub-user/user-list", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<IEnumerable<HTXUser>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Lock

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubAccountLock>> SetLockAsync(long subUserId, LockAction lockAction, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            parameters.AddEnum("action", lockAction);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/management", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXSubAccountLock>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub User

        /// <inheritdoc />
        public async Task<WebCallResult<HTXUser>> GetSubUserAsync(long subUserId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/sub-user/user-state", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXUser>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Tradable Market

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSubMarketTradable>>> SetTradableMarketAsync(IEnumerable<string> subUserIds, SubAccountMarketType accountType, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUids", string.Join(",", subUserIds));
            parameters.AddEnum("accountType", accountType);
            parameters.Add("enabled", enabled ? "activated": "deactivated");
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/tradable-market", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXSubMarketTradable>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Asset Transfer Permissions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSubTransferPermission>>> SetAssetTransferPermissionsAsync(IEnumerable<string> subUserIds, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUids", string.Join(",", subUserIds));
            parameters.Add("transferrable", enabled);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/transferability", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXSubTransferPermission>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub User Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "subUid", subUserId.ToString(CultureInfo.InvariantCulture)}
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/sub-user/account-list", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXSubUserAccounts>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create Api Key

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubApiKey>> CreateApiKeyAsync(string otpToken, long subUserId, string note, IEnumerable<string> permissions, IEnumerable<string> ipAddresses, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("otpToken", otpToken);
            parameters.Add("subUid", subUserId);
            parameters.Add("note", note);
            parameters.Add("permission", string.Join(",", permissions));
            parameters.Add("ipAddresses", string.Join(",", ipAddresses));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/api-key-generation", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXSubApiKey>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Api Key

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubApiKeyEdit>> EditApiKeyAsync(long subUserId, string apiKey, string note, IEnumerable<string> permissions, IEnumerable<string> ipAddresses, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            parameters.Add("accessKey", apiKey);
            parameters.Add("note", note);
            parameters.Add("permission", string.Join(",", permissions));
            parameters.Add("ipAddresses", string.Join(",", ipAddresses));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/api-key-modification", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXSubApiKeyEdit>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Delete Api Key

        /// <inheritdoc />
        public async Task<WebCallResult> DeleteApiKeyAsync(long subUserId, string apiKey, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            parameters.Add("accessKey", apiKey);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v2/sub-user/api-key-deletion", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<object>(request, parameters, ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion

        #region Transfer With Sub Account

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "sub-uid", subAccountId },
                { "currency", asset },
                { "amount", quantity }
            };
            parameters.AddEnum("type", transferType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/subuser/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXDepositAddress>>> GetDepositAddressAsync(long subUserId, string asset, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/sub-user/deposit-address", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSubDeposit>>> GetDepositHistoryAsync(long subUserId, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.Add("subUid", subUserId);
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptionalEnum("sort", sort);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("fromId", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/sub-user/query-deposit", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<IEnumerable<HTXSubDeposit>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Aggregate Balances

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAggBalance>>> GetAggregateBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v1/subuser/aggregate-balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<IEnumerable<HTXAggBalance>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAccountBalances>>> GetBalancesAsync(long subAccountId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/accounts/{subAccountId}", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAccountBalances>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion


    }
}
