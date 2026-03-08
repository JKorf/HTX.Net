namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin assets and positions info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginAssetsAndPositions: HTXIsolatedMarginAccountInfo
    {
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public HTXPosition[]? Positions { get; set; } = Array.Empty<HTXPosition>();
    }
}
