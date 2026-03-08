using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Liquidation order
    /// </summary>
    [SerializationModel]
    public record HTXLiquidationOrder
    {
        /// <summary>
        /// ["<c>query_id</c>"] Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>

        [JsonPropertyName("direction")]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>

        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Turnover { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
