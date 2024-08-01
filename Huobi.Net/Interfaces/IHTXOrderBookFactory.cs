using HTX.Net.Objects.Options;

namespace HTX.Net.Interfaces
{
    /// <summary>
    /// HTX order book factory
    /// </summary>
    public interface IHTXOrderBookFactory
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<HTXOrderBookOptions> Spot { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the Spot API
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Order book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<HTXOrderBookOptions>? options = null);
    }
}
