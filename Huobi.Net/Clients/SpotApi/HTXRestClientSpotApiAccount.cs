using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;

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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/account/accounts", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAccount>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/accounts/{accountId}/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/asset-valuation", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<HTXAccountValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<HTXTransactionResult>> TransferSubAccountAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default)
        {
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
            return await _baseClient.SendBasicAsync<HTXTransactionResult>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, CancellationToken ct = default)
        {
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/history", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXAccountHistory>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/ledger", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXLedgerEntry>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /v2/account/transfer
        // /v1/futures/transfer
        // /v2/point/account
        // /v2/point/transfer
        // /v1/account/switch/user/info
        // /v1/account/overview/info
        // /v1/account/fee/switch

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection() { { "currency", asset } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/deposit/address", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /v2/account/withdraw/quota
        // /v2/account/withdraw/address

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, string? clientOrderId = null, CancellationToken ct = default)
        {
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/dw/withdraw/api/create", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        // /v1/query/withdraw/client-order-id
        // /v1/dw/withdraw-virtual/{withdraw-id}/cancel

        #region Get Withdraw Deposit

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("from", from?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("direct", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/query/deposit-withdraw", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXWithdrawDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Fee Rates

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXFeeRate>>> GetFeeRatesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", string.Join(",", symbols));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/reference/transact-fee-rate", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXFeeRate>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        // Split User Accounts to seperate topic
        // Split Margin Loans?


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXRepaymentResult>>> RepayMarginLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "accountId", accountId },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("transactId", transactionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXRepaymentResult>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/dw/transfer-in/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/dw/transfer-out/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXLoanInfo>>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols == null? null: string.Join(",", symbols));

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXLoanInfo>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(
            string symbol, 
            IEnumerable<MarginOrderStatus>? states = null, 
            DateTime? startDate = null, 
            DateTime? endDate= null, 
            string? from = null, 
            FilterDirection? direction = null, 
            int? limit = null, 
            int? subUserId = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            parameters.AddOptionalParameter("states", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("start-date", startDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("end-date", endDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("from", from);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXMarginBalances>>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXMarginBalances>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/transfer-in", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/transfer-out", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXLoanInfoAsset>>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/cross-margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXLoanInfoAsset>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXMarginOrder>>> GetCrossMarginClosedOrdersAsync(
            string? asset = null,
            MarginOrderStatus? state = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("state", EnumConverter.GetString(state));
            parameters.AddOptionalParameter("start-date", startDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("end-date", endDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("from", from);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/cross-margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXMarginOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/cross-margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<HTXMarginBalances>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXRepayment>>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId =null, string? asset =null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("repayId", repayId);
            parameters.AddOptionalParameter("accountId", accountId);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("sort", sort);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXRepayment>>(request, null, ct).ConfigureAwait(false);
        }
        


        /// <inheritdoc />
        public async Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/user/uid", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXUser>>> GetSubAccountUsersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/sub-user/user-list", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<IEnumerable<HTXUser>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "subUid", subUserId.ToString(CultureInfo.InvariantCulture)}
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/sub-user/account-list", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendAsync<HTXSubUserAccounts>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/account/accounts/{subAccountId}", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            var result = await _baseClient.SendBasicAsync<IEnumerable<HTXAccountBalances>>(request, null, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<IEnumerable<HTXBalance>>(result.Error!);

            return result.As(result.Data.First().Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection()
            {
                { "sub-uid", subAccountId },
                { "currency", asset },
                { "amount", quantity }
            };
            parameters.AddEnum("type", transferType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/subuser/transfer", HTXExchange.RateLimiter.EndpointLimit, 1, true);
            return await _baseClient.SendBasicAsync<long>(request, null, ct).ConfigureAwait(false);
        }
    }
}
