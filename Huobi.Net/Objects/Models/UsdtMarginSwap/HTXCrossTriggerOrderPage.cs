using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trigger cross margin order page
    /// </summary>
    public record HTXCrossTriggerOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<HTXCrossTriggerOrder> Orders { get; set; } = Array.Empty<HTXCrossTriggerOrder>();
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
    public record HTXCrossTriggerOrder: HTXTriggerOrder
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}
