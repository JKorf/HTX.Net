﻿
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Balance data
    /// </summary>
    public record HTXBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The type of the balance
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BalanceType Type { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// Sequence number of the update
        /// </summary>
        [JsonPropertyName("seq-num")]
        public string SequenceNumber { get; set; } = string.Empty;
    }
}
