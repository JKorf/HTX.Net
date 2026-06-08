namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Account balance detail
    /// </summary>
    [SerializationModel]
    public record HTXAccountBalanceDetailV5
    {
        /// <summary>
        /// ["<c>currency</c>"] Currency
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>isolated_equity</c>"] Isolated equity
        /// </summary>
        [JsonPropertyName("isolated_equity")]
        public decimal IsolatedEquity { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>withdraw_available</c>"] Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>isolated_profit_unreal</c>"] Isolated unrealized profit and loss
        /// </summary>
        [JsonPropertyName("isolated_profit_unreal")]
        public decimal IsolatedUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin_rate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenance_margin_rate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>initial_margin_rate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initial_margin_rate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>voucher</c>"] Voucher
        /// </summary>
        [JsonPropertyName("voucher")]
        public decimal Voucher { get; set; }
        /// <summary>
        /// ["<c>voucher_value</c>"] Voucher value
        /// </summary>
        [JsonPropertyName("voucher_value")]
        public decimal VoucherValue { get; set; }
        /// <summary>
        /// ["<c>created_time</c>"] Created time
        /// </summary>
        [JsonPropertyName("created_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_time</c>"] Updated time
        /// </summary>
        [JsonPropertyName("updated_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
