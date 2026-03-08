using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account assets info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginSubAccountAssets
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
        public HTXIsolatedMarginSubAccountAsset[] Assets { get; set; } = Array.Empty<HTXIsolatedMarginSubAccountAsset>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginSubAccountAsset
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
