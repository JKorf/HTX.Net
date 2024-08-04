using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract element info
    /// </summary>
    public record HTXContractElements
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate cap
        /// </summary>
        [JsonPropertyName("funding_rate_cap")]
        public decimal FundingRateCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonPropertyName("funding_rate_floor")]
        public decimal FundingRateFloor { get; set; }
        /// <summary>
        /// Mode type
        /// </summary>
        [JsonPropertyName("mode_type")]
        public ElementModeType ModeType { get; set; }
        /// <summary>
        /// Swap delivery type
        /// </summary>
        [JsonPropertyName("swap_delivery_type")]
        public SwapDeliveryType SwapDeliveryType { get; set; }
        /// <summary>
        /// Settle period
        /// </summary>
        [JsonPropertyName("settle_period")]
        public int SettlePeriod { get; set; }
        /// <summary>
        /// Instrument index code
        /// </summary>
        [JsonPropertyName("instrument_index_code")]
        public string InstrumentIndexCode { get; set; } = string.Empty;
        /// <summary>
        /// Price ticks
        /// </summary>
        [JsonPropertyName("price_ticks")]
        public IEnumerable<HTXContractElementsPriceTick> PriceTicks { get; set; } = Array.Empty<HTXContractElementsPriceTick>();
        /// <summary>
        /// Instrument values
        /// </summary>
        [JsonPropertyName("instrument_values")]
        public IEnumerable<HTXContractElementsPriceTick> InstrumentValues { get; set; } = Array.Empty<HTXContractElementsPriceTick>();
        /// <summary>
        /// Min level
        /// </summary>
        [JsonPropertyName("min_level")]
        public decimal MinLevel { get; set; }
        /// <summary>
        /// Max level
        /// </summary>
        [JsonPropertyName("max_level")]
        public decimal MaxLevel { get; set; }
        /// <summary>
        /// Order limits
        /// </summary>
        [JsonPropertyName("order_limits")]
        public IEnumerable<HTXContractElementsOrderLimit> OrderLimits { get; set; } = Array.Empty<HTXContractElementsOrderLimit>();
        /// <summary>
        /// Normal limits
        /// </summary>
        [JsonPropertyName("normal_limits")]
        public IEnumerable<HTXContractElementsLimit> NormalLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// Open limits
        /// </summary>
        [JsonPropertyName("open_limits")]
        public IEnumerable<HTXContractElementsLimit> OpenLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// Trade limits
        /// </summary>
        [JsonPropertyName("trade_limits")]
        public IEnumerable<HTXContractElementsLimit> TradeLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// Real time settlement
        /// </summary>
        [JsonPropertyName("real_time_settlement")]
        public bool RealTimeSettlement { get; set; }
        /// <summary>
        /// Transfer profit ratio
        /// </summary>
        [JsonPropertyName("transfer_profit_ratio")]
        public decimal TransferProfitRatio { get; set; }
        /// <summary>
        /// Cross transfer profit ratio
        /// </summary>
        [JsonPropertyName("cross_transfer_profit_ratio")]
        public decimal CrossTransferProfitRatio { get; set; }
        /// <summary>
        /// Instrument types
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public IEnumerable<ElementInstrumentType> InstrumentTypes { get; set; } = Array.Empty<ElementInstrumentType>();
        /// <summary>
        /// Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// Instrument value
        /// </summary>
        [JsonPropertyName("instrument_value")]
        public decimal InstrumentValue { get; set; }
        /// <summary>
        /// Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// Open order limit
        /// </summary>
        [JsonPropertyName("open_order_limit")]
        public decimal OpenOrderLimit { get; set; }
        /// <summary>
        /// Offset order limit
        /// </summary>
        [JsonPropertyName("offset_order_limit")]
        public decimal OffsetOrderLimit { get; set; }
        /// <summary>
        /// Long position limit
        /// </summary>
        [JsonPropertyName("long_position_limit")]
        public decimal LongPositionLimit { get; set; }
        /// <summary>
        /// Short position limit
        /// </summary>
        [JsonPropertyName("short_position_limit")]
        public decimal ShortPositionLimit { get; set; }
        /// <summary>
        /// Contract infos
        /// </summary>
        [JsonPropertyName("contract_infos")]
        public IEnumerable<HTXContractElementsContractInfo> ContractInfos { get; set; } = Array.Empty<HTXContractElementsContractInfo>();
    }

    /// <summary>
    /// Price info
    /// </summary>
    public record HTXContractElementsPriceTick
    {
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public ElementBusinessType BusinessType { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Limit info
    /// </summary>
    public record HTXContractElementsLimit
    {
        /// <summary>
        /// Instrument type
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType InstrumentType { get; set; }
        /// <summary>
        /// Open
        /// </summary>
        [JsonPropertyName("open")]
        public decimal Open { get; set; }
        /// <summary>
        /// Close
        /// </summary>
        [JsonPropertyName("close")]
        public decimal Close { get; set; }
    }

    /// <summary>
    /// Order limit
    /// </summary>
    public record HTXContractElementsOrderLimit: HTXContractElementsLimit
    {
        /// <summary>
        /// Open after closing
        /// </summary>
        [JsonPropertyName("open_after_closing")]
        public decimal OpenAfterClosing { get; set; }
    }

    /// <summary>
    /// Contract info
    /// </summary>
    public record HTXContractElementsContractInfo
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Instrument type
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType InstrumentType { get; set; }
        /// <summary>
        /// Settlement date
        /// </summary>
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// Delivery time
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// Create date
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Contract status
        /// </summary>
        [JsonPropertyName("contract_status")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
    }


}
