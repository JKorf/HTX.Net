namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    public record HTXBalanceWrapper
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Trade
        /// </summary>
        [JsonPropertyName("trade")]
        public decimal Trade { get; set; }
    }
}
