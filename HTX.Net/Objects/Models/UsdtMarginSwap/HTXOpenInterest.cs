using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Open interest
    /// </summary>
    [SerializationModel]
    public record HTXOpenInterest
    {
        /// <summary>
        /// ["<c>symbol</c>"] The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>value</c>"] Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_amount</c>"] Trade amount
        /// </summary>
        [JsonPropertyName("trade_amount")]
        public decimal TradeAmount { get; set; }
        /// <summary>
        /// ["<c>trade_volume</c>"] Trade volume
        /// </summary>
        [JsonPropertyName("trade_volume")]
        public decimal TradeVolume { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Trade turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
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
