using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Market data
    /// </summary>
    [SerializationModel]
    public record HTXTicker: HTXSymbolData
    {
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("id")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Turnover, quantity * contract value * price
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal? Value { get; set; }
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<HTXOrderBookEntry>))]
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry? Ask { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<HTXOrderBookEntry>))]
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry? Bid { get; set; }
    }

    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record HTXListTicker : HTXTicker
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string? ContractCode { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }

        /// <summary>
        /// Number of buys and sells in last 24h
        /// </summary>
        [JsonPropertyName("number_of")]
        public long? Trades { get; set; }
    }
}
