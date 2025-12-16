using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record HTXAssetNetworks
    {
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public HTXAssetNetwork[] Networks { get; set; } = Array.Empty<HTXAssetNetwork>();
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("instStatus")]
        public AssetStatus Status { get; set; }
    }

    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record HTXAssetNetwork
    {
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
        /// <summary>
        /// Base network
        /// </summary>
        [JsonPropertyName("baseChain")]
        public string BaseNetwork { get; set; } = string.Empty;
        /// <summary>
        /// Base network protocol
        /// </summary>
        [JsonPropertyName("baseChainProtocol")]
        public string BaseNetworkProtocol { get; set; } = string.Empty;
        /// <summary>
        /// Is dynamic
        /// </summary>
        [JsonPropertyName("isDynamic")]
        public bool IsDynamic { get; set; }
        /// <summary>
        /// Deposit status
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public NetworkStatus? DepositStatus { get; set; }
        /// <summary>
        /// Max transact fee withdraw
        /// </summary>
        [JsonPropertyName("maxTransactFeeWithdraw")]
        public decimal MaxTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmt")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositAmt")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// Fixed withdraw fee, only applicable if fee type is fixed
        /// </summary>
        [JsonPropertyName("transactFeeWithdraw")]
        public decimal? FixedWithdrawFee { get; set; }
        /// <summary>
        /// Ratio withdraw fee, only applicable if fee type is ratio
        /// </summary>
        [JsonPropertyName("transactFeeRateWithdraw")]
        public decimal? RatioWithdrawFee { get; set; }
        /// <summary>
        /// Min transact fee withdraw
        /// </summary>
        [JsonPropertyName("minTransactFeeWithdraw")]
        public decimal MinTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawAmt")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Num of confirmations
        /// </summary>
        [JsonPropertyName("numOfConfirmations")]
        public int NumOfConfirmations { get; set; }
        /// <summary>
        /// Num of fast confirmations
        /// </summary>
        [JsonPropertyName("numOfFastConfirmations")]
        public int NumOfFastConfirmations { get; set; }
        /// <summary>
        /// Withdraw fee type
        /// </summary>
        [JsonPropertyName("withdrawFeeType")]
        public FeeType? WithdrawFeeType { get; set; }
        /// <summary>
        /// Withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public decimal WithdrawPrecision { get; set; }
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
        /// Withdraw quota total
        /// </summary>
        [JsonPropertyName("withdrawQuotaTotal")]
        public decimal? WithdrawQuotaTotal { get; set; }
        /// <summary>
        /// Withdraw status
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public NetworkStatus? WithdrawStatus { get; set; }
    }


}
