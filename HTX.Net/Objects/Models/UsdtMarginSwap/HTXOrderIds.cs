namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXOrderIds
    {
        /// <summary>
        /// ["<c>order_id_str</c>"] Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
    }
}
