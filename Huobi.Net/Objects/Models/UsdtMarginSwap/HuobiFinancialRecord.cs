using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Financial records page
    /// </summary>
    public class HuobiFinancialRecordsPage
    {
        /// <summary>
        /// Total amount of pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("financial_record")]
        public IEnumerable<HuobiFinancialRecord> Records { get; set; } = Array.Empty<HuobiFinancialRecord>();
    }

    /// <summary>
    /// Financial records
    /// </summary>
    public class HuobiFinancialRecord
    {
        /// <summary>
        /// Record id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Record type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public FinancialRecordType Type { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Face margin account
        /// </summary>
        [JsonProperty("face_margin_account")]
        public string FaceMarginAccount { get; set; } = string.Empty;

    }
}
