using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Order book
    /// </summary>
    public class HuobiOrderBook
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonIgnore]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of bids
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = new List<HuobiOrderBookEntry>();
        /// <summary>
        /// List of asks
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = new List<HuobiOrderBookEntry>();
    }
}
