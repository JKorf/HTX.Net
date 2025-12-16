namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Current transaction fee rate applied to the user
    /// </summary>
    [SerializationModel]
    public record HTXFeeRate
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Basic fee rate – passive side
        /// </summary>
        [JsonPropertyName("actualMakerRate")]
        public decimal ActualMakerRate { get; set; }

        /// <summary>
        /// Basic fee rate – aggressive side
        /// </summary>
        [JsonPropertyName("actualTakerRate")]
        public decimal ActualTakerRate { get; set; }

        /// <summary>
        /// Deducted fee rate – passive side
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }

        /// <summary>
        /// Basic fee rate – aggressive side
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
    }
}
