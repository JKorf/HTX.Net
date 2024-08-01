

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order id
    /// </summary>
    public record HTXTriggerOrderId
    {
        /// <summary>
        /// Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
    }
}
