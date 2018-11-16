using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
{
    public class HuobiSymbol
    {
        public string Symbol { get; set; }
        [JsonProperty("base-currency")]
        public string BaseCurrency { get; set; }
        [JsonProperty("quote-currency")]
        public string QuoteCurrency { get; set; }
        [JsonProperty("price-precision")]
        public int PricePrecision { get; set; }
        [JsonProperty("amount-precision")]
        public int AmountPrecision { get; set; }
        [JsonProperty("symbol-partition")]
        public string SymbolPartition { get; set; }
    }
}
