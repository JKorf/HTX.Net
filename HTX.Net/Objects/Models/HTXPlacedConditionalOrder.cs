using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed conditional order
    /// </summary>
    [SerializationModel]
    public record HTXPlacedConditionalOrder
    {
        /// <summary>
        /// The id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
