using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Swaps
{
    /// <summary>
    /// Position data
    /// </summary>
    public class HuobiSwapsPosition
    {
        /// <summary>
        /// The product code
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// The position quantity
        /// </summary>
        [JsonProperty("volume")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The available position that can be closed
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// The amount frozen
        /// </summary>
        [JsonProperty("frozen")]
        public decimal Frozen { get; set; }

        /// <summary>
        /// The opening average price
        /// </summary>
        [JsonProperty("cost_open")]
        public decimal CostOpen { get; set; }

        /// <summary>
        /// The average price of the position
        /// </summary>
        [JsonProperty("cost_hold")]
        public decimal CostHold { get; set; }

        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }

        /// <summary>
        /// The profit rate
        /// </summary>
        [JsonProperty("profit_rate")]
        public decimal ProfitRate { get; set; }

        /// <summary>
        /// The profit
        /// </summary>
        [JsonProperty("profit")]
        public decimal Profit { get; set; }

        /// <summary>
        /// The position margin
        /// </summary>
        [JsonProperty("position_margin")]
        public decimal PositionMargin { get; set; }

        /// <summary>
        /// The leverage rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public int LeverRate { get; set; }

        /// <summary>
        /// The direction
        /// </summary>
        [JsonProperty("direction")]
        public OrderSide Direction { get; set; }

        /// <summary>
        /// Latest price
        /// </summary>
        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
    }
}
