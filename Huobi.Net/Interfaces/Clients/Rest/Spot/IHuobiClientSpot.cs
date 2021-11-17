using CryptoExchange.Net.Interfaces;

namespace Huobi.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Interface for the Huobi client
    /// </summary>
    public interface IHuobiClientSpot : IRestClient
    {
        IHuobiClientSpotAccount Account { get; }
        IHuobiClientSpotExchangeData ExchangeData { get; }
        IHuobiClientSpotTrading Trading { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}