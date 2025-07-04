using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Settlement page
    /// </summary>
    [SerializationModel]
    public record HTXSettlementPage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total size
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }

        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("settlement_record")]
        public HTXSettlementRecord[] Records { get; set; } = Array.Empty<HTXSettlementRecord>();
    }

    /// <summary>
    /// Settlement info
    /// </summary>
    [SerializationModel]
    public record HTXSettlementRecord
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Clawback ratio
        /// </summary>
        [JsonPropertyName("clawback_ratio")]
        public decimal ClawbackRatio { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]

        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
