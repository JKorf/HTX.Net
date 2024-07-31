using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account sentiment
    /// </summary>
    public record HTXAccountSentiment
    {
        /// <summary>
        /// List
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXAccountSentimentValue> List { get; set; } = Array.Empty<HTXAccountSentimentValue>();
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }

    /// <summary>
    /// Sentiment value
    /// </summary>
    public record HTXAccountSentimentValue
    {
        /// <summary>
        /// Buy ratio
        /// </summary>
        [JsonPropertyName("buy_ratio")]
        public decimal BuyRatio { get; set; }
        /// <summary>
        /// Sell ratio
        /// </summary>
        [JsonPropertyName("sell_ratio")]
        public decimal SellRatio { get; set; }
        /// <summary>
        /// Locked ratio
        /// </summary>
        [JsonPropertyName("locked_ratio")]
        public decimal LockedRatio { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }


}
