using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Leverage position limit info
    /// </summary>
    [SerializationModel]
    public record HTXLeveragePositionLimit
    {
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
        /// ["<c>list</c>"] Limits
        /// </summary>
        [JsonPropertyName("list")]
        public HTXLeveragePositionLeverageLimit[] Limits { get; set; } = Array.Empty<HTXLeveragePositionLeverageLimit>();
    }

    /// <summary>
    /// Leverage position limit
    /// </summary>
    [SerializationModel]
    public record HTXLeveragePositionLeverageLimit
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
