using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi websocket API. 
    /// </summary>
    public interface IHuobiSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        public IHuobiSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt margin swap streams
        /// </summary>
        public IHuobiSocketClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}