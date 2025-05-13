using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Asset quota
    /// </summary>
    [SerializationModel]
    public record HTXWithdrawalQuota
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Networks
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
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Max withdraw amt
        /// </summary>
        [JsonPropertyName("maxWithdrawAmt")]
        public decimal MaxWithdrawAmt { get; set; }
        /// <summary>
        /// Withdraw quota per day
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerDay")]
        public decimal WithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// Remain withdraw quota per day
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaPerDay")]
        public decimal RemainWithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// Withdraw quota per year
        /// </summary>
        [JsonPropertyName("withdrawQuotaPerYear")]
        public decimal WithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// Remain withdraw quota per year
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaPerYear")]
        public decimal RemainWithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// Withdraw quota total
        /// </summary>
        [JsonPropertyName("withdrawQuotaTotal")]
        public decimal WithdrawQuotaTotal { get; set; }
        /// <summary>
        /// Remain withdraw quota total
        /// </summary>
        [JsonPropertyName("remainWithdrawQuotaTotal")]
        public decimal RemainWithdrawQuotaTotal { get; set; }
    }
}
