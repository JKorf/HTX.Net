namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXOrderId
    {
        /// <summary>
        /// ["<c>order-id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order-id")]
        public long OrderId { get; set; }
        [JsonInclude, JsonPropertyName("orderId")]
        internal long OrderIdInt
        {
            get => OrderId;
            set => OrderId = value;
        }
    }
}
