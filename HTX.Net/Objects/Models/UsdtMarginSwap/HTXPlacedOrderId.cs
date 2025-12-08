namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Placed order id 
    /// </summary>
    [SerializationModel]
    public record HTXPlacedOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
    }
}
