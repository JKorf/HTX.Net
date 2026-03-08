using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// User settlement record page
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginUserSettlementRecordPage
    {
        /// <summary>
        /// ["<c>total_page</c>"] Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>current_page</c>"] Current pages
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total amount of records
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// ["<c>settlement_records</c>"] Records
        /// </summary>
        [JsonPropertyName("settlement_records")]
        public HTXIsolatedMarginUserSettlementRecord[] Records { get; set; } = Array.Empty<HTXIsolatedMarginUserSettlementRecord>();
    }

    /// <summary>
    /// User settlement record
    /// </summary>
    [SerializationModel]
    public record HTXIsolatedMarginUserSettlementRecord
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_balance_init</c>"] Margin balance init
        /// </summary>
        [JsonPropertyName("margin_balance_init")]
        public decimal MarginBalanceInit { get; set; }
        /// <summary>
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>settlement_profit_real</c>"] Settlement profit realized
        /// </summary>
        [JsonPropertyName("settlement_profit_real")]
        public decimal SettlementRealizedPnl { get; set; }
        /// <summary>
        /// ["<c>settlement_time</c>"] Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// ["<c>clawback</c>"] Clawback
        /// </summary>
        [JsonPropertyName("clawback")]
        public decimal Clawback { get; set; }
        /// <summary>
        /// ["<c>funding_fee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("funding_fee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// ["<c>offset_profitloss</c>"] Offset profit loss
        /// </summary>
        [JsonPropertyName("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public HTXCrossSettlementPosition[] Positions { get; set; } = Array.Empty<HTXCrossSettlementPosition>();
    }

    /// <summary>
    /// Settlement position
    /// </summary>
    [SerializationModel]
    public record HTXCrossSettlementPosition
    {
        /// <summary>
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>cost_open</c>"] Cost open
        /// </summary>
        [JsonPropertyName("cost_open")]
        public decimal CostOpen { get; set; }
        /// <summary>
        /// ["<c>cost_hold_pre</c>"] Cost hold before settlement
        /// </summary>
        [JsonPropertyName("cost_hold_pre")]
        public decimal CostHoldPre { get; set; }
        /// <summary>
        /// ["<c>cost_hold</c>"] Cost hold after settlement
        /// </summary>
        [JsonPropertyName("cost_hold")]
        public decimal ColdHold { get; set; }
        /// <summary>
        /// ["<c>settlement_profit_unreal</c>"] Settlement unrealized profit and loss
        /// </summary>
        [JsonPropertyName("settlement_profit_unreal")]
        public decimal SettlementUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>settlement_price</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// ["<c>settlement_type</c>"] Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]

        public SettlementType SettlementType { get; set; }

    }
}
