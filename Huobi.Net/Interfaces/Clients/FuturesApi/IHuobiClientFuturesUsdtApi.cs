using System;

namespace Huobi.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// USDT-M futures API endpoints
    /// </summary>
    public interface IHuobiClientFuturesUsdtApi : IDisposable 
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientFuturesUsdtApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IHuobiClientFuturesUsdtApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IHuobiClientFuturesUsdtApiTrading Trading { get; }
    }
}
