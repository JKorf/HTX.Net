using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Tiered margin info
    /// </summary>
    public class HuobiTieredMarginInfo
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// List
        /// </summary>
        public IEnumerable<HuobiTieredMarginRate> List { get; set; } = Array.Empty<HuobiTieredMarginRate>();
    }

    /// <summary>
    /// Tiered cross margin info
    /// </summary>
    public class HuobiTieredCrossMarginInfo: HuobiTieredMarginInfo
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get;set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
    }

    /// <summary>
    /// Margin rate
    /// </summary>
    public class HuobiTieredMarginRate
    {
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public decimal LeverRate { get; set; }
        /// <summary>
        /// Ladders
        /// </summary>
        public IEnumerable<HuobiTieredMarginLadder> Ladders { get; set; } = Array.Empty<HuobiTieredMarginLadder>();
    }

    /// <summary>
    /// Ladder info
    /// </summary>
    public class HuobiTieredMarginLadder
    {
        /// <summary>
        /// Min marging balance
        /// </summary>
        [JsonProperty("min_margin_balance")]
        public decimal? MinMarginBalance { get; set; }
        /// <summary>
        /// Max margin balance
        /// </summary>
        [JsonProperty("max_margin_balance")]
        public decimal? MaxMarginBalance { get; set; }
        /// <summary>
        /// Min margin available
        /// </summary>
        [JsonProperty("min_margin_available")]
        public decimal? MinMarginAvailable { get; set; }
        /// <summary>
        /// Max margin available
        /// </summary>
        [JsonProperty("max_margin_available")]
        public decimal? MaxMarginAvailable { get; set; }
    }
}
