using Huobi.Net.Clients.FuturesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    public class HuobiClientUsdtMarginSwapApiTrading
    {
        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiTrading(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
