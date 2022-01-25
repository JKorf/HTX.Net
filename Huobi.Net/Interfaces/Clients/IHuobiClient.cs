using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.SwapsApi;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi API. 
    /// </summary>
    public interface IHuobiClient : IRestClient
    {
        /// <summary>
        /// Spot endpoints
        /// </summary>
        IHuobiClientSpotApi SpotApi { get; }
        /// <summary>
        /// COIN-M futures endpoints
        /// </summary>
        IHuobiClientFuturesCoinApi FuturesCoinApi { get; }
        /// <summary>
        /// COIN-M swaps endpoints
        /// </summary>
        IHuobiClientSwapsCoinApi SwapsCoinApi { get; }
        /// <summary>
        /// USDT-M futures endpoints
        /// </summary>
        IHuobiClientFuturesUsdtApi FuturesUsdtApi { get; }
    }
}