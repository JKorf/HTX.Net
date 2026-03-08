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
        /// ["<c>id</c>"] Open time
        /// </summary>
        [JsonPropertyName("id")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Turnover, quantity * contract value * price
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal? Value { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter<HTXOrderBookEntry>))]
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry? Ask { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid
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
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string? ContractCode { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }

        /// <summary>
        /// ["<c>number_of</c>"] Number of buys and sells in last 24h
        /// </summary>
        [JsonPropertyName("number_of")]
        public long? Trades { get; set; }
    }
}
