namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Placed order id 
    /// </summary>
    [SerializationModel]
    public record HTXPlacedOrderId
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
    }
}
