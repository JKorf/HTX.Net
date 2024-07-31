using System;
using System.Collections.Generic;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Positions limits
    /// </summary>
    public record HTXCrossMarginPositionLimit
    {
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
        public string MarginMode { get; set; } = string.Empty;
        /// <summary>
        /// Buy limit
        /// </summary>
        [JsonPropertyName("buy_limit")]
        public decimal BuyLimit { get; set; }
        /// <summary>
        /// Sell limit
        /// </summary>
        [JsonPropertyName("sell_limit")]
        public decimal SellLimit { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverRate { get; set; }
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
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]    
        public decimal MarkPrice { get; set; }
    }


}
