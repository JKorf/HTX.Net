using CryptoExchange.Net.Interfaces;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Interfaces.Clients.SpotApi;

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
        /// Usdt margin swap endpoints
        /// </summary>
        HuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }
    }
}