using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginAccountInfo
    {
        /// <summary>
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>margin_position</c>"] Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// ["<c>margin_frozen</c>"] Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// ["<c>margin_available</c>"] Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal? MarginAvailable { get; set; }
        /// <summary>
        /// ["<c>profit_real</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>new_risk_rate</c>"] New risk rate
        /// </summary>
        [JsonPropertyName("new_risk_rate")]
        public decimal? NewRiskRate { get; set; }
        /// <summary>
        /// ["<c>withdraw_available</c>"] Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// ["<c>money_in</c>"] Money in
        /// </summary>
        [JsonPropertyName("money_in")]
        public decimal? MoneyIn { get; set; }
        /// <summary>
        /// ["<c>money_out</c>"] Money out
        /// </summary>
        [JsonPropertyName("money_out")]
        public decimal? MoneyOut { get; set; }
        /// <summary>
        /// ["<c>margin_static</c>"] Margin static
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// ["<c>cross_max_available</c>"] Cross max available
        /// </summary>
        [JsonPropertyName("cross_max_available")]
        public decimal CrossMaxAvailable { get; set; }
        /// <summary>
        /// ["<c>margin_asset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
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
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]

        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ["<c>adl_risk_percent</c>"] ADL risk percentage
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercentage { get; set; }
        /// <summary>
        /// ["<c>contract_detail</c>"] Contract details
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public HTXCrossMarginAccountContract[] ContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
        /// <summary>
        /// ["<c>futures_contract_detail</c>"] Futures contract details
        /// </summary>
        [JsonPropertyName("futures_contract_detail")]
        public HTXCrossMarginAccountContract[] FuturesContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
    }
}
