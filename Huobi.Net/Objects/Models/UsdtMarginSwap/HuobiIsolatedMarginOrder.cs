using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    public class HuobiIsolatedMarginOrder
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
        /// Quantity of contract
        /// </summary>
        [JsonProperty("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonProperty("order_price_type")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("order_type")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public Offset Offset { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Cancel time
        /// </summary>
        [JsonProperty("canceled_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonProperty("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Value of the quantity filled
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonProperty("trade_avg_price")]
        public decimal AverageFillPrice { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonProperty("margin_asset")]
        public string MarginAsset { get; set; } = String.Empty;
        /// <summary>
        /// Profit
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public SwapMarginOrderStatus Status { get; set; } 
        /// <summary>
        /// Source
        /// </summary>
        [JsonProperty("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Liquidation type
        /// </summary>
        [JsonProperty("liquidation_type")]
        public LiquidationType LiquidationType { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Is take profit/stop loss
        /// </summary>
        [JsonProperty("is_tpsl")]
        public bool IsTakeProfitStopLoss { get; set; }
        /// <summary>
        /// Real profit
        /// </summary>
        [JsonProperty("real_profit")]
        public decimal RealProfit { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonProperty("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonProperty("trade")]
        public IEnumerable<HuobiMarginTrade>? Trades { get; set; }
    }

    /// <summary>
    /// Cross margin order info
    /// </summary>
    public class HuobiCrossMarginOrder : HuobiIsolatedMarginOrder
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof (EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof (EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
