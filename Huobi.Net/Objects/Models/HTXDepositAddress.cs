

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    public record HTXDepositAddress 
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// Block chain name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
    }
}
