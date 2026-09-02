using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.RateLimiting.Guards;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class HTXRestClientSpotApiExchangeData : IHTXRestClientSpotApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly HTXRestClientSpotApi _baseClient;

        internal HTXRestClientSpotApiExchangeData(HTXRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get System Status

        /// <inheritdoc />
        public async Task<HttpResult<HTXSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "https://status.huobigroup.com", "api/v2/summary.json", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendRawAsync<HTXSystemStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Status

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/market-status", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<HTXSymbolStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/settings/common/symbols", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendBasicAsync<HTXSymbol[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<HttpResult<HTXAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/settings/common/currencies", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendBasicAsync<HTXAsset[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Config

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolConfig[]>> GetSymbolConfigAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("symbols", symbols == null ? null : string.Join(",", symbols.Select(s => s.ToLowerInvariant())));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v1/settings/common/market-symbols", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendBasicAsync<HTXSymbolConfig[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Networks

        /// <inheritdoc />
        public async Task<HttpResult<HTXAssetNetworkInfo[]>> GetNetworksAsync(NetworkRequestFilter? descFilter = null, string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("show-desc", descFilter);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v1/settings/common/chains", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendBasicAsync<HTXAssetNetworkInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets And Networks

        /// <inheritdoc />
        public async Task<HttpResult<HTXAssetNetworks[]>> GetAssetsAndNetworksAsync(string? asset = null, CancellationToken ct = default)
        {
            asset = asset?.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/reference/currencies", HTXExchange.RateLimiter.EndpointLimit, 1, false,
                new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<HTXAssetNetworks[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v1/common/timestamp", HTXExchange.RateLimiter.EndpointLimit, 1, false, preventCaching: true,
                limitGuard: new SingleLimitGuard(100, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendBasicAsync<long?>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            var time = DateTimeConverter.ParseFromDouble(result.Data!.Value)!;
            return HttpResult.Ok(result, time);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<HTXKline[]>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
            };
            parameters.Add("period", period);
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/history/kline", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendBasicAsync<HTXKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/detail/merged", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendTimestampAsync<HTXSymbolTickMerged>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXSymbolTickMerged>(result);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return HttpResult.Ok(result, result.Data.Item1);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/tickers", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendTimestampAsync<HTXSymbolTick[]>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXSymbolTicks>(result);

            return HttpResult.Ok(result, new HTXSymbolTicks() { Ticks = result.Data.Item1, Timestamp = result.Data.Item2 });
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
                { "type", "step"+mergeStep }
            };
            parameters.Add("depth", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/depth", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendTimestampAsync<HTXOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXOrderBook>(result);

            return HttpResult.Ok(result, result.Data.Item1);
        }

        #endregion

        #region Get Last Trade

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/trade", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendBasicAsync<HTXSymbolTrade>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol },
            };
            parameters.Add("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/history/trade", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(3000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendBasicAsync<HTXSymbolTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbol Details 24H

        /// <inheritdoc />
        public async Task<HttpResult<HTXSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();
            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/detail", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(4500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendTimestampAsync<HTXSymbolDetails>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXSymbolDetails>(result);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return HttpResult.Ok(result, result.Data.Item1);
        }

        #endregion

        #region Get Full Order Book

        /// <inheritdoc />
        public async Task<HttpResult<HTXOrderBook>> GetFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ToLowerInvariant();

            var parameters = new Parameters(HTXExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "market/fullMbp ", HTXExchange.RateLimiter.SpotMarketLimit, 1, false,
                new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendTimestampAsync<HTXOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<HTXOrderBook>(result);

            return HttpResult.Ok(result, result.Data.Item1);
        }

        #endregion

    }
}
