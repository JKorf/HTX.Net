using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTickMerged: HuobiMarketData
    {
        public long Version { get; set; }
        
        [JsonProperty("bid")]
        public HuobiOrderBookEntry BestBid { get; set; }
        [JsonProperty("ask")]
        public HuobiOrderBookEntry BestAsk { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class HuobiOrderBookEntry
    {
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        [ArrayProperty(1)]
        public decimal Amount { get; set; }

    }
}
