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
        /// ["<c>order-id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order-id")]
        public long? OrderId { get; set; }
        /// <summary>
        /// ["<c>client-order-id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client-order-id")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Whether the placement was successful
        /// </summary>
        public bool Success => ErrorCode == null;
        /// <summary>
        /// ["<c>err-code</c>"] The error code
        /// </summary>
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>err-msg</c>"] The error message
        /// </summary>
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
    }
}
