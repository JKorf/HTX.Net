using CryptoExchange.Net.Converters;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Market data
    /// </summary>
    public record HTXMarketData: HTXSymbolData
    {
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HTXOrderBookEntry? Ask { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HTXOrderBookEntry? Bid { get; set; }
    }
}
