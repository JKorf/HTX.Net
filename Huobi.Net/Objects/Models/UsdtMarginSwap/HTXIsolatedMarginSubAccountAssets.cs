using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account assets info
    /// </summary>
    public record HTXIsolatedMarginSubAccountAssets
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
        public IEnumerable<HTXIsolatedMarginSubAccountAsset> Assets { get; set; } = Array.Empty<HTXIsolatedMarginSubAccountAsset>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    public record HTXIsolatedMarginSubAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
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
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
    }
}
