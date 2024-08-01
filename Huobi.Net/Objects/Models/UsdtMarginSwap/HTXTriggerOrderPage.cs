using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger order page
    /// </summary>
    public record HTXTriggerOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<HTXTriggerOrder> Orders { get; set; } = Array.Empty<HTXTriggerOrder>();
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public decimal TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public decimal CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Trigger order
    /// </summary>
    public record HTXTriggerOrder
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
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public int OrderType { get; set; }
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
        /// Order id str
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
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
        /// Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatusFilter Status { get; set; }
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
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }


}
