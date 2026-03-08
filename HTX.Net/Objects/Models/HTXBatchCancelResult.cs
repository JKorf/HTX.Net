using HTX.Net.Converters;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Result of a batch cancel
    /// </summary>
    [SerializationModel]
    public record HTXBatchCancelResult
    {
        /// <summary>
        /// ["<c>success</c>"] Orders that were successfully canceled
        /// </summary>
        [JsonPropertyName("success")]
        public string[] Successful { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>failed</c>"] Orders that failed to cancel
        /// </summary>
        [JsonPropertyName("failed")]
        public HTXFailedCancelResult[] Failed { get; set; } = Array.Empty<HTXFailedCancelResult>();
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    [SerializationModel]
    public record HTXFailedCancelResult
    {
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
        /// <summary>
        /// ["<c>order-id</c>"] The id of the failed order
        /// </summary>
        [JsonPropertyName("order-id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order-state</c>"] The state of the order
        /// </summary>
        [JsonPropertyName("order-state")]
        public int? OrderStatus { get; set; }
        /// <summary>
        /// ["<c>client-order-id</c>"] The id of the failed order
        /// </summary>
        [JsonPropertyName("client-order-id")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }
    }
}
