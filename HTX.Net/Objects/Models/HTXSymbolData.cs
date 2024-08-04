namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public record HTXSymbolData
    {
        /// <summary>
        /// The highest price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// The price at the opening
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// The volume in base asset
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The volume in quote asset (quantity * price)
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// The number of trades
        /// </summary>
        [JsonPropertyName("count")]
        public int? TradeCount { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }
    }

    /// <summary>
    /// Ticker data
    /// </summary>
    public record HTXSymbolTicker : HTXSymbolData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Symbol kline data
    /// </summary>
    public record HTXKline : HTXSymbolData
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("id")]
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// Turnover, quantity * contract value * price
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal? Value { get; set; }
    }

    /// <summary>
    /// Symbol details
    /// </summary>
    public record HTXSymbolDetails : HTXSymbolData
    {
        /// <summary>
        /// The id of the details
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Symbol tick
    /// </summary>
    public record HTXSymbolTick : HTXSymbolData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// Last trade quantity
        /// </summary>
        [JsonPropertyName("lastSize")]
        public decimal LastTradeQuantity { get; set; }
    }
}
