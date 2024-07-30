using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Best offer
    /// </summary>
    public record HTXSwapBookTicker
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
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
