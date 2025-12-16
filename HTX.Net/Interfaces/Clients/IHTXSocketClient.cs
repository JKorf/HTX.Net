using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

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
        /// <see cref="IHTXSocketClientSpotApi"/>
        public IHTXSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt futures streams
        /// </summary>
        /// <see cref="IHTXSocketClientUsdtFuturesApi"/>
        public IHTXSocketClientUsdtFuturesApi UsdtFuturesApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}