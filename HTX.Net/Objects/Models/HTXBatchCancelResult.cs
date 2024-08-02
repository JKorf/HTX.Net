namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Result of a batch cancel
    /// </summary>
    public record HTXBatchCancelResult
    {
        /// <summary>
        /// Orders that were successfully canceled
        /// </summary>
        [JsonPropertyName("success")]
        public IEnumerable<string> Successful { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Orders that failed to cancel
        /// </summary>
        [JsonPropertyName("failed")]
        public IEnumerable<HTXFailedCancelResult> Failed { get; set; } = Array.Empty<HTXFailedCancelResult>();
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    public record HTXFailedCancelResult
    {
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
        /// <summary>
        /// The id of the failed order
        /// </summary>
        [JsonPropertyName("order-id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonPropertyName("order-state")]
        public int? OrderStatus { get; set; }
        /// <summary>
        /// The id of the failed order
        /// </summary>
        [JsonPropertyName("client-order-id")]
        public string? ClientOrderId { get; set; }
    }
}
