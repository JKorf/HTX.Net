using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi API. 
    /// </summary>
    public interface IHuobiRestClient : IRestClient
    {
        /// <summary>
        /// Spot endpoints
        /// </summary>
        IHuobiClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt margin swap endpoints
        /// </summary>
        IHuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}