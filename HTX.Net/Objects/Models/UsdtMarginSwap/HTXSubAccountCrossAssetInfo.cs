using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account asset info page
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountCrossAssetInfoPage
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
        public HTXSubAccountCrossAssetInfo[] SubAccounts { get; set; } = Array.Empty<HTXSubAccountCrossAssetInfo>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountCrossAssetInfo
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
        public HTXSubAccountCrossAssetInfoAsset[] Assets { get; set; } = Array.Empty<HTXSubAccountCrossAssetInfoAsset>();
    }

    /// <summary>
    /// Sub account asset
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountCrossAssetInfoAsset
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
