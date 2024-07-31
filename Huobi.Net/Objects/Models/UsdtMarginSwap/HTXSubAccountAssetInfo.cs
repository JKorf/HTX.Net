using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account asset info page
    /// </summary>
    public record HTXSubAccountAssetInfoPage
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
        public IEnumerable<HTXSubAccountAssetInfo> SubAccounts { get; set; } = Array.Empty<HTXSubAccountAssetInfo>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    public record HTXSubAccountAssetInfo
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
        public IEnumerable<HTXSubAccountAssetInfoAsset> Assets { get; set; } = Array.Empty<HTXSubAccountAssetInfoAsset>();
    }

    /// <summary>
    /// Sub account asset
    /// </summary>
    public record HTXSubAccountAssetInfoAsset
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
