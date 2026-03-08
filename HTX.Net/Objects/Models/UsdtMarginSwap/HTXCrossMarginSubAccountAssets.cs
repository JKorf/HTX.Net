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
        /// ["<c>sub_uid</c>"] Sub account uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public long SubUid { get; set; }
        /// <summary>
        /// ["<c>list</c>"] Assets
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
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>margin_asset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
    }
}
