using HTX.Net.Enums;
using HTX.Net.Objects.Models;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// HTX exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHTXRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get current system status and announcements
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52f93-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported symbols
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51cb5-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51aee-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol trading configuration
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4f5d6-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSymbolConfig[]>> GetSymbolConfigAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get information on networks
        /// <para><a href="https://www.htx.com/en-in/opend/newApiPages/?id=7ec4f25c-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="descFilter">Description filter</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAssetNetworkInfo[]>> GetNetworksAsync(NetworkRequestFilter? descFilter = null, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get assets and network information
        /// <para><a href="https://www.htx.com/en-in/opend/newApiPages/?id=7ec516fc-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXAssetNetworks[]>> GetAssetsAndNetworksAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the latest ticker for all symbols
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a808-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolTicks>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a3b6-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a4da-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="limit">The amount of candlesticks</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXKline[]>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the order book for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a0fc-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for, for example `ETHUSDT`</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="limit">The depth of the book, 5, 10 or 20</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the last trade for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4aa2b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the last x trades for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a59d-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for, for example `ETHUSDT`</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h stats for a symbol
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4a2cd-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the full order book. Updated once per second and returns up to 5000 levels.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-193426b1a86" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderBook>> GetFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the current market status
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec513b1-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4bb2c-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
