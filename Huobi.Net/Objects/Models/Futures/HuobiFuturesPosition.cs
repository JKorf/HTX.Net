using System;
using System.Collections.Generic;
using System.Text;
using Huobi.Net.Converters.Futures;
using Huobi.Net.Enums;
using Huobi.Net.Enums.Futures;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position data
    /// </summary>
    public class HuobiFuturesPosition
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
        /// The contract type
        /// </summary>
        [JsonProperty("contract_type"), JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }

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

    /// <summary>
    /// Position data
    /// </summary>
    public class HuobiFuturesUsdtPosition
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

        /// <summary>
        /// The margin asset
        /// </summary>
        [JsonProperty("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// The margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// The margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// The position mode, single_side，dual_side
        /// </summary>
        [JsonProperty("position_mode")]
        public string PositionMode { get; set; } = string.Empty;
    }

    /// <summary>
    /// Position data
    /// </summary>
    public class HuobiFuturesUsdtCrossPosition : HuobiFuturesUsdtPosition
    {
        /// <summary>
        /// The contract type
        /// </summary>
        [JsonProperty("contract_type"), JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// The business type, futures, swap
        /// </summary>
        [JsonProperty("business_type")]
        public string BusinessType { get; set; } = string.Empty;
    }
}
