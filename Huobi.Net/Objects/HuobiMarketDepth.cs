using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiMarketDepth
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of bids
        /// </summary>
        public List<HuobiOrderBookEntry> Bids { get; set; }
        /// <summary>
        /// List of asks
        /// </summary>
        public List<HuobiOrderBookEntry> Asks { get; set; }
    }
}
