using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Business type
        /// </summary>

        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonPropertyName("ask")]
        public HTXOrderBookEntry Ask { get; set; } = null!;
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonPropertyName("bid")]
        public HTXOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("mrid")]
        public long Id { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }
}
