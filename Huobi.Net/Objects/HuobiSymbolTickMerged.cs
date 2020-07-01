using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol tick info
    /// </summary>
    public class HuobiSymbolTickMerged: HuobiSymbolData
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The id of the tick
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// The current best bid for the symbol
        /// </summary>
        [JsonProperty("bid")]
        public HuobiOrderBookEntry BestBid { get; set; } = default!;

        /// <summary>
        /// The current best ask for the symbol
        /// </summary>
        [JsonProperty("ask")]
        public HuobiOrderBookEntry BestAsk { get; set; } = default!;
    }

    /// <summary>
    /// Order book entry
    /// </summary>
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
