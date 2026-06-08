namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Account balance
    /// </summary>
    [SerializationModel]
    public record HTXAccountBalanceV5
    {
        /// <summary>
        /// ["<c>state</c>"] Account status
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>equity</c>"] Account equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
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
        /// ["<c>profit_unreal</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>available_margin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal AvailableMargin { get; set; }
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
        /// <summary>
        /// ["<c>details</c>"] Currency details
        /// </summary>
        [JsonPropertyName("details")]
        public HTXAccountBalanceDetailV5[] Details { get; set; } = [];
    }
}
