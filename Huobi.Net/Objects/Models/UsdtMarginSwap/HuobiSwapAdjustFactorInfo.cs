using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Adjust factor info
    /// </summary>
    public class HuobiSwapAdjustFactorInfo
    {
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
        /// list
        /// </summary>
        public IEnumerable<HuobiFactorInfo> List { get; set; } = Array.Empty<HuobiFactorInfo>();
    }

    /// <summary>
    /// Cross margin adjust factor info
    /// </summary>
    public class HuobiCrossSwapAdjustFactorInfo: HuobiSwapAdjustFactorInfo
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }
    }

    /// <summary>
    /// Factor info
    /// </summary>
    public class HuobiFactorInfo
    {
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public decimal LeverRate { get; set; }
        /// <summary>
        /// Ladders
        /// </summary>
        public IEnumerable<HuobiFactorEntry> Ladders { get; set; } = Array.Empty<HuobiFactorEntry>();
    }

    /// <summary>
    /// Factor info
    /// </summary>
    public class HuobiFactorEntry
    {
        /// <summary>
        /// Ladder
        /// </summary>
        public int Ladder { get; set; }
        /// <summary>
        /// Min size
        /// </summary>
        [JsonProperty("min_size")]
        public int? MinSize { get; set; }
        /// <summary>
        /// Max size
        /// </summary>
        [JsonProperty("max_size")]
        public int? MaxSize { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonProperty("adjust_factor")]
        public decimal AdjustFactor { get; set; }
    }
}
