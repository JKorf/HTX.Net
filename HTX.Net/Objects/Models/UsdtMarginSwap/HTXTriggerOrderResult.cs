using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger order operation result
    /// </summary>
    [SerializationModel]
    public record HTXTriggerOrderResult
    {
        /// <summary>
        /// Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXTriggerOrderResultError[] Errors { get; set; } = Array.Empty<HTXTriggerOrderResultError>();
        /// <summary>
        /// Successful operations, comma seperated
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
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Err code
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// Err msg
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }


}
