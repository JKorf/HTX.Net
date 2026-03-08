using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginUserTrade
    {
        /// <summary>
        /// ["<c>query_id</c>"] Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>match_id</c>"] Match id
        /// </summary>
        [JsonPropertyName("match_id")]
        public long MatchId { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>trade_price</c>"] Price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>trade_fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("trade_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>offset_profitloss</c>"] Offset profit loss
        /// </summary>
        [JsonPropertyName("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// ["<c>create_date</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole Role { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_id_str</c>"] Order id str
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
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
        /// ["<c>real_profit</c>"] Real profit
        /// </summary>
        [JsonPropertyName("real_profit")]
        public decimal RealProfit { get; set; }
        /// <summary>
        /// ["<c>reduce_only</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }


}
