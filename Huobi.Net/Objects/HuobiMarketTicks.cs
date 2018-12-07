using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    public class HuobiMarketTicks
    {
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for markets
        /// </summary>
        public List<HuobiMarketTick> Ticks { get; set; }
    }
    
}
