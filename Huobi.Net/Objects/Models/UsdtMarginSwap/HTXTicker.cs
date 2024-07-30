using CryptoExchange.Net.Converters;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Market data
    /// </summary>
    public record HTXTicker: HTXSymbolData
    {
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry? Ask { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry? Bid { get; set; }
    }
}
