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
        [JsonConverterCtor<ReplaceConverter>($"{HTXExchange.ClientOrderIdPrefix}->")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
