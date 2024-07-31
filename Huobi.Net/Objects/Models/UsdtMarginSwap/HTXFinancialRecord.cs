using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Financial records page
    /// </summary>
    public record HTXFinancialRecordsPage
    {
        /// <summary>
        /// Total amount of pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("financial_record")]
        public IEnumerable<HTXFinancialRecord> Records { get; set; } = Array.Empty<HTXFinancialRecord>();
    }

    /// <summary>
    /// Financial records
    /// </summary>
    public record HTXFinancialRecord
    {
        /// <summary>
        /// Record id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Record type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("type")]
        public FinancialRecordType Type { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Face margin account
        /// </summary>
        [JsonPropertyName("face_margin_account")]
        public string FaceMarginAccount { get; set; } = string.Empty;

    }
}
