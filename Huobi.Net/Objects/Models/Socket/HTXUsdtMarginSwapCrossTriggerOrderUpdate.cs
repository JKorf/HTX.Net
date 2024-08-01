using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trigger order update
    /// </summary>
    public record HTXUsdtMarginSwapCrossTriggerOrderUpdate : HTXOpMessage
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
        public IEnumerable<HTXUsdtMarginSwapCrossTriggerOrderUpdateData> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossTriggerOrderUpdateData>();
    }

    /// <summary>
    /// Trigger update
    /// </summary>
    public record HTXUsdtMarginSwapCrossTriggerOrderUpdateData : HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData
    {
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
