using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account sentiment
    /// </summary>
    [SerializationModel]
    public record HTXAccountSentiment
    {
        /// <summary>
        /// ["<c>list</c>"] List
        /// </summary>
        [JsonPropertyName("list")]
        public HTXAccountSentimentValue[] List { get; set; } = Array.Empty<HTXAccountSentimentValue>();
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }

    /// <summary>
    /// Sentiment value
    /// </summary>
    [SerializationModel]
    public record HTXAccountSentimentValue
    {
        /// <summary>
        /// ["<c>buy_ratio</c>"] Buy ratio
        /// </summary>
        [JsonPropertyName("buy_ratio")]
        public decimal BuyRatio { get; set; }
        /// <summary>
        /// ["<c>sell_ratio</c>"] Sell ratio
        /// </summary>
        [JsonPropertyName("sell_ratio")]
        public decimal SellRatio { get; set; }
        /// <summary>
        /// ["<c>locked_ratio</c>"] Locked ratio
        /// </summary>
        [JsonPropertyName("locked_ratio")]
        public decimal LockedRatio { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
