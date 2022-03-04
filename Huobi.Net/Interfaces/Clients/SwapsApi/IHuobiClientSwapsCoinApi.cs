using System;

namespace Huobi.Net.Interfaces.Clients.SwapsApi
{
    /// <summary>
    /// COIN-M swaps API endpoints
    /// </summary>
    public interface IHuobiClientSwapsCoinApi : IDisposable 
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientSwapsCoinApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHuobiClientSwapsCoinApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHuobiClientSwapsCoinApiTrading Trading { get; }
    }
}
