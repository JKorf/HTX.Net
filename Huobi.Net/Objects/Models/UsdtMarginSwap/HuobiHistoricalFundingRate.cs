using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rates page
    /// </summary>
    public class HuobiFundingRatePage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalResults { get; set; }
        /// <summary>
        /// Rates
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<HuobiHistoricalFundingRate> Rates { get; set; } = Array.Empty<HuobiHistoricalFundingRate>();
    }

    /// <summary>
    /// Historical funding rate
    /// </summary>
    public class HuobiHistoricalFundingRate
    {
        /// <summary>
        /// Average premium index
        /// </summary>
        [JsonProperty("avg_premium_index")]
        public decimal AveragePremiumIndex { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonProperty("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Realized rate
        /// </summary>
        [JsonProperty("realized_rate")]
        public decimal RealizedRate { get; set; }
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonProperty("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
