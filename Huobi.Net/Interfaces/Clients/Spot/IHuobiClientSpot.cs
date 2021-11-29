using CryptoExchange.Net.Interfaces;

namespace Huobi.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Client for accessing the Huobi API. 
    /// </summary>
    public interface IHuobiClientSpot
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientSpotAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHuobiClientSpotExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHuobiClientSpotTrading Trading { get; }
    }
}