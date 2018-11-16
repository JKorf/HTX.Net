using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiMarketDepth
    {
        public List<HuobiOrderBookEntry> Bids { get; set; }
        public List<HuobiOrderBookEntry> Asks { get; set; }
    }
}
