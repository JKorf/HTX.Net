using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account info
    /// </summary>
    public record HTXCrossMarginAccountInfo
    {
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Profit real
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal ProfitReal { get; set; }
        /// <summary>
        /// Profit unreal
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal ProfitUnreal { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// Margin static
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Contract details
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public IEnumerable<HTXCrossMarginAccountContract> ContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
        /// <summary>
        /// Futures contract details
        /// </summary>
        [JsonPropertyName("futures_contract_detail")]
        public IEnumerable<HTXCrossMarginAccountContract> FuturesContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
    }
}
