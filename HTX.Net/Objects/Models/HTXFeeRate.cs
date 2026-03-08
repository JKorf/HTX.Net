namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Current transaction fee rate applied to the user
    /// </summary>
    [SerializationModel]
    public record HTXFeeRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>actualMakerRate</c>"] Basic fee rate – passive side
        /// </summary>
        [JsonPropertyName("actualMakerRate")]
        public decimal ActualMakerRate { get; set; }

        /// <summary>
        /// ["<c>actualTakerRate</c>"] Basic fee rate – aggressive side
        /// </summary>
        [JsonPropertyName("actualTakerRate")]
        public decimal ActualTakerRate { get; set; }

        /// <summary>
        /// ["<c>makerFeeRate</c>"] Deducted fee rate – passive side
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }

        /// <summary>
        /// ["<c>takerFeeRate</c>"] Basic fee rate – aggressive side
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
    }
}
