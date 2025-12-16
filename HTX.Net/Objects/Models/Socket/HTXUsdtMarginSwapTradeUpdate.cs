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
        /// Update id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trades
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
        /// Amount of trades
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("direction")]

        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
    }
}
