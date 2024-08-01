namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    public record HTXCrossMarginOrderPage : HTXPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<HTXCrossMarginOrder> Orders { get; set; } = Array.Empty<HTXCrossMarginOrder>();
    }
}
