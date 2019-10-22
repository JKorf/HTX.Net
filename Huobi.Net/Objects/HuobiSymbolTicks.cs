using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol ticks
    /// </summary>
    public class HuobiSymbolTicks
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        public IEnumerable<HuobiSymbolTick> Ticks { get; set; } = new List<HuobiSymbolTick>();
    }
    
}
