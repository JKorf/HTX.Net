using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Sub account assets info
    /// </summary>
    public class HuobiIsolatedMarginSubAccountAssets
    {
        /// <summary>
        /// Sub account uid
        /// </summary>
        [JsonProperty("sub_uid")]
        public long SubUid { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<HuobiIsolatedMarginSubAccountAsset> Assets { get; set; } = Array.Empty<HuobiIsolatedMarginSubAccountAsset>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    public class HuobiIsolatedMarginSubAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonProperty("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonProperty("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
    }
}
