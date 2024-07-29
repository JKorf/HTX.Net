using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Info on an asset
    /// </summary>
    public record HTXAssetInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the asset
        /// </summary>
        [JsonPropertyName("instStatus")]
        public InstrumentStatus Status { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public IEnumerable<HTXNetwork> Networks { get; set; } = Array.Empty<HTXNetwork>();
    }

    /// <summary>
    /// Info on an asset network
    /// </summary>
    public record HTXNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Base network
        /// </summary>
        [JsonPropertyName("baseChain")]
        public string BaseNetwork { get; set; } = string.Empty;
        /// <summary>
        /// Protocol of the base network
        /// </summary>
        [JsonPropertyName("baseChainProtocol")]
        public string BaseNetworkProtocol { get; set; } = string.Empty;
        /// <summary>
        /// Is dynamic fee type or not (only applicable to withdrawFeeType = fixed)
        /// </summary>
        [JsonPropertyName("isDynamic")]
        public bool IsDynamic { get; set; }
        /// <summary>
        /// Deposit status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("depositStatus")]
        public CurrencyStatus DepositStatus { get; set; }
        /// <summary>
        /// Maximum withdraw fee in each request (only applicable to withdrawFeeType = circulated or ratio)	
        /// </summary>
        [JsonPropertyName("maxTransactFeeWithdraw")]
        public decimal MaxTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Max withdraw quantity per request
        /// </summary>
        [JsonPropertyName("maxWithdrawAmt")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Min deposit quantity per request
        /// </summary>
        [JsonPropertyName("minDepositAmt")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// Min withdraw quantity per request
        /// </summary>
        [JsonPropertyName("minWithdrawAmt")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Withdraw fee in each request (only applicable to withdrawFeeType = fixed)
        /// </summary>
        [JsonPropertyName("transactFeeWithdraw")]
        public decimal TransactFeeWithdraw { get; set; }
        /// <summary>
        /// Withdraw fee in each request (only applicable to withdrawFeeType = ratio)
        /// </summary>
        [JsonPropertyName("transactFeeRateWithdraw")]
        public decimal? TransactFeeRateWithdraw { get; set; }
        /// <summary>
        /// Minimal withdraw fee in each request (only applicable to withdrawFeeType = circulated or ratio)
        /// </summary>
        [JsonPropertyName("minTransactFeeWithdraw")]
        public decimal MinTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Number of confirmations required for deposit
        /// </summary>
        [JsonPropertyName("numOfConfirmations")]
        public int NumberOfConfirmations { get; set; }
        /// <summary>
        /// Number of confirmations required for quick success (trading allowed but withdrawal disallowed)
        /// </summary>
        [JsonPropertyName("numOfFastConfirmations")]
        public int NumberOfFastConfirmations { get; set; }
        /// <summary>
        /// Type of withdraw fee
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("withdrawFeeType")]
        public FeeType WithdrawFeeType { get; set; }
        /// <summary>
        /// Precision of withdrawing
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// Withdraw quota per day
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerDay")]
        public decimal? WithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// Withdraw quota per year
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerYear")]
        public decimal? WithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// Withdraw quota in total
        /// </summary>
        [JsonPropertyName("withdrawQuotaTotal")]
        public decimal? WithdrawQuotaTotal { get; set; }

        /// <summary>
        /// Withdraw status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("withdrawStatus")]
        public CurrencyStatus WithdrawStatus { get; set; }
    }
}
