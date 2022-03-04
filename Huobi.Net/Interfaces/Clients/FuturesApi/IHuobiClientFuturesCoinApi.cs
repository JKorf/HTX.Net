using System;

namespace Huobi.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// COIN-M futures API endpoints
    /// </summary>
    public interface IHuobiClientFuturesCoinApi : IDisposable 
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientFuturesCoinApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHuobiClientFuturesCoinApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHuobiClientFuturesCoinApiTrading Trading { get; }
    }
}
