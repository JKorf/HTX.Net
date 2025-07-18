using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.RateLimiting.Guards;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApiMargin : IHTXRestClientSpotApiMargin
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientSpotApi _baseClient;

        internal HTXRestClientSpotApiMargin(HTXRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Repay Loan

        /// <inheritdoc />
        public async Task<WebCallResult<HTXRepaymentResult[]>> RepayLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "accountId", accountId },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("transactId", transactionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXRepaymentResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Spot To Isolated Margin

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/dw/transfer-in/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Isolated To Spot Margin

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "v1/dw/transfer-out/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Loan Interest Rate And Quota

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLoanInfo[]>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols == null? null: string.Join(",", symbols.Select(s => s.ToLowerInvariant())));

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXLoanInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Request Isolated Margin Loan

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();
            symbol = symbol.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Repay Isolated Margin Loan

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrder[]>> GetIsolatedMarginClosedOrdersAsync(
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
            symbol = symbol.ToLowerInvariant();

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

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Balance

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginBalances[]>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXMarginBalances[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Spot To Cross Margin

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/transfer-in", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Cross Margin To Spot

        /// <inheritdoc />
        public async Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/transfer-out", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Loan Interest Rate And Quota

        /// <inheritdoc />
        public async Task<WebCallResult<HTXLoanInfoAsset[]>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/cross-margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXLoanInfoAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Request Cross Margin Loan

        /// <inheritdoc />
        public async Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<long>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Repay Cross Margin Loan

        /// <inheritdoc />
        public async Task<WebCallResult> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"v1/cross-margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginOrder[]>> GetCrossMarginClosedOrdersAsync(
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
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("state", EnumConverter.GetString(state));
            parameters.AddOptionalParameter("start-date", startDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("end-date", endDate?.ToString("yyyy-mm-dd"));
            parameters.AddOptionalParameter("from", from);
            parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/cross-margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Balance

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"v1/cross-margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync<HTXMarginBalances>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Limit

        /// <inheritdoc />
        public async Task<WebCallResult<HTXMaxHolding[]>> GetCrossMarginLimitAsync(string? asset = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/margin/limit", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXMaxHolding[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Repayment History

        /// <inheritdoc />
        public async Task<WebCallResult<HTXRepayment[]>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId =null, string? asset =null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("repayId", repayId);
            parameters.AddOptionalParameter("accountId", accountId);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("sort", sort);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXRepayment[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
