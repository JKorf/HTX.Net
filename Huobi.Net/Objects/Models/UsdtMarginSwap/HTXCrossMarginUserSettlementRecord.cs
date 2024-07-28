using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// User settlement record page
    /// </summary>
    public record HTXCrossMarginUserSettlementRecordPage
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
        public IEnumerable<HTXCrossMarginUserSettlementRecord> Records { get; set; } = Array.Empty<HTXCrossMarginUserSettlementRecord>();
    }

    /// <summary>
    /// User settlement record
    /// </summary>
    public record HTXCrossMarginUserSettlementRecord
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
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
        public decimal SettlementProfitReal { get; set; }
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Clawback
        /// </summary>
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
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Contract details
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public IEnumerable<HTXCrossMarginSettlementAccountContract> ContractDetails { get; set; } = Array.Empty<HTXCrossMarginSettlementAccountContract>();

    }

    /// <summary>
    /// Settlement contract details
    /// </summary>
    public record HTXCrossMarginSettlementAccountContract
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
        /// Offset profit loss
        /// </summary>
        [JsonPropertyName("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HTXSettlementPosition> Positions { get; set; } = Array.Empty<HTXSettlementPosition>();
    }

    /// <summary>
    /// Settlement position
    /// </summary>
    public record HTXSettlementPosition
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
        public OrderSide Direction { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
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
        /// Settlement profit unreal
        /// </summary>
        [JsonPropertyName("settlement_profit_unreal")]
        public decimal SettlementProfitUnreal { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonPropertyName("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonPropertyName("settlement_type")]
        [JsonConverter(typeof(EnumConverter))]
        public SettlementType SettlementType { get; set; }

    }
}
