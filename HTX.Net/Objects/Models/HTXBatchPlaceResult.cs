using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Converters;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Batch placement result
    /// </summary>
    [SerializationModel]
    public record HTXBatchPlaceResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order-id")]
        public long? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client-order-id")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Whether the placement was successful
        /// </summary>
        public bool Success => ErrorCode == null;
        /// <summary>
        /// The error code
        /// </summary>
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// The error message
        /// </summary>
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
    }
}
