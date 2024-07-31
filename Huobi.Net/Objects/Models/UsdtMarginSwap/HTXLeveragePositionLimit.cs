﻿using System;
using System.Collections.Generic;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Leverage position limit info
    /// </summary>
    public record HTXLeveragePositionLimit
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
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Limits
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXLeveragePositionLeverageLimit> Limits { get; set; } = Array.Empty<HTXLeveragePositionLeverageLimit>();
    }

    /// <summary>
    /// Leverage position limit
    /// </summary>
    public record HTXLeveragePositionLeverageLimit
    {
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("leverage_rate")]
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
