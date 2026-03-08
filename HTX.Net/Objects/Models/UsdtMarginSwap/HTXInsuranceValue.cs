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
        /// ["<c>total_page</c>"] Total amount of pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total amount of results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
        /// <summary>
        /// ["<c>tick</c>"] The history data
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
        /// ["<c>insurance_fund</c>"] Insurance fund
        /// </summary>
        [JsonPropertyName("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
