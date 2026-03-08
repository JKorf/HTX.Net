using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginOrder
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>volume</c>"] Quantity of contract
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]

        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]

        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>

        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>

        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>order_id_str</c>"] Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public long? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        [JsonInclude, JsonPropertyName("create_date")]
        internal DateTime CreateTimeInt 
        {
            get => CreateTime;
            set => CreateTime = value; 
        }

        /// <summary>
        /// ["<c>canceled_at</c>"] Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Value of the quantity filled
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>trade_avg_price</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("trade_avg_price")]
        public decimal? AverageFillPrice { get; set; }
        /// <summary>
        /// ["<c>margin_frozen</c>"] Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// ["<c>margin_asset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = String.Empty;
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>

        [JsonPropertyName("status")]
        public SwapMarginOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>liquidation_type</c>"] Liquidation type
        /// </summary>
        [JsonPropertyName("liquidation_type")]
        public LiquidationType? LiquidationType { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>is_tpsl</c>"] Is take profit/stop loss
        /// </summary>
        [JsonPropertyName("is_tpsl")]
        public bool IsTakeProfitStopLoss { get; set; }
        /// <summary>
        /// ["<c>real_profit</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>fee_amount</c>"] HTX fee quantity
        /// </summary>
        [JsonPropertyName("fee_amount")]
        public decimal FeeQuantity { get; set; }
        /// <summary>
        /// ["<c>fee_quote_amount</c>"] HTX fee quote quantity
        /// </summary>
        [JsonPropertyName("fee_quote_amount")]
        public decimal FeeQuoteQuantity { get; set; }
        /// <summary>
        /// Cancel source
        /// </summary>
        [JsonPropertyName("canceled_source"), JsonConverter(typeof(NumberStringConverter))]
        public string? CancelSource { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>trade</c>"] Trades
        /// </summary>
        [JsonPropertyName("trade")]
        public HTXMarginTrade[]? Trades { get; set; }
    }

    /// <summary>
    /// Cross margin order info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginOrder : HTXIsolatedMarginOrder
    {
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
