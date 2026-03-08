using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Best offer
    /// </summary>
    [SerializationModel]
    public record HTXSwapBookTicker
    {
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>

        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ask</c>"] Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry Ask { get; set; } = null!;
        /// <summary>
        /// ["<c>bid</c>"] Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// ["<c>mrid</c>"] Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
