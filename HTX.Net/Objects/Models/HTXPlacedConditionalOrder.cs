using HTX.Net.Converters;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed conditional order
    /// </summary>
    [SerializationModel]
    public record HTXPlacedConditionalOrder
    {
        /// <summary>
        /// ["<c>clientOrderId</c>"] The id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
