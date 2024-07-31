using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trading status
    /// </summary>
    public record HTXTradingStatus
    {
        /// <summary>
        /// Is disable
        /// </summary>
        [JsonPropertyName("is_disable")]
        public bool IsDisable { get; set; }
        /// <summary>
        /// Order price types
        /// </summary>
        [JsonPropertyName("order_price_types")]
        public string OrderPriceTypes { get; set; } = string.Empty;
        /// <summary>
        /// Disable reason
        /// </summary>
        [JsonPropertyName("disable_reason")]
        public string DisableReason { get; set; } = string.Empty;
        /// <summary>
        /// Disable interval
        /// </summary>
        [JsonPropertyName("disable_interval")]
        public long DisableInterval { get; set; }
        /// <summary>
        /// Recovery time
        /// </summary>
        [JsonPropertyName("recovery_time")]
        public long RecoveryTime { get; set; }
        /// <summary>
        /// Cancel order ratio info
        /// </summary>
        [JsonPropertyName("COR")]
        public HTXTradingStatusCor CancelOrderRatio { get; set; } = null!;
        /// <summary>
        /// Total disables info
        /// </summary>
        [JsonPropertyName("TDN")]
        public HTXTradingStatusTdn TotalDisables { get; set; } = null!;
    }

    /// <summary>
    /// Cancel order ratio
    /// </summary>
    public record HTXTradingStatusCor
    {
        /// <summary>
        /// Orders threshold
        /// </summary>
        [JsonPropertyName("orders_threshold")]
        public int OrdersThreshold { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public int Orders { get; set; }
        /// <summary>
        /// Invalid cancel orders
        /// </summary>
        [JsonPropertyName("invalid_cancel_orders")]
        public int InvalidCancelOrders { get; set; }
        /// <summary>
        /// Cancel ratio threshold
        /// </summary>
        [JsonPropertyName("cancel_ratio_threshold")]
        public decimal CancelRatioThreshold { get; set; }
        /// <summary>
        /// Cancel ratio
        /// </summary>
        [JsonPropertyName("cancel_ratio")]
        public decimal CancelRatio { get; set; }
        /// <summary>
        /// Is trigger
        /// </summary>
        [JsonPropertyName("is_trigger")]
        public bool IsTrigger { get; set; }
        /// <summary>
        /// Is active
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Total disables
    /// </summary>
    public record HTXTradingStatusTdn
    {
        /// <summary>
        /// Disables threshold
        /// </summary>
        [JsonPropertyName("disables_threshold")]
        public int DisablesThreshold { get; set; }
        /// <summary>
        /// Disables
        /// </summary>
        [JsonPropertyName("disables")]
        public int Disables { get; set; }
        /// <summary>
        /// Is trigger
        /// </summary>
        [JsonPropertyName("is_trigger")]
        public bool IsTrigger { get; set; }
        /// <summary>
        /// Is active
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }


}
