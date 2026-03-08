namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Max holdings
    /// </summary>
    [SerializationModel]
    public record HTXMaxHolding
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>max-holdings</c>"] Max holdings
        /// </summary>
        [JsonPropertyName("max-holdings")]
        public decimal MaxHoldings { get; set; }
    }


}
