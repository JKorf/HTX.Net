using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the HTX API. 
    /// </summary>
    public interface IHTXRestClient : IRestClient
    {
        /// <summary>
        /// Spot endpoints
        /// </summary>
        IHTXRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt futures endpoints
        /// </summary>
        IHTXRestClientUsdtFuturesApi UsdtFuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}