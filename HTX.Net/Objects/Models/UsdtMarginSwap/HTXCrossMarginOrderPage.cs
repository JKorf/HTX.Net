namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginOrderPage : HTXPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXCrossMarginOrder[] Orders { get; set; } = Array.Empty<HTXCrossMarginOrder>();
    }
}
