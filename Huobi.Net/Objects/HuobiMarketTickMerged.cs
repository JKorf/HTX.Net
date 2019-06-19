using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.OrderBook;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTickMerged: HuobiMarketData
    {
        /// <summary>
        /// The id of the tick
        /// </summary>
        public long Id { get; set; }
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

        [JsonIgnore]
        public DateTime Timestamp { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class HuobiOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

    }
}
