using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin trade status
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginTradeStatus
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
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open</c>"] Open order access
        /// </summary>
        [JsonPropertyName("open")]
        public bool Open { get; set; }
        /// <summary>
        /// ["<c>close</c>"] Close order access
        /// </summary>
        [JsonPropertyName("close")]
        public bool Close { get; set; }
        /// <summary>
        /// ["<c>cancel</c>"] Cancel order access
        /// </summary>
        [JsonPropertyName("cancel")]
        public bool Cancel { get; set; }
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
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>

        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
