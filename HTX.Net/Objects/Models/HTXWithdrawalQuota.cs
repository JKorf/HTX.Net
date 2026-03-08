namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Asset quota
    /// </summary>
    [SerializationModel]
    public record HTXWithdrawalQuota
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chains</c>"] Networks
        /// </summary>
        [JsonPropertyName("chains")]
        public HTXWithdrawalNetworkQuota[] Networks { get; set; } = Array.Empty<HTXWithdrawalNetworkQuota>();
    }

    /// <summary>
    /// Network quota
    /// </summary>
    [SerializationModel]
    public record HTXWithdrawalNetworkQuota
    {
        /// <summary>
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>maxWithdrawAmt</c>"] Max withdraw amt
        /// </summary>
        [JsonPropertyName("maxWithdrawAmt")]
        public decimal MaxWithdrawAmt { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaPerDay</c>"] Withdraw quota per day
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerDay")]
        public decimal WithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// ["<c>remainWithdrawQuotaPerDay</c>"] Remain withdraw quota per day
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaPerDay")]
        public decimal RemainWithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaPerYear</c>"] Withdraw quota per year
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerYear")]
        public decimal WithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// ["<c>remainWithdrawQuotaPerYear</c>"] Remain withdraw quota per year
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaPerYear")]
        public decimal RemainWithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// ["<c>withdrawQuotaTotal</c>"] Withdraw quota total
        /// </summary>
        [JsonPropertyName("withdrawQuotaTotal")]
        public decimal WithdrawQuotaTotal { get; set; }
        /// <summary>
        /// ["<c>remainWithdrawQuotaTotal</c>"] Remain withdraw quota total
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaTotal")]
        public decimal RemainWithdrawQuotaTotal { get; set; }
    }
}
