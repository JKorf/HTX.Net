using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the HTX websocket API. 
    /// </summary>
    public interface IHTXSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        public IHTXSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt margin swap streams
        /// </summary>
        public IHTXSocketClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}