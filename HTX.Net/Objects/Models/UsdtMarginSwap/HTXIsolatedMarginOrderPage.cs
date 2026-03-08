namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginOrderPage : HTXPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXIsolatedMarginOrder[] Orders { get; set; } = Array.Empty<HTXIsolatedMarginOrder>();
    }
}
