namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed conditional order
    /// </summary>
    public record HTXPlacedConditionalOrder
    {
        /// <summary>
        /// The id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
