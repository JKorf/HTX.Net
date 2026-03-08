using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Trigger order update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapCrossTriggerOrderUpdate : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventOrderTrigger EventOrderTrigger { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapCrossTriggerOrderUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossTriggerOrderUpdateData>();
    }

    /// <summary>
    /// Trigger update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapCrossTriggerOrderUpdateData : HTXUsdtMarginSwapIsolatedTriggerOrderUpdateData
    {
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
