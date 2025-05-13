using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Batch result
    /// </summary>
    [SerializationModel]
    public record HTXBatchResult
    {
        /// <summary>
        /// Errors in the batch
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXBatchError[] Errors { get; set; } = [];
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("successes")]
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// Batch operation error
    /// </summary>
    [SerializationModel]
    public record HTXBatchError
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
