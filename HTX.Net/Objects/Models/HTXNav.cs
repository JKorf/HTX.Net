using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// NAV info for ETP
    /// </summary>
    [SerializationModel]
    public record HTXNav
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Latest Nav
        /// </summary>
        [JsonPropertyName("nav")]
        public decimal Nav { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("navTime")]
        public DateTime NavTime { get; set; }
        /// <summary>
        /// Outstanding shares
        /// </summary>
        [JsonPropertyName("outstanding")]
        public decimal Outstanding { get; set; }
        /// <summary>
        /// Baskets
        /// </summary>
        [JsonPropertyName("basket")]
        public HTXBasket[] Basket { get; set; } = Array.Empty<HTXBasket>();
        /// <summary>
        /// Actual leverage ratio
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
