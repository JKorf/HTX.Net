
using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Incremental order book update
    /// </summary>
    public record HTXUsdtMarginSwapIncementalOrderBook
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// List of changed bids
        /// </summary>
        public IEnumerable<HTXOrderBookEntry> Bids { get; set; } = Array.Empty<HTXOrderBookEntry>();
        /// <summary>
        /// List of changed asks
        /// </summary>
        public IEnumerable<HTXOrderBookEntry> Asks { get; set; } = Array.Empty<HTXOrderBookEntry>();
    }
}
