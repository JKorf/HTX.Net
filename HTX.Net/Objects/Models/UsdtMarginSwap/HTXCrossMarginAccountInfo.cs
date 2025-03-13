using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// New risk rate
        /// </summary>
        [JsonPropertyName("new_risk_rate")]
        public decimal? NewRiskRate { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// Money in
        /// </summary>
        [JsonPropertyName("money_in")]
        public decimal? MoneyIn { get; set; }
        /// <summary>
        /// Money out
        /// </summary>
        [JsonPropertyName("money_out")]
        public decimal? MoneyOut { get; set; }
        /// <summary>
        /// Margin static
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// Cross max available
        /// </summary>
        [JsonPropertyName("cross_max_available")]
        public decimal CrossMaxAvailable { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]

        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ADL risk percentage
        /// </summary>
        [JsonPropertyName("adl_risk_percent")]
        public decimal? AdlRiskPercentage { get; set; }
        /// <summary>
        /// Contract details
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public HTXCrossMarginAccountContract[] ContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
        /// <summary>
        /// Futures contract details
        /// </summary>
        [JsonPropertyName("futures_contract_detail")]
        public HTXCrossMarginAccountContract[] FuturesContractDetails { get; set; } = Array.Empty<HTXCrossMarginAccountContract>();
    }
}
