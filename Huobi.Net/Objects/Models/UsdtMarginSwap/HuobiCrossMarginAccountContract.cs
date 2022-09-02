using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin account contract details
    /// </summary>
    public class HuobiCrossMarginAccountContract
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
        /// Profit unreal
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonProperty("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Lever rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public decimal LeverRate { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonProperty("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Trade partition
        /// </summary>
        [JsonProperty("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
    }
}
