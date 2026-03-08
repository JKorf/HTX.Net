using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Levearege position limits
    /// </summary>
    [SerializationModel]
    public record HTXCrossLeveragePositionLimit
    {
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
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
        /// ["<c>list</c>"] List
        /// </summary>
        [JsonPropertyName("list")]
        public HTXCrossLeveragePositionLimitLeverage[] List { get; set; } = Array.Empty<HTXCrossLeveragePositionLimitLeverage>();
    }

    /// <summary>
    /// Limits
    /// </summary>
    [SerializationModel]
    public record HTXCrossLeveragePositionLimitLeverage
    {
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// ["<c>buy_limit_value</c>"] Buy limit value
        /// </summary>
        [JsonPropertyName("buy_limit_value")]
        public decimal BuyLimitValue { get; set; }
        /// <summary>
        /// ["<c>sell_limit_value</c>"] Sell limit value
        /// </summary>
        [JsonPropertyName("sell_limit_value")]
        public decimal SellLimitValue { get; set; }
    }


}
