using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Settlement page
    /// </summary>
    public class HuobiSettlementPage
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
        /// Total size
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalSize { get; set; }

        /// <summary>
        /// Records
        /// </summary>
        [JsonProperty("settlement_record")]
        public IEnumerable<HuobiSettlementRecord> Records { get; set; } = Array.Empty<HuobiSettlementRecord>();
    }

    /// <summary>
    /// Settlement info
    /// </summary>
    public class HuobiSettlementRecord
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonProperty("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Clawback ratio
        /// </summary>
        [JsonProperty("clawback_ratio")]
        public decimal ClawbackRatio { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonProperty("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonProperty("settlement_type")]
        [JsonConverter(typeof(EnumConverter))]
        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
