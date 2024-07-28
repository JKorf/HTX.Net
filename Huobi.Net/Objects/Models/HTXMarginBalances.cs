using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Margin account balance
    /// </summary>
    public record HTXMarginBalances
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; } = string.Empty;
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk-rate")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// Account balance sum
        /// </summary>
        [JsonPropertyName("acct-balance-sum")]
        public decimal? AccountBalanceSum { get; set; }
        /// <summary>
        /// Debt balance sum
        /// </summary>
        [JsonPropertyName("debt-balance-sum")]
        public decimal? DebtBalanceSum { get; set; }
        /// <summary>
        /// The price which margin closeout was triggered
        /// </summary>
        [JsonPropertyName("fl-price")]
        public decimal? FlPrice { get; set; }
        /// <summary>
        /// Fl type
        /// </summary>
        [JsonPropertyName("fl-type")]
        public string? FlType { get; set; } = string.Empty;
        /// <summary>
        /// Account details
        /// </summary>
        public IEnumerable<HTXIsolatedBalance> List { get; set; } = Array.Empty<HTXIsolatedBalance>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public record HTXIsolatedBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Balance type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BalanceType Type { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
