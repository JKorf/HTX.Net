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
        /// Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current pages
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Records
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
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance init
        /// </summary>
        [JsonPropertyName("margin_balance_init")]
        public decimal MarginBalanceInit { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Settlement profit realized
        /// </summary>
        [JsonPropertyName("settlement_profit_real")]
        public decimal SettlementRealizedPnl { get; set; }
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Clawback
        /// </summary>
        [JsonPropertyName("clawback")]
        public decimal Clawback { get; set; }
        /// <summary>
        /// Funding fee
        /// </summary>
        [JsonPropertyName("funding_fee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// Offset profit loss
        /// </summary>
        [JsonPropertyName("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Positions
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
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Cost open
        /// </summary>
        [JsonPropertyName("cost_open")]
        public decimal CostOpen { get; set; }
        /// <summary>
        /// Cost hold before settlement
        /// </summary>
        [JsonPropertyName("cost_hold_pre")]
        public decimal CostHoldPre { get; set; }
        /// <summary>
        /// Cost hold after settlement
        /// </summary>
        [JsonPropertyName("cost_hold")]
        public decimal ColdHold { get; set; }
        /// <summary>
        /// Settlement unrealized profit and loss
        /// </summary>
        [JsonPropertyName("settlement_profit_unreal")]
        public decimal SettlementUnrealizedPnl { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]

        public SettlementType SettlementType { get; set; }

    }
}
