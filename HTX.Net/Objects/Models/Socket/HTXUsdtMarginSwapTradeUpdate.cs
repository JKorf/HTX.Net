using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapTradesUpdate
    {
        /// <summary>
        /// ["<c>id</c>"] Update id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Trades
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapTradeUpdate[] Trades { get; set; } = Array.Empty<HTXUsdtMarginSwapTradeUpdate>();
    }

    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapTradeUpdate
    {
        /// <summary>
        /// ["<c>amount</c>"] Amount of trades
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Side
        /// </summary>
        [JsonPropertyName("direction")]

        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
    }
}
