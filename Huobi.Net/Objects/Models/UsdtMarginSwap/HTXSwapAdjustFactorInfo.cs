using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Adjust factor info
    /// </summary>
    public record HTXSwapAdjustFactorInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// list
        /// </summary>
        public IEnumerable<HTXFactorInfo> List { get; set; } = Array.Empty<HTXFactorInfo>();
    }

    /// <summary>
    /// Cross margin adjust factor info
    /// </summary>
    public record HTXCrossSwapAdjustFactorInfo: HTXSwapAdjustFactorInfo
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }

    /// <summary>
    /// Factor info
    /// </summary>
    public record HTXFactorInfo
    {
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverRate { get; set; }
        /// <summary>
        /// Ladders
        /// </summary>
        public IEnumerable<HTXFactorEntry> Ladders { get; set; } = Array.Empty<HTXFactorEntry>();
    }

    /// <summary>
    /// Factor info
    /// </summary>
    public record HTXFactorEntry
    {
        /// <summary>
        /// Ladder
        /// </summary>
        public int Ladder { get; set; }
        /// <summary>
        /// Min size
        /// </summary>
        [JsonPropertyName("min_size")]
        public int? MinSize { get; set; }
        /// <summary>
        /// Max size
        /// </summary>
        [JsonPropertyName("max_size")]
        public int? MaxSize { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
    }
}
