using CryptoExchange.Net.Interfaces;
using Huobi.Net.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt margin swap api endpoints
    /// </summary>
    public interface IHuobiRestClientUsdtMarginSwapApi : IRestApiClient
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiRestClientUsdtMarginSwapApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHuobiRestClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHuobiRestClientUsdtMarginSwapApiTrading Trading { get; }
    }
}