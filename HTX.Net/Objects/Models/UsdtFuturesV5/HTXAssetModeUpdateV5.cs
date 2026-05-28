using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Asset mode update
    /// </summary>
    [SerializationModel]
    public record HTXAssetModeUpdateV5
    {
        /// <summary>
        /// ["<c>assets_mode</c>"] Asset mode
        /// </summary>
        [JsonPropertyName("assets_mode")]
        public AssetMode AssetMode { get; set; }
    }
}
