using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the HTX API. 
    /// </summary>
    public interface IHTXRestClient : IRestClient<HTXCredentials>
    {
        /// <summary>
        /// Spot endpoints
        /// </summary>
        /// <see cref="IHTXRestClientSpotApi"/>
        IHTXRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt futures endpoints
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesApi"/>
        IHTXRestClientUsdtFuturesApi UsdtFuturesApi { get; }
        /// <summary>
        /// Usdt futures V5 endpoints
        /// </summary>
        /// <see cref="IHTXRestClientUsdtFuturesV5Api"/>
        IHTXRestClientUsdtFuturesV5Api UsdtFuturesV5Api { get; }
    }
}
