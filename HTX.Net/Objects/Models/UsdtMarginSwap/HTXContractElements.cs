using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract element info
    /// </summary>
    [SerializationModel]
    public record HTXContractElements
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>funding_rate_cap</c>"] Funding rate cap
        /// </summary>
        [JsonPropertyName("funding_rate_cap")]
        public decimal FundingRateCap { get; set; }
        /// <summary>
        /// ["<c>funding_rate_floor</c>"] Funding rate floor
        /// </summary>
        [JsonPropertyName("funding_rate_floor")]
        public decimal FundingRateFloor { get; set; }
        /// <summary>
        /// ["<c>mode_type</c>"] Mode type
        /// </summary>
        [JsonPropertyName("mode_type")]
        public ElementModeType ModeType { get; set; }
        /// <summary>
        /// ["<c>swap_delivery_type</c>"] Swap delivery type
        /// </summary>
        [JsonPropertyName("swap_delivery_type")]
        public SwapDeliveryType SwapDeliveryType { get; set; }
        /// <summary>
        /// ["<c>settle_period</c>"] Settle period
        /// </summary>
        [JsonPropertyName("settle_period")]
        public int SettlePeriod { get; set; }
        /// <summary>
        /// ["<c>instrument_index_code</c>"] Instrument index code
        /// </summary>
        [JsonPropertyName("instrument_index_code")]
        public string InstrumentIndexCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price_ticks</c>"] Price ticks
        /// </summary>
        [JsonPropertyName("price_ticks")]
        public HTXContractElementsPriceTick[] PriceTicks { get; set; } = Array.Empty<HTXContractElementsPriceTick>();
        /// <summary>
        /// ["<c>instrument_values</c>"] Instrument values
        /// </summary>
        [JsonPropertyName("instrument_values")]
        public HTXContractElementsPriceTick[] InstrumentValues { get; set; } = Array.Empty<HTXContractElementsPriceTick>();
        /// <summary>
        /// ["<c>min_level</c>"] Min level
        /// </summary>
        [JsonPropertyName("min_level")]
        public decimal MinLevel { get; set; }
        /// <summary>
        /// ["<c>max_level</c>"] Max level
        /// </summary>
        [JsonPropertyName("max_level")]
        public decimal MaxLevel { get; set; }
        /// <summary>
        /// ["<c>order_limits</c>"] Order limits
        /// </summary>
        [JsonPropertyName("order_limits")]
        public HTXContractElementsOrderLimit[] OrderLimits { get; set; } = Array.Empty<HTXContractElementsOrderLimit>();
        /// <summary>
        /// ["<c>normal_limits</c>"] Normal limits
        /// </summary>
        [JsonPropertyName("normal_limits")]
        public HTXContractElementsLimit[] NormalLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// ["<c>open_limits</c>"] Open limits
        /// </summary>
        [JsonPropertyName("open_limits")]
        public HTXContractElementsLimit[] OpenLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// ["<c>trade_limits</c>"] Trade limits
        /// </summary>
        [JsonPropertyName("trade_limits")]
        public HTXContractElementsLimit[] TradeLimits { get; set; } = Array.Empty<HTXContractElementsLimit>();
        /// <summary>
        /// ["<c>real_time_settlement</c>"] Real time settlement
        /// </summary>
        [JsonPropertyName("real_time_settlement")]
        public bool RealTimeSettlement { get; set; }
        /// <summary>
        /// ["<c>transfer_profit_ratio</c>"] Transfer profit ratio
        /// </summary>
        [JsonPropertyName("transfer_profit_ratio")]
        public decimal TransferProfitRatio { get; set; }
        /// <summary>
        /// ["<c>cross_transfer_profit_ratio</c>"] Cross transfer profit ratio
        /// </summary>
        [JsonPropertyName("cross_transfer_profit_ratio")]
        public decimal CrossTransferProfitRatio { get; set; }
        /// <summary>
        /// ["<c>instrument_type</c>"] Instrument types
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType[] InstrumentTypes { get; set; } = Array.Empty<ElementInstrumentType>();
        /// <summary>
        /// ["<c>price_tick</c>"] Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// ["<c>instrument_value</c>"] Instrument value
        /// </summary>
        [JsonPropertyName("instrument_value")]
        public decimal InstrumentValue { get; set; }
        /// <summary>
        /// ["<c>trade_partition</c>"] Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open_order_limit</c>"] Open order limit
        /// </summary>
        [JsonPropertyName("open_order_limit")]
        public decimal OpenOrderLimit { get; set; }
        /// <summary>
        /// ["<c>offset_order_limit</c>"] Offset order limit
        /// </summary>
        [JsonPropertyName("offset_order_limit")]
        public decimal OffsetOrderLimit { get; set; }
        /// <summary>
        /// ["<c>long_position_limit</c>"] Long position limit
        /// </summary>
        [JsonPropertyName("long_position_limit")]
        public decimal LongPositionLimit { get; set; }
        /// <summary>
        /// ["<c>short_position_limit</c>"] Short position limit
        /// </summary>
        [JsonPropertyName("short_position_limit")]
        public decimal ShortPositionLimit { get; set; }
        /// <summary>
        /// ["<c>contract_infos</c>"] Contract infos
        /// </summary>
        [JsonPropertyName("contract_infos")]
        public HTXContractElementsContractInfo[] ContractInfos { get; set; } = Array.Empty<HTXContractElementsContractInfo>();
    }

    /// <summary>
    /// Price info
    /// </summary>
    [SerializationModel]
    public record HTXContractElementsPriceTick
    {
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public ElementBusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Limit info
    /// </summary>
    [SerializationModel]
    public record HTXContractElementsLimit
    {
        /// <summary>
        /// ["<c>instrument_type</c>"] Instrument type
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType InstrumentType { get; set; }
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [JsonPropertyName("open")]
        public decimal Open { get; set; }
        /// <summary>
        /// ["<c>close</c>"] Close
        /// </summary>
        [JsonPropertyName("close")]
        public decimal Close { get; set; }
    }

    /// <summary>
    /// Order limit
    /// </summary>
    [SerializationModel]
    public record HTXContractElementsOrderLimit: HTXContractElementsLimit
    {
        /// <summary>
        /// ["<c>open_after_closing</c>"] Open after closing
        /// </summary>
        [JsonPropertyName("open_after_closing")]
        public decimal OpenAfterClosing { get; set; }
    }

    /// <summary>
    /// Contract info
    /// </summary>
    [SerializationModel]
    public record HTXContractElementsContractInfo
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>instrument_type</c>"] Instrument type
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType InstrumentType { get; set; }
        /// <summary>
        /// ["<c>settlement_date</c>"] Settlement date
        /// </summary>
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// ["<c>delivery_time</c>"] Delivery time
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// ["<c>create_date</c>"] Create date
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// ["<c>contract_status</c>"] Contract status
        /// </summary>
        [JsonPropertyName("contract_status")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// ["<c>delivery_date</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
    }


}
