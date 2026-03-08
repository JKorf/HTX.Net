namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    [SerializationModel]
    public record HTXMarginOrderDetails : HTXIsolatedMarginOrder
    {
        /// <summary>
        /// ["<c>instrument_price</c>"] Instrument price
        /// </summary>
        [JsonPropertyName("instrument_price")]
        public decimal InstrumentPrice { get; set; }
        /// <summary>
        /// ["<c>final_interest</c>"] Final interest
        /// </summary>
        [JsonPropertyName("final_interest")]
        public decimal FinalInterest { get; set; }
        /// <summary>
        /// ["<c>adjust_value</c>"] Adjust value
        /// </summary>
        [JsonPropertyName("adjust_value")]
        public decimal AdjustValue { get; set; }
        /// <summary>
        /// ["<c>total_page</c>"] Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total amount of records
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// ["<c>trades</c>"] Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public new HTXMarginTrade[] Trades { get; set; } = Array.Empty<HTXMarginTrade>();
    }
}
