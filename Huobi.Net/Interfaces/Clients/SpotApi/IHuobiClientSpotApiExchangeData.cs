using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Huobi exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHuobiClientSpotApiExchangeData
    {
        /// <summary>
        /// Gets the latest ticker for all symbols
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTicks>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-aggregated-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-klines-candles" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="limit">The amount of candlesticks</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="limit">The depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the last trade for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the last x trades for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-most-recent-trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSymbolTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets 24h stats for a symbol
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-24h-market-summary" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets real time NAV for ETP
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-real-time-nav" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiNav>> GetNavAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the current market status
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-market-status" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-trading-symbol" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-currencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<string>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies and chains
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#apiv2-currency-amp-chains" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAssetInfo>>> GetAssetDetailsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the server time
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-current-timestamp" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
