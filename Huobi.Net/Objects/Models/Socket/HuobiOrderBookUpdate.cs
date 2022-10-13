using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Incremental order book update
    /// </summary>
    public class HuobiUsdtMarginSwapIncementalOrderBook
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// List of changed bids
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();
        /// <summary>
        /// List of changed asks
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();
    }
}
