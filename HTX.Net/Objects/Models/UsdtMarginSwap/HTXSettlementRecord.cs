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
        /// ["<c>total_page</c>"] Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total size
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }

        /// <summary>
        /// ["<c>settlement_record</c>"] Records
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
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settlement_time</c>"] Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// ["<c>clawback_ratio</c>"] Clawback ratio
        /// </summary>
        [JsonPropertyName("clawback_ratio")]
        public decimal ClawbackRatio { get; set; }
        /// <summary>
        /// ["<c>settlement_price</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// ["<c>settlement_type</c>"] Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]

        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
