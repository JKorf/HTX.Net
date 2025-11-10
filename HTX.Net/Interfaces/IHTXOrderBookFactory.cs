using CryptoExchange.Net.SharedApis;
using HTX.Net.Objects.Options;

namespace HTX.Net.Interfaces
{
    /// <summary>
    /// HTX order book factory
    /// </summary>
    public interface IHTXOrderBookFactory : IExchangeService
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<HTXOrderBookOptions> Spot { get; }
        /// <summary>
        /// Usdt futures order book factory methods
        /// </summary>
        public IOrderBookFactory<HTXOrderBookOptions> UsdtFutures { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<HTXOrderBookOptions>? options = null);

        /// <summary>
        /// Create a SymbolOrderBook for the Spot API
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Order book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<HTXOrderBookOptions>? options = null);

        /// <summary>
        /// Create a SymbolOrderBook for the Usdt futures API
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Order book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateUsdtFutures(string symbol, Action<HTXOrderBookOptions>? options = null);
    }
}
