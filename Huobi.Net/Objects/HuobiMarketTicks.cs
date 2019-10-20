using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Market ticks
    /// </summary>
    public class HuobiMarketTicks
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for markets
        /// </summary>
        public IEnumerable<HuobiMarketTick> Ticks { get; set; } = new List<HuobiMarketTick>();
    }
    
}
