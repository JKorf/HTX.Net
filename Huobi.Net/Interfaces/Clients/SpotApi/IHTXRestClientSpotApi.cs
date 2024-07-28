using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API endpoints
    /// </summary>
    public interface IHTXRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHTXRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHTXRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHTXRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        public ISpotClient CommonSpotClient { get; }
    }
}