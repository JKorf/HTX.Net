using Huobi.Net.Clients;
using Huobi.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    public static class CryptoExchangeClientExtensions
    {
        public static IHuobiRestClient Huobi(this ICryptoExchangeClient baseClient) => baseClient.TryGet<IHuobiRestClient>() ?? new HuobiRestClient();
    }
}
