using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Platform wide valuation
    /// </summary>
    [SerializationModel]
    public record HTXPlatformValuation
    {
        /// <summary>
        /// ["<c>updated</c>"] Updated
        /// </summary>
        [JsonPropertyName("updated")]
        public HTXPlatformValuationUpdate Updated { get; set; } = null!;
        /// <summary>
        /// ["<c>todayProfitRate</c>"] Today profit rate
        /// </summary>
        [JsonPropertyName("todayProfitRate")]
        public decimal? TodayProfitRate { get; set; }
        /// <summary>
        /// ["<c>totalBalance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// ["<c>todayProfit</c>"] Todays profit
        /// </summary>
        [JsonPropertyName("todayProfit")]
        public decimal? TodayProfit { get; set; }
        /// <summary>
        /// ["<c>profitAccountBalanceList</c>"] Account balance list
        /// </summary>
        [JsonPropertyName("profitAccountBalanceList")]
        public HTXPlatformValuationBalance[] Balances { get; set; } = Array.Empty<HTXPlatformValuationBalance>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXPlatformValuationUpdate
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Last update time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXPlatformValuationBalance
    {
        /// <summary>
        /// ["<c>distributionType</c>"] Balance type
        /// </summary>
        [JsonPropertyName("distributionType")]
        public ValuationBalanceType? BalanceType { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>accountBalance</c>"] Account balance
        /// </summary>
        [JsonPropertyName("accountBalance")]
        public decimal AccountBalance { get; set; }
    }


}
