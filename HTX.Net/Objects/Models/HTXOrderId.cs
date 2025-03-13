using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order-id")]
        public long OrderId { get; set; }
    }
}
