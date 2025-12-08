using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Insurance info
    /// </summary>
    [SerializationModel]
    public record HTXInsuranceInfo
    {
        /// <summary>
        /// Total amount of pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty; 
        /// <summary>
        /// The history data
        /// </summary>
        [JsonPropertyName("tick")]
        public HTXInsuranceValue[] History { get; set; } = Array.Empty<HTXInsuranceValue>();
    }

    /// <summary>
    /// Insurance value
    /// </summary>
    [SerializationModel]
    public record HTXInsuranceValue
    {
        /// <summary>
        /// Insurance fund
        /// </summary>
        [JsonPropertyName("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
