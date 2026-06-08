using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Order trade
    /// </summary>
    [SerializationModel]
    public record HTXOrderTradeV5
    {
        /// <summary>
        /// ["<c>id</c>"] Query id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string? ContractCode { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string? TradeId { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide? Side { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public FuturesPositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Execution type
        /// </summary>
        [JsonPropertyName("order_type")]
        public string? OrderType { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderTypeV5? Type { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public OrderRole? Role { get; set; }
        /// <summary>
        /// ["<c>trade_price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("trade_price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Trade turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal? Turnover { get; set; }
        /// <summary>
        /// ["<c>created_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("created_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("updated_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>order_source</c>"] Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string? OrderSource { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Fee currency
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string? FeeCurrency { get; set; }
        /// <summary>
        /// ["<c>trade_fee</c>"] Trade fee
        /// </summary>
        [JsonPropertyName("trade_fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// ["<c>deduction_price</c>"] Deduction price
        /// </summary>
        [JsonPropertyName("deduction_price")]
        public decimal? DeductionPrice { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal? Profit { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType? ContractType { get; set; }
    }
}
