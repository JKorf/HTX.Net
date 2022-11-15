﻿using CryptoExchange.Net.Interfaces;
using Huobi.Net.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt margin swap api endpoints
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApi
    {
        /// <summary>
        /// The factory for creating requests. Used for unit testing
        /// </summary>
        IRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IHuobiClientUsdtMarginSwapApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        HuobiClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        HuobiClientUsdtMarginSwapApiTrading Trading { get; }
    }
}