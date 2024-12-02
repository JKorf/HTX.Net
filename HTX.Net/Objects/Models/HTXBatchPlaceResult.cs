namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Batch placement result
    /// </summary>
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
        [JsonConverterCtor<ReplaceConverter>($"{HTXExchange.ClientOrderIdPrefix}->")]
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
