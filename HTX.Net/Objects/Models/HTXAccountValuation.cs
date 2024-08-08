﻿namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account valuation
    /// </summary>
    public record HTXAccountValuation
    {
        /// <summary>
        /// The balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
