using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol ticks
    /// </summary>
    public record HTXSymbolDatas
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        [JsonPropertyName("ticks")]
        public IEnumerable<HTXSymbolTicker> Ticks { get; set; } = Array.Empty<HTXSymbolTicker>();
    }

    /// <summary>
    /// Symbol ticks
    /// </summary>
    public record HTXSymbolTicks
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        [JsonPropertyName("ticks")]
        public IEnumerable<HTXSymbolTick> Ticks { get; set; } = Array.Empty<HTXSymbolTick>();
    }
}
