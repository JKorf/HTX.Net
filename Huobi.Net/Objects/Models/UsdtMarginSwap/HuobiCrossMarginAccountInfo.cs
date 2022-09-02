using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account info
    /// </summary>
    public class HuobiCrossMarginAccountInfo
    {
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonProperty("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonProperty("margin_available")]
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Profit real
        /// </summary>
        [JsonProperty("profit_real")]
        public decimal ProfitReal { get; set; }
        /// <summary>
        /// Profit unreal
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonProperty("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// Margin static
        /// </summary>
        [JsonProperty("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonProperty("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonProperty("position_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Contract details
        /// </summary>
        [JsonProperty("contract_detail")]
        public IEnumerable<HuobiCrossMarginAccountContract> ContractDetails { get; set; } = Array.Empty<HuobiCrossMarginAccountContract>();
        /// <summary>
        /// Futures contract details
        /// </summary>
        [JsonProperty("futures_contract_detail")]
        public IEnumerable<HuobiCrossMarginAccountContract> FuturesContractDetails { get; set; } = Array.Empty<HuobiCrossMarginAccountContract>();
    }
}
