using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trigger order update
    /// </summary>
    public record HTXUsdtMarginSwapIsolatedTriggerOrderUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventOrderTrigger EventOrderTrigger { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Trigger type
        /// </summary>
        [JsonPropertyName("trigger_type")]
        public TriggerType? TriggerType { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int? OrderType { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order id string
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string RelationOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType? OrderPriceType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter? OrderStatus { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public decimal? TriggeredPrice { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Triggered time
        /// </summary>
        [JsonPropertyName("triggered_at")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// Order insert time
        /// </summary>
        [JsonPropertyName("order_insert_at")]
        public DateTime? OrderInsertTime { get; set; }
        /// <summary>
        /// Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
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
        /// Fail code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public string? FailCode { get; set; }
        /// <summary>
        /// Fail reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }


}
