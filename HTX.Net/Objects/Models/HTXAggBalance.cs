using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX aggregated sub account balance
    /// </summary>
    public record HTXAggBalance
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
        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
