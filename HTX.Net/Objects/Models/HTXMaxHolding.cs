namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Max holdings
    /// </summary>
    [SerializationModel]
    public record HTXMaxHolding
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Max holdings
        /// </summary>
        [JsonPropertyName("max-holdings")]
        public decimal MaxHoldings { get; set; }
    }


}
