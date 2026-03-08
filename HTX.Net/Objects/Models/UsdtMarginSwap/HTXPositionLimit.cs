using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Position limit
    /// </summary>
    [SerializationModel]
    public record HTXPositionLimit
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
        /// ["<c>buy_limit</c>"] Buy limit
        /// </summary>
        [JsonPropertyName("buy_limit")]
        public decimal BuyLimit { get; set; }
        /// <summary>
        /// ["<c>sell_limit</c>"] Sell limit
        /// </summary>
        [JsonPropertyName("sell_limit")]
        public decimal SellLimit { get; set; }
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>lever_rate</c>"] Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LevererageRate { get; set; }
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
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
    }


}
