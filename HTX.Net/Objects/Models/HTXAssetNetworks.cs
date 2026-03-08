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
        /// ["<c>chains</c>"] Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public HTXAssetNetwork[] Networks { get; set; } = Array.Empty<HTXAssetNetwork>();
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>instStatus</c>"] Status
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
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayName</c>"] Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
        /// <summary>
        /// ["<c>baseChain</c>"] Base network
        /// </summary>
        [JsonPropertyName("baseChain")]
        public string BaseNetwork { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseChainProtocol</c>"] Base network protocol
        /// </summary>
        [JsonPropertyName("baseChainProtocol")]
        public string BaseNetworkProtocol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isDynamic</c>"] Is dynamic
        /// </summary>
        [JsonPropertyName("isDynamic")]
        public bool IsDynamic { get; set; }
        /// <summary>
        /// ["<c>depositStatus</c>"] Deposit status
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public NetworkStatus? DepositStatus { get; set; }
        /// <summary>
        /// ["<c>maxTransactFeeWithdraw</c>"] Max transact fee withdraw
        /// </summary>
        [JsonPropertyName("maxTransactFeeWithdraw")]
        public decimal MaxTransactFeeWithdraw { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmt</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmt")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>minDepositAmt</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositAmt")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>transactFeeWithdraw</c>"] Fixed withdraw fee, only applicable if fee type is fixed
        /// </summary>
        [JsonPropertyName("transactFeeWithdraw")]
        public decimal? FixedWithdrawFee { get; set; }
        /// <summary>
        /// ["<c>transactFeeRateWithdraw</c>"] Ratio withdraw fee, only applicable if fee type is ratio
        /// </summary>
        [JsonPropertyName("transactFeeRateWithdraw")]
        public decimal? RatioWithdrawFee { get; set; }
        /// <summary>
        /// ["<c>minTransactFeeWithdraw</c>"] Min transact fee withdraw
        /// </summary>
        [JsonPropertyName("minTransactFeeWithdraw")]
        public decimal MinTransactFeeWithdraw { get; set; }
        /// <summary>
        /// ["<c>minWithdrawAmt</c>"] Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawAmt")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>numOfConfirmations</c>"] Num of confirmations
        /// </summary>
        [JsonPropertyName("numOfConfirmations")]
        public int NumOfConfirmations { get; set; }
        /// <summary>
        /// ["<c>numOfFastConfirmations</c>"] Num of fast confirmations
        /// </summary>
        [JsonPropertyName("numOfFastConfirmations")]
        public int NumOfFastConfirmations { get; set; }
        /// <summary>
        /// ["<c>withdrawFeeType</c>"] Withdraw fee type
        /// </summary>
        [JsonPropertyName("withdrawFeeType")]
        public FeeType? WithdrawFeeType { get; set; }
        /// <summary>
        /// ["<c>withdrawPrecision</c>"] Withdraw precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public decimal WithdrawPrecision { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaPerDay</c>"] Withdraw quota per day
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerDay")]
        public decimal? WithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaPerYear</c>"] Withdraw quota per year
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerYear")]
        public decimal? WithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaTotal</c>"] Withdraw quota total
        /// </summary>
        [JsonPropertyName("withdrawQuotaTotal")]
        public decimal? WithdrawQuotaTotal { get; set; }
        /// <summary>
        /// ["<c>withdrawStatus</c>"] Withdraw status
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public NetworkStatus? WithdrawStatus { get; set; }
    }


}
