using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Platform wide valuation
    /// </summary>
    public record HTXPlatformValuation
    {
        /// <summary>
        /// Updated
        /// </summary>
        [JsonPropertyName("updated")]
        public HTXPlatformValuationUpdate Updated { get; set; } = null!;
        /// <summary>
        /// Today profit rate
        /// </summary>
        [JsonPropertyName("todayProfitRate")]
        public decimal TodayProfitRate { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// Todays profit
        /// </summary>
        [JsonPropertyName("todayProfit")]
        public decimal TodayProfit { get; set; }
        /// <summary>
        /// Account balance list
        /// </summary>
        [JsonPropertyName("profitAccountBalanceList")]
        public IEnumerable<HTXPlatformValuationBalance> Balances { get; set; } = Array.Empty<HTXPlatformValuationBalance>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record HTXPlatformValuationUpdate
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public record HTXPlatformValuationBalance
    {
        /// <summary>
        /// Balance type
        /// </summary>
        [JsonPropertyName("distributionType")]
        public ValuationBalanceType? BalanceType { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Account balance
        /// </summary>
        [JsonPropertyName("accountBalance")]
        public decimal AccountBalance { get; set; }
    }


}
