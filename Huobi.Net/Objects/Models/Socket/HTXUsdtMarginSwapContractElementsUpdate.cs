﻿using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using HTX.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.Socket
{
    internal record HTXUsdtMarginSwapContractElementsUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapContractElementsUpdate> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapContractElementsUpdate>();
    }

    /// <summary>
    /// Contract element info
    /// </summary>
    public record HTXUsdtMarginSwapContractElementsUpdate
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
        public decimal? FundingRateCap { get; set; }
        /// <summary>
        /// Funding rate floor
        /// </summary>
        [JsonPropertyName("funding_rate_floor")]
        public decimal? FundingRateFloor { get; set; }
        /// <summary>
        /// Mode type
        /// </summary>
        [JsonPropertyName("mode_type")]
        public ElementModeType? ModeType { get; set; }
        /// <summary>
        /// Swap delivery type
        /// </summary>
        [JsonPropertyName("swap_delivery_type")]
        public SwapDeliveryType? SwapDeliveryType { get; set; }
        /// <summary>
        /// Settle period
        /// </summary>
        [JsonPropertyName("settle_period")]
        public int? SettlePeriod { get; set; }
        /// <summary>
        /// Instrument index code
        /// </summary>
        [JsonPropertyName("instrument_index_code")]
        public string? InstrumentIndexCode { get; set; } = string.Empty;
        /// <summary>
        /// Price ticks
        /// </summary>
        [JsonPropertyName("price_ticks")]
        public IEnumerable<HTXContractElementsPriceTick>? PriceTicks { get; set; }
        /// <summary>
        /// Instrument values
        /// </summary>
        [JsonPropertyName("instrument_values")]
        public IEnumerable<HTXContractElementsPriceTick>? InstrumentValues { get; set; }
        /// <summary>
        /// Min level
        /// </summary>
        [JsonPropertyName("min_level")]
        public decimal? MinLevel { get; set; }
        /// <summary>
        /// Max level
        /// </summary>
        [JsonPropertyName("max_level")]
        public decimal? MaxLevel { get; set; }
        /// <summary>
        /// Order limits
        /// </summary>
        [JsonPropertyName("order_limits")]
        public IEnumerable<HTXContractElementsOrderLimit>? OrderLimits { get; set; }
        /// <summary>
        /// Normal limits
        /// </summary>
        [JsonPropertyName("normal_limits")]
        public IEnumerable<HTXContractElementsLimit>? NormalLimits { get; set; }
        /// <summary>
        /// Open limits
        /// </summary>
        [JsonPropertyName("open_limits")]
        public IEnumerable<HTXContractElementsLimit>? OpenLimits { get; set; }
        /// <summary>
        /// Trade limits
        /// </summary>
        [JsonPropertyName("trade_limits")]
        public IEnumerable<HTXContractElementsLimit>? TradeLimits { get; set; }
        /// <summary>
        /// Real time settlement
        /// </summary>
        [JsonPropertyName("real_time_settlement")]
        public bool? RealTimeSettlement { get; set; }
        /// <summary>
        /// Transfer profit ratio
        /// </summary>
        [JsonPropertyName("transfer_profit_ratio")]
        public decimal? TransferProfitRatio { get; set; }
        /// <summary>
        /// Cross transfer profit ratio
        /// </summary>
        [JsonPropertyName("cross_transfer_profit_ratio")]
        public decimal? CrossTransferProfitRatio { get; set; }
        /// <summary>
        /// Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal? PriceTick { get; set; }
        /// <summary>
        /// Instrument value
        /// </summary>
        [JsonPropertyName("instrument_value")]
        public decimal? InstrumentValue { get; set; }
        /// <summary>
        /// Trade partition
        /// </summary>
        [JsonPropertyName("trade_partition")]
        public string? TradePartition { get; set; } = string.Empty;
        /// <summary>
        /// Open order limit
        /// </summary>
        [JsonPropertyName("open_order_limit")]
        public decimal? OpenOrderLimit { get; set; }
        /// <summary>
        /// Offset order limit
        /// </summary>
        [JsonPropertyName("offset_order_limit")]
        public decimal? OffsetOrderLimit { get; set; }
        /// <summary>
        /// Long position limit
        /// </summary>
        [JsonPropertyName("long_position_limit")]
        public decimal? LongPositionLimit { get; set; }
        /// <summary>
        /// Short position limit
        /// </summary>
        [JsonPropertyName("short_position_limit")]
        public decimal? ShortPositionLimit { get; set; }
        /// <summary>
        /// Contract infos
        /// </summary>
        [JsonPropertyName("contract_infos")]
        public IEnumerable<HTXContractElementsContractInfo>? ContractInfos { get; set; }
    }
}
