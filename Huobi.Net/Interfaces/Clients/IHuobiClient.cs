using CryptoExchange.Net.Interfaces;

namespace Huobi.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Client for accessing the Huobi API. 
    /// </summary>
    public interface IHuobiClient : IRestClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientSpot SpotApi { get; }
    }
}