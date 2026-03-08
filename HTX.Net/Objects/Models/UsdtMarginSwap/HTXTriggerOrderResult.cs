namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger order operation result
    /// </summary>
    [SerializationModel]
    public record HTXTriggerOrderResult
    {
        /// <summary>
        /// ["<c>errors</c>"] Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXTriggerOrderResultError[] Errors { get; set; } = Array.Empty<HTXTriggerOrderResultError>();
        /// <summary>
        /// ["<c>successes</c>"] Successful operations, comma seperated
        /// </summary>
        [JsonPropertyName("successes")]
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// Error info
    /// </summary>
    [SerializationModel]
    public record HTXTriggerOrderResultError
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>err_code</c>"] Err code
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>err_msg</c>"] Err msg
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }


}
