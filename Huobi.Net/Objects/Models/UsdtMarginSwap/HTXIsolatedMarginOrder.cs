using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    public record HTXIsolatedMarginOrder
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Quantity of contract
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        [JsonConverter(typeof(EnumConverter))]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public Offset Offset { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Value of the quantity filled
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal AverageFillPrice { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
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
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Liquidation type
        /// </summary>
        [JsonPropertyName("liquidation_type")]
        public LiquidationType LiquidationType { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Is take profit/stop loss
        /// </summary>
        [JsonPropertyName("is_tpsl")]
        public bool IsTakeProfitStopLoss { get; set; }
        /// <summary>
        /// Real profit
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealProfit { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trade")]
        public IEnumerable<HTXMarginTrade>? Trades { get; set; }
    }

    /// <summary>
    /// Cross margin order info
    /// </summary>
    public record HTXCrossMarginOrder : HTXIsolatedMarginOrder
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof (EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof (EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
