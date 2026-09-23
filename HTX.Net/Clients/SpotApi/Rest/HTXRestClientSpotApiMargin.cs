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
        public async Task<HttpResult<HTXRepaymentResult[]>> RepayLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "accountId", accountId },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.Add("transactId", transactionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXRepaymentResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Spot To Isolated Margin

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/dw/transfer-in/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Transfer Isolated To Spot Margin

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "v1/dw/transfer-out/margin", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Isolated Loan Interest Rate And Quota

        /// <inheritdoc />
        public async Task<HttpResult<HTXLoanInfo[]>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("symbols", symbols == null? null: string.Join(",", symbols.Select(s => s.ToLowerInvariant())));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXLoanInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Request Isolated Margin Loan

        /// <inheritdoc />
        public async Task<HttpResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();
            symbol = symbol.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Repay Isolated Margin Loan

        /// <inheritdoc />
        public async Task<HttpResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Isolated Margin Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginOrder[]>> GetIsolatedMarginClosedOrdersAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            parameters.Add("states", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
            parameters.Add("start-date", startDate?.ToString("yyyy-mm-dd"));
            parameters.Add("end-date", endDate?.ToString("yyyy-mm-dd"));
            parameters.Add("from", from);
            parameters.Add("direct", EnumConverter.GetString(direction));
            parameters.Add("size", limit);
            parameters.Add("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Balance

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginBalances[]>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            parameters.Add("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXMarginBalances[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer Spot To Cross Margin

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/cross-margin/transfer-in", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Transfer Cross Margin To Spot

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "currency", asset },
                { "amount", quantity },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/cross-margin/transfer-out", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Cross Loan Interest Rate And Quota

        /// <inheritdoc />
        public async Task<HttpResult<HTXLoanInfoAsset[]>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/cross-margin/loan-info", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXLoanInfoAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Request Cross Margin Loan

        /// <inheritdoc />
        public async Task<HttpResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            asset = asset.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "currency", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/cross-margin/orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendBasicAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Repay Cross Margin Loan

        /// <inheritdoc />
        public async Task<HttpResult> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"v1/cross-margin/orders/{orderId}/repay", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendBasicAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginOrder[]>> GetCrossMarginClosedOrdersAsync(
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

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("state", EnumConverter.GetString(state));
            parameters.Add("start-date", startDate?.ToString("yyyy-mm-dd"));
            parameters.Add("end-date", endDate?.ToString("yyyy-mm-dd"));
            parameters.Add("from", from);
            parameters.Add("direct", EnumConverter.GetString(direction));
            parameters.Add("size", limit);
            parameters.Add("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/cross-margin/loan-orders", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXMarginOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Balance

        /// <inheritdoc />
        public async Task<HttpResult<HTXMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("sub-uid", subUserId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"v1/cross-margin/accounts/balance", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXMarginBalances>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Limit

        /// <inheritdoc />
        public async Task<HttpResult<HTXMaxHolding[]>> GetCrossMarginLimitAsync(string? asset = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/margin/limit", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<HTXMaxHolding[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Repayment History

        /// <inheritdoc />
        public async Task<HttpResult<HTXRepayment[]>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId =null, string? asset =null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("repayId", repayId);
            parameters.Add("accountId", accountId);
            parameters.Add("currency", asset);
            parameters.Add("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.Add("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("sort", sort);
            parameters.Add("limit", limit);
            parameters.Add("fromId", fromId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/account/repayment", HTXExchange.RateLimiter.EndpointLimit, 1, true,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<HTXRepayment[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
