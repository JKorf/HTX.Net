namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin assets and positions info
    /// </summary>
    public record HTXIsolatedMarginAssetsAndPositions: HTXIsolatedMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public IEnumerable<HTXPosition>? Positions { get; set; } = Array.Empty<HTXPosition>();
    }
}
