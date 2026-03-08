namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXSwapOrderId
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
