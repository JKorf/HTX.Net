using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account asset info page
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountAssetInfoPage
    {
        /// <summary>
        /// ["<c>total_page</c>"] Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total items
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>sub_list</c>"] Sub accounts
        /// </summary>
        [JsonPropertyName("sub_list")]
        public HTXSubAccountAssetInfo[] SubAccounts { get; set; } = Array.Empty<HTXSubAccountAssetInfo>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountAssetInfo
    {
        /// <summary>
        /// ["<c>sub_uid</c>"] Sub user id
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// ["<c>account_info_list</c>"] Assets
        /// </summary>
        [JsonPropertyName("account_info_list")]
        public HTXSubAccountAssetInfoAsset[] Assets { get; set; } = Array.Empty<HTXSubAccountAssetInfoAsset>();
    }

    /// <summary>
    /// Sub account asset
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountAssetInfoAsset
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
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
