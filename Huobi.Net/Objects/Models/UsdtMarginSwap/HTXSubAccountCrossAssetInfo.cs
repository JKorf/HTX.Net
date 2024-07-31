using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account asset info page
    /// </summary>
    public record HTXSubAccountCrossAssetInfoPage
    {
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("total_page")]    
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total items
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
        /// <summary>
        /// Sub accounts
        /// </summary>
        [JsonPropertyName("sub_list")]
        public IEnumerable<HTXSubAccountCrossAssetInfo> SubAccounts { get; set; } = Array.Empty<HTXSubAccountCrossAssetInfo>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    public record HTXSubAccountCrossAssetInfo
    {
        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("account_info_list")]
        public IEnumerable<HTXSubAccountCrossAssetInfoAsset> Assets { get; set; } = Array.Empty<HTXSubAccountCrossAssetInfoAsset>();
    }

    /// <summary>
    /// Sub account asset
    /// </summary>
    public record HTXSubAccountCrossAssetInfoAsset
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
