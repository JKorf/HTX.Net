using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.ExtensionMethods;
using System.Security.Cryptography;

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
        public async Task<WebCallResult<HTXSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v2/summary.json", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendToAddressRawAsync<HTXSystemStatus>("https://status.huobigroup.com", request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/settings/common/symbols", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendBasicAsync<IEnumerable<HTXSymbol>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/settings/common/currencies", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendBasicAsync<IEnumerable<HTXAsset>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Config

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSymbolConfig>>> GetSymbolConfigAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v1/settings/common/market-symbols", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendBasicAsync<IEnumerable<HTXSymbolConfig>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


        /// <inheritdoc />
        public async Task<WebCallResult<HTXSymbolTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/tickers", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendTimestampAsync<IEnumerable<HTXSymbolTick>>(request, null, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXSymbolTicks>(result.Error!);

            return result.As(new HTXSymbolTicks() { Ticks = result.Data.Item1, Timestamp = result.Data.Item2 });
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/detail/merged", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendTimestampAsync<HTXSymbolTickMerged>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXSymbolTickMerged>(result.Error!);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
            };
            parameters.AddEnum("period", period);
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/history/kline", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXKline>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 2000);
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "type", "step"+mergeStep }
            };
            parameters.AddOptionalParameter("depth", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/depth", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendTimestampAsync<HTXOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXOrderBook>(result.Error!);

            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/trade", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            return await _baseClient.SendAsync<HTXSymbolTrade>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXSymbolTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
            };
            parameters.AddOptionalParameter("size", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/history/trade", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            return await _baseClient.SendBasicAsync<IEnumerable<HTXSymbolTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/detail", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendTimestampAsync<HTXSymbolDetails>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXSymbolDetails>(result.Error!);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXNav>> GetNavAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHTXSymbol();
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "market/etp", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendTimestampAsync<HTXNav>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HTXNav>(result.Error!);

            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HTXSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/market-status", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            return await _baseClient.SendAsync<HTXSymbolStatus>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HTXAssetInfo>>> GetAssetDetailsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/common/currencys", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            return await _baseClient.SendAsync<IEnumerable<HTXAssetInfo>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/common/timestamp", HTXExchange.RateLimiter.EndpointLimit, 1, false);
            var result = await _baseClient.SendBasicAsync<string>(request, null, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<DateTime>(result.Error!);
            var time = DateTimeConverter.ParseFromString(result.Data)!;
            return result.As(time);
        }

    }
}
