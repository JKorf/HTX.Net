using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiMarketDepth
    {
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
