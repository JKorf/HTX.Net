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
    public class HuobiCrossMarginSubAccountAssets
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
        public IEnumerable<HuobiCrossMarginSubAccountAsset> Assets { get; set; } = Array.Empty<HuobiCrossMarginSubAccountAsset>();
    }

    /// <summary>
    /// Sub account asset info
    /// </summary>
    public class HuobiCrossMarginSubAccountAsset
    {
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; }
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
