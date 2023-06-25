using CryptoExchange.Net.Interfaces;
using Huobi.Net.Objects.Options;
using System;

namespace Huobi.Net.Interfaces
{
    /// <summary>
    /// Huobi order book factory
    /// </summary>
    public interface IHuobiOrderBookFactory
    {
        /// <summary>
        /// Create a SymbolOrderBook for the Spot API
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Order book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<HuobiOrderBookOptions>? options = null);
    }
}
