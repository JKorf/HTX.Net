using Huobi.Net.Clients;
using Huobi.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Huobi REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IHuobiRestClient Huobi(this ICryptoRestClient baseClient) => baseClient.TryGet<IHuobiRestClient>(() => new HuobiRestClient());

        /// <summary>
        /// Get the Huobi Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IHuobiSocketClient Huobi(this ICryptoSocketClient baseClient) => baseClient.TryGet<IHuobiSocketClient>(() => new HuobiSocketClient());
    }
}
