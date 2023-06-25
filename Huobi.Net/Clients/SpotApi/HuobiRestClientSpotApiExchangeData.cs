using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;
using Huobi.Net.Interfaces.Clients.SpotApi;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiRestClientSpotApiExchangeData : IHuobiClientSpotApiExchangeData
    {
        private const string MarketTickerEndpoint = "market/tickers";
        private const string MarketTickerMergedEndpoint = "market/detail/merged";
        private const string MarketKlineEndpoint = "market/history/kline";
        private const string MarketDepthEndpoint = "market/depth";
        private const string MarketLastTradeEndpoint = "market/trade";
        private const string MarketTradeHistoryEndpoint = "market/history/trade";
        private const string MarketDetailsEndpoint = "market/detail";
        private const string NavEndpoint = "market/etp";

        private const string MarketStatusEndpoint = "market-status";
        private const string CommonSymbolsEndpoint = "common/symbols";
        private const string CommonCurrenciesEndpoint = "common/currencys";
        private const string CommonCurrenciesAndChainsEndpoint = "reference/currencies";
        private const string ServerTimeEndpoint = "common/timestamp";

        private readonly HuobiRestClientSpotApi _baseClient;

        internal HuobiRestClientSpotApiExchangeData(HuobiRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSymbolTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimestampRequest<IEnumerable<HuobiSymbolTick>>(_baseClient.GetUrl(MarketTickerEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiSymbolTicks>(result.Error!);

            return result.As(new HuobiSymbolTicks() { Ticks = result.Data.Item1, Timestamp = result.Data.Item2 });
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await _baseClient.SendHuobiTimestampRequest<HuobiSymbolTickMerged>(_baseClient.GetUrl(MarketTickerMergedEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiSymbolTickMerged>(result.Error!);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl(MarketKlineEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 2000);
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", "step"+mergeStep }
            };
            parameters.AddOptionalParameter("depth", limit);

            var result = await _baseClient.SendHuobiTimestampRequest<HuobiOrderBook>(_baseClient.GetUrl(MarketDepthEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiOrderBook>(result.Error!);

            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await _baseClient.SendHuobiRequest<HuobiSymbolTrade>(_baseClient.GetUrl(MarketLastTradeEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSymbolTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            limit?.ValidateIntBetween(nameof(limit), 0, 2000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
            };
            parameters.AddOptionalParameter("size", limit);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSymbolTrade>>(_baseClient.GetUrl(MarketTradeHistoryEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await _baseClient.SendHuobiTimestampRequest<HuobiSymbolDetails>(_baseClient.GetUrl(MarketDetailsEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiSymbolDetails>(result.Error!);

            result.Data.Item1.Timestamp = result.Data.Item2;
            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiNav>> GetNavAsync(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await _baseClient.SendHuobiTimestampRequest<HuobiNav>(_baseClient.GetUrl(NavEndpoint), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiNav>(result.Error!);

            return result.As(result.Data.Item1);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiV2Request<HuobiSymbolStatus>(_baseClient.GetUrl(MarketStatusEndpoint, "2"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSymbol>>(_baseClient.GetUrl(CommonSymbolsEndpoint, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<string>>(_baseClient.GetUrl(CommonCurrenciesEndpoint, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiAssetInfo>>> GetAssetDetailsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.SendHuobiV2Request<IEnumerable<HuobiAssetInfo>>(_baseClient.GetUrl(CommonCurrenciesAndChainsEndpoint, "2"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiRequest<string>(_baseClient.GetUrl(ServerTimeEndpoint, "1"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
            if (!result)
                return result.AsError<DateTime>(result.Error!);
            var time = (DateTime)JsonConvert.DeserializeObject(result.Data, typeof(DateTime), new DateTimeConverter())!;
            return result.As(time);
        }

    }
}
