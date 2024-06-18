using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Symbol ticks
    /// </summary>
    public record HuobiSymbolDatas
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        public IEnumerable<HuobiSymbolTicker> Ticks { get; set; } = Array.Empty<HuobiSymbolTicker>();
    }

    /// <summary>
    /// Symbol ticks
    /// </summary>
    public record HuobiSymbolTicks
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        public IEnumerable<HuobiSymbolTick> Ticks { get; set; } = Array.Empty<HuobiSymbolTick>();
    }
}
