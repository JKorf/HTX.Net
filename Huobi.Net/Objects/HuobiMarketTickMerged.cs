using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTickMerged: HuobiMarketData
    {
        public long Version { get; set; }
        
        /// <summary>
        /// The current best bid for the market
        /// </summary>
        [JsonProperty("bid")]
        public HuobiOrderBookEntry BestBid { get; set; }
        /// <summary>
        /// The current best ask for the market
        /// </summary>
        [JsonProperty("ask")]
        public HuobiOrderBookEntry BestAsk { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class HuobiOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The amount for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Amount { get; set; }

    }
}
