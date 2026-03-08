namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// NAV info for ETP
    /// </summary>
    [SerializationModel]
    public record HTXNav
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>nav</c>"] Latest Nav
        /// </summary>
        [JsonPropertyName("nav")]
        public decimal Nav { get; set; }
        /// <summary>
        /// ["<c>navTime</c>"] Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("navTime")]
        public DateTime NavTime { get; set; }
        /// <summary>
        /// ["<c>outstanding</c>"] Outstanding shares
        /// </summary>
        [JsonPropertyName("outstanding")]
        public decimal Outstanding { get; set; }
        /// <summary>
        /// ["<c>basket</c>"] Baskets
        /// </summary>
        [JsonPropertyName("basket")]
        public HTXBasket[] Basket { get; set; } = Array.Empty<HTXBasket>();
        /// <summary>
        /// ["<c>actualLeverage</c>"] Actual leverage ratio
        /// </summary>
        [JsonPropertyName("actualLeverage")]
        public decimal ActualLeverage { get; set; }
    }
    
    /// <summary>
    /// Basket
    /// </summary>
    [SerializationModel]
    public record HTXBasket
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
