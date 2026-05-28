namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Open interest
    /// </summary>
    [SerializationModel]
    public record HTXOpenInterestV5
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
    }
}
