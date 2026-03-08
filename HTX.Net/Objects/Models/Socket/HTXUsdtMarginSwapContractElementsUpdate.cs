using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    [SerializationModel]
    internal record HTXUsdtMarginSwapContractElementsUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapContractElementsUpdate[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapContractElementsUpdate>();
    }

    /// <summary>
    /// Contract element info
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapContractElementsUpdate
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
        public decimal? FundingRateCap { get; set; }
        /// <summary>
        /// ["<c>funding_rate_floor</c>"] Funding rate floor
        /// </summary>
        [JsonPropertyName("funding_rate_floor")]
        public decimal? FundingRateFloor { get; set; }
        /// <summary>
        /// ["<c>mode_type</c>"] Mode type
        /// </summary>
        [JsonPropertyName("mode_type")]
        public ElementModeType? ModeType { get; set; }
        /// <summary>
        /// ["<c>swap_delivery_type</c>"] Swap delivery type
        /// </summary>
        [JsonPropertyName("swap_delivery_type")]
        public SwapDeliveryType? SwapDeliveryType { get; set; }
        /// <summary>
        /// ["<c>settle_period</c>"] Settle period
        /// </summary>
        [JsonPropertyName("settle_period")]
        public int? SettlePeriod { get; set; }
        /// <summary>
        /// ["<c>instrument_index_code</c>"] Instrument index code
        /// </summary>
        [JsonPropertyName("instrument_index_code")]
        public string? InstrumentIndexCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price_ticks</c>"] Price ticks
        /// </summary>
        [JsonPropertyName("price_ticks")]
        public HTXContractElementsPriceTick[]? PriceTicks { get; set; }
        /// <summary>
        /// ["<c>instrument_values</c>"] Instrument values
        /// </summary>
        [JsonPropertyName("instrument_values")]
        public HTXContractElementsPriceTick[]? InstrumentValues { get; set; }
        /// <summary>
        /// ["<c>min_level</c>"] Min level
        /// </summary>
        [JsonPropertyName("min_level")]
        public decimal? MinLevel { get; set; }
        /// <summary>
        /// ["<c>max_level</c>"] Max level
        /// </summary>
        [JsonPropertyName("max_level")]
        public decimal? MaxLevel { get; set; }
        /// <summary>
        /// ["<c>order_limits</c>"] Order limits
        /// </summary>
        [JsonPropertyName("order_limits")]
        public HTXContractElementsOrderLimit[]? OrderLimits { get; set; }
        /// <summary>
        /// ["<c>normal_limits</c>"] Normal limits
        /// </summary>
        [JsonPropertyName("normal_limits")]
        public HTXContractElementsLimit[]? NormalLimits { get; set; }
        /// <summary>
        /// ["<c>open_limits</c>"] Open limits
        /// </summary>
        [JsonPropertyName("open_limits")]
        public HTXContractElementsLimit[]? OpenLimits { get; set; }
        /// <summary>
        /// ["<c>trade_limits</c>"] Trade limits
        /// </summary>
        [JsonPropertyName("trade_limits")]
        public HTXContractElementsLimit[]? TradeLimits { get; set; }

        /// <summary>
        /// ["<c>instrument_type</c>"] Instrument types
        /// </summary>
        [JsonPropertyName("instrument_type")]
        public ElementInstrumentType[] InstrumentTypes { get; set; } = Array.Empty<ElementInstrumentType>();
        /// <summary>
        /// ["<c>real_time_settlement</c>"] Real time settlement
        /// </summary>
        [JsonPropertyName("real_time_settlement")]
        public bool? RealTimeSettlement { get; set; }
        /// <summary>
        /// ["<c>transfer_profit_ratio</c>"] Transfer profit ratio
        /// </summary>
        [JsonPropertyName("transfer_profit_ratio")]
        public decimal? TransferProfitRatio { get; set; }
        /// <summary>
        /// ["<c>cross_transfer_profit_ratio</c>"] Cross transfer profit ratio
        /// </summary>
        [JsonPropertyName("cross_transfer_profit_ratio")]
        public decimal? CrossTransferProfitRatio { get; set; }
        /// <summary>
        /// ["<c>price_tick</c>"] Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal? PriceTick { get; set; }
        /// <summary>
        /// ["<c>instrument_value</c>"] Instrument value
        /// </summary>
        [JsonPropertyName("instrument_value")]
        public decimal? InstrumentValue { get; set; }
        /// <summary>
        /// ["<c>trade_partition</c>"] Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string? TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open_order_limit</c>"] Open order limit
        /// </summary>
        [JsonPropertyName("open_order_limit")]
        public decimal? OpenOrderLimit { get; set; }
        /// <summary>
        /// ["<c>offset_order_limit</c>"] Offset order limit
        /// </summary>
        [JsonPropertyName("offset_order_limit")]
        public decimal? OffsetOrderLimit { get; set; }
        /// <summary>
        /// ["<c>long_position_limit</c>"] Long position limit
        /// </summary>
        [JsonPropertyName("long_position_limit")]
        public decimal? LongPositionLimit { get; set; }
        /// <summary>
        /// ["<c>short_position_limit</c>"] Short position limit
        /// </summary>
        [JsonPropertyName("short_position_limit")]
        public decimal? ShortPositionLimit { get; set; }
        /// <summary>
        /// ["<c>contract_infos</c>"] Contract infos
        /// </summary>
        [JsonPropertyName("contract_infos")]
        public HTXContractElementsContractInfo[]? ContractInfos { get; set; }
    }
}
