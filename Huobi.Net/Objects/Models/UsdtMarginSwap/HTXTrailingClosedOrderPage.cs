using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trailing order page
    /// </summary>
    public record HTXTrailingClosedOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<HTXTrailingClosedOrder> Orders { get; set; } = Array.Empty<HTXTrailingClosedOrder>();
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
    }

    /// <summary>
    /// Trailing order info
    /// </summary>
    public record HTXTrailingClosedOrder : HTXTrailingOrder
    {
        /// <summary>
        /// Triggered price
        /// </summary>
        [JsonPropertyName("triggered_price")]
        public decimal? TriggeredPrice { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Cancel time
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("fail_code")]
        public int? FailCode { get; set; }
        /// <summary>
        /// Error reason
        /// </summary>
        [JsonPropertyName("fail_reason")]
        public string? FailReason { get; set; }
        /// <summary>
        /// Relation order id
        /// </summary>
        [JsonPropertyName("relation_order_id")]
        public string? RelationOrderId { get; set; }
        /// <summary>
        /// Lowest/highest market price (use the lowest price when buy. use the highest when sell)
        /// </summary>
        [JsonPropertyName("market_limit_price")]
        public decimal? MarketLimitPrice { get; set; }
        /// <summary>
        /// Real volume
        /// </summary>
        [JsonPropertyName("real_volume")]
        public decimal? RealVolume { get; set; }
        /// <summary>
        /// Formula price(the lowest (highest) market price* (1 ± callback rate))
        /// </summary>
        [JsonPropertyName("formula_price")]
        public decimal? FormulaPrice { get; set; }
    }


}
