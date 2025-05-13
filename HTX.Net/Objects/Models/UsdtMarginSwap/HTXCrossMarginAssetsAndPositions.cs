using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin assets and positions info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginAssetsAndPositions : HTXCrossMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public HTXCrossPosition[]? Positions { get; set; } = Array.Empty<HTXCrossPosition>();
    }
}
