using Huobi.Net.Clients.FuturesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiAccount
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiAccount(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

    }
}
