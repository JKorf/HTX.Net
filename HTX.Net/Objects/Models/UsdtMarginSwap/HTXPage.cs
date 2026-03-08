namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Page of data
    /// </summary>
    [SerializationModel]
    public record HTXPage
    {
        /// <summary>
        /// ["<c>total_page</c>"] Total amount of pages
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
    }
}
