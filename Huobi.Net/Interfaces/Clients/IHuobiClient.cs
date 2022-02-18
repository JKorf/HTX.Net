using CryptoExchange.Net.Interfaces;
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
    }
}