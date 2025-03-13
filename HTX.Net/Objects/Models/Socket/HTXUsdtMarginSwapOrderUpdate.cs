using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapOrderUpdate : HTXOpMessage
    {
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
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
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter OrderStatus { get; set; }
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
        /// Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int OrderType { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Value filled in quote asset
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Liquidation type
        /// </summary>
        [JsonPropertyName("liquidation_type")]
        public LiquidationType? LiquidationType { get; set; }
        /// <summary>
        /// Average trade price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// Canceled at
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
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
        /// Is take profit / stop loss
        /// </summary>
        [JsonPropertyName("is_tpsl")]
        public bool IsTpsl { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Canceled source
        /// </summary>
        [JsonPropertyName("canceled_source"), JsonConverter(typeof(NumberStringConverter))]
        public string? CanceledSource { get; set; }
        /// <summary>
        /// Trade info
        /// </summary>
        [JsonPropertyName("trade")]
        public HTXUsdtMarginSwapOrderUpdateTrade[] Trade { get; set; } = Array.Empty<HTXUsdtMarginSwapOrderUpdateTrade>();
    }

    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapOrderUpdateTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("trade_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Deduction asset price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? DeductionAssetPrice { get; set; }
        /// <summary>
        /// Trade value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Value { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole Role { get; set; }
        /// <summary>
        /// Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// Real profit
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealProfit { get; set; }
    }


}
