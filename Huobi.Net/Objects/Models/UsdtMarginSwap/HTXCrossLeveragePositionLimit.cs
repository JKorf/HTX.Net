using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Levearege position limits
    /// </summary>
    public record HTXCrossLeveragePositionLimit
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// List
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXCrossLeveragePositionLimitLeverage> List { get; set; } = Array.Empty<HTXCrossLeveragePositionLimitLeverage>();
    }

    /// <summary>
    /// Limits
    /// </summary>
    public record HTXCrossLeveragePositionLimitLeverage
    {
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Buy limit value
        /// </summary>
        [JsonPropertyName("buy_limit_value")]
        public decimal BuyLimitValue { get; set; }
        /// <summary>
        /// Sell limit value
        /// </summary>
        [JsonPropertyName("sell_limit_value")]
        public decimal SellLimitValue { get; set; }
    }


}
