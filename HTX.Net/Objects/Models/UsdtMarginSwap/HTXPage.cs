using CryptoExchange.Net.Converters.SystemTextJson;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Page of data
    /// </summary>
    [SerializationModel]
    public record HTXPage
    {
        /// <summary>
        /// Total amount of pages
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
    }
}
