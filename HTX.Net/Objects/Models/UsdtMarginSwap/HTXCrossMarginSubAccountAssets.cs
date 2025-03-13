using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account assets info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginSubAccountAssets
    {
        /// <summary>
        /// Sub account uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public long SubUid { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("list")]
        public HTXCrossMarginSubAccountAsset[] Assets { get; set; } = Array.Empty<HTXCrossMarginSubAccountAsset>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginSubAccountAsset
    {
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
    }
}
