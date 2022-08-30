using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Insurance info
    /// </summary>
    public class HuobiInsuranceInfo
    {
        /// <summary>
        /// Total amount of pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of results
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalSize { get; set; }
        /// <summary>
        /// The asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty; 
        /// <summary>
        /// The history data
        /// </summary>
        [JsonProperty("tick")]
        public IEnumerable<HuobiInsuranceValue> History { get; set; } = Array.Empty<HuobiInsuranceValue>();
    }

    /// <summary>
    /// Insurance value
    /// </summary>
    public class HuobiInsuranceValue
    {
        /// <summary>
        /// Insurance fund
        /// </summary>
        [JsonProperty("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
