namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record HTXBalanceWrapper
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>trade</c>"] Trade
        /// </summary>
        [JsonPropertyName("trade")]
        public decimal Trade { get; set; }
    }
}
