using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// User settlement record page
    /// </summary>
    public class HuobiIsolatedMarginUserSettlementRecordPage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current pages
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Records
        /// </summary>
        [JsonProperty("settlement_records")]
        public IEnumerable<HuobiIsolatedMarginUserSettlementRecord> Records { get; set; } = Array.Empty<HuobiIsolatedMarginUserSettlementRecord>();
    }

    /// <summary>
    /// User settlement record
    /// </summary>
    public class HuobiIsolatedMarginUserSettlementRecord
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance init
        /// </summary>
        [JsonProperty("margin_balance_init")]
        public decimal MarginBalanceInit { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Settlement profit realized
        /// </summary>
        [JsonProperty("settlement_profit_real")]
        public decimal SettlementProfitReal { get; set; }
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonProperty("settlement_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Clawback
        /// </summary>
        public decimal Clawback { get; set; }
        /// <summary>
        /// Funding fee
        /// </summary>
        [JsonProperty("funding_fee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// Offset profit loss
        /// </summary>
        [JsonProperty("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HuobiCrossSettlementPosition> Positions { get; set; } = Array.Empty<HuobiCrossSettlementPosition>();
    }

    /// <summary>
    /// Settlement position
    /// </summary>
    public class HuobiCrossSettlementPosition
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
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
        [JsonProperty("cost_open")]
        public decimal CostOpen { get; set; }
        /// <summary>
        /// Cost hold before settlement
        /// </summary>
        [JsonProperty("cost_hold_pre")]
        public decimal CostHoldPre { get; set; }
        /// <summary>
        /// Cost hold after settlement
        /// </summary>
        [JsonProperty("cost_hold")]
        public decimal ColdHold { get; set; }
        /// <summary>
        /// Settlement profit unreal
        /// </summary>
        [JsonProperty("settlement_profit_unreal")]
        public decimal SettlementProfitUnreal { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonProperty("settlement_price")]
        public decimal SettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonProperty("settlement_type")]
        [JsonConverter(typeof(EnumConverter))]
        public SettlementType SettlementType { get; set; }

    }
}
