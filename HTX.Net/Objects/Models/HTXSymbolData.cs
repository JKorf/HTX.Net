namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    [SerializationModel]
    public record HTXSymbolData
    {
        /// <summary>
        /// ["<c>high</c>"] The highest price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] The lowest price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>open</c>"] The price at the opening
        /// </summary>
        [JsonPropertyName("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>close</c>"] The last price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The volume in base asset
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] The volume in quote asset (quantity * price)
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>count</c>"] The number of trades
        /// </summary>
        [JsonPropertyName("count")]
        public int? TradeCount { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }
    }

    /// <summary>
    /// Ticker data
    /// </summary>
    [SerializationModel]
    public record HTXSymbolTicker : HTXSymbolData
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Symbol kline data
    /// </summary>
    [SerializationModel]
    public record HTXKline : HTXSymbolData
    {
        /// <summary>
        /// ["<c>id</c>"] The start time of the kline
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("id")]
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// ["<c>trade_turnover</c>"] Turnover, quantity * contract value * price
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal? Value { get; set; }
    }

    /// <summary>
    /// Kline data
    /// </summary>
    [SerializationModel]
    public record HTXSwapKline : HTXKline
    {
        /// <summary>
        /// ["<c>mrid</c>"] Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long OrderId { get; set; }
    }

    /// <summary>
    /// Symbol details
    /// </summary>
    [SerializationModel]
    public record HTXSymbolDetails : HTXSymbolData
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the details
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp of the data
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Symbol tick
    /// </summary>
    [SerializationModel]
    public record HTXSymbolTick : HTXSymbolData
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>bidSize</c>"] Quantity of the best bid
        /// </summary>
        [JsonPropertyName("bidSize")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askSize</c>"] Quantity of the best ask
        /// </summary>
        [JsonPropertyName("askSize")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>lastPrice</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// ["<c>lastSize</c>"] Last trade quantity
        /// </summary>
        [JsonPropertyName("lastSize")]
        public decimal LastTradeQuantity { get; set; }
    }
}
