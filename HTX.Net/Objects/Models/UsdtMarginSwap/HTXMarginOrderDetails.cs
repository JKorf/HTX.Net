using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    [SerializationModel]
    public record HTXMarginOrderDetails : HTXIsolatedMarginOrder
    {
        /// <summary>
        /// Instrument price
        /// </summary>
        [JsonPropertyName("instrument_price")]
        public decimal InstrumentPrice { get; set; }
        /// <summary>
        /// Final interest
        /// </summary>
        [JsonPropertyName("final_interest")]
        public decimal FinalInterest { get; set; }
        /// <summary>
        /// Adjust value
        /// </summary>
        [JsonPropertyName("adjust_value")]
        public decimal AdjustValue { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public new HTXMarginTrade[] Trades { get; set; } = Array.Empty<HTXMarginTrade>();
    }
}
