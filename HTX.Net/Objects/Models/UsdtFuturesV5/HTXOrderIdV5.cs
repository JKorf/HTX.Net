namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXOrderIdV5
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
    }
}
