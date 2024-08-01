namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Tp/sl set result
    /// </summary>
    public record HTXTpSlResult
    {
        /// <summary>
        /// Take profit order
        /// </summary>
        [JsonPropertyName("tp_order")]
        public HTXTpSlResultOrder? TpOrder { get; set; }
        /// <summary>
        /// Stop loss order
        /// </summary>
        [JsonPropertyName("sl_order")]
        public HTXTpSlResultOrder? SlOrder { get; set; }
    }

    /// <summary>
    /// Order info
    /// </summary>
    public record HTXTpSlResultOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
    }
}
