using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Asset mode
    /// </summary>
    [SerializationModel]
    public record HTXAssetModeV5
    {
        /// <summary>
        /// ["<c>asset_mode</c>"] Asset mode
        /// </summary>
        [JsonPropertyName("asset_mode")]
        public AssetMode AssetMode { get; set; }
    }
}
