using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Margin account balance
    /// </summary>
    [SerializationModel]
    public record HTXMarginBalances
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>risk-rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk-rate")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// ["<c>acct-balance-sum</c>"] Account balance sum
        /// </summary>
        [JsonPropertyName("acct-balance-sum")]
        public decimal? AccountBalanceSum { get; set; }
        /// <summary>
        /// ["<c>debt-balance-sum</c>"] Debt balance sum
        /// </summary>
        [JsonPropertyName("debt-balance-sum")]
        public decimal? DebtBalanceSum { get; set; }
        /// <summary>
        /// ["<c>fl-price</c>"] The price which margin closeout was triggered
        /// </summary>
        [JsonPropertyName("fl-price")]
        public decimal? FlPrice { get; set; }
        /// <summary>
        /// ["<c>fl-type</c>"] Fl type
        /// </summary>
        [JsonPropertyName("fl-type")]
        public string? FlType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>list</c>"] Account details
        /// </summary>
        [JsonPropertyName("list")]
        public HTXIsolatedBalance[] List { get; set; } = Array.Empty<HTXIsolatedBalance>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Balance type
        /// </summary>

        [JsonPropertyName("type")]
        public BalanceType Type { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
