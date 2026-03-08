namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Batch result
    /// </summary>
    [SerializationModel]
    public record HTXBatchResult
    {
        /// <summary>
        /// ["<c>errors</c>"] Errors in the batch
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXBatchError[] Errors { get; set; } = [];
        /// <summary>
        /// ["<c>successes</c>"] Success
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
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>err_code</c>"] Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>err_msg</c>"] Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
