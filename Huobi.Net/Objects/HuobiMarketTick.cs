using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTick: HuobiMarketData
    {
        public string Symbol { get; set; }
    }
}
