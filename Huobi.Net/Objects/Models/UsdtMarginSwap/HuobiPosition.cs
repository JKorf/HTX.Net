using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin position info
    /// </summary>
    public class HuobiPosition
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
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        public decimal Frozen { get; set; }
        /// <summary>
        /// Opening average price
        /// </summary>
        [JsonProperty("cost_open")]
        public decimal CostOpen { get; set; }
        /// <summary>
        /// Average price of position
        /// </summary>
        [JsonProperty("cost_hold")]
        public decimal CostHold { get; set; }
        /// <summary>
        /// Unrealized pnl
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }
        /// <summary>
        /// Profit rate
        /// </summary>
        [JsonProperty("profit_rate")]
        public decimal ProfitRate { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public decimal LeverRate { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonProperty("position_margin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Order direction
        /// </summary>
        [JsonProperty("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Profit
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
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
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonProperty("position_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public PositionMode PositionMode { get; set; }
    }
}
