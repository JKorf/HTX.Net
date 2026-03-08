using HTX.Net.Enums;
using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Cross margin position update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapCrossPositionUpdate : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapCrossPositionUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossPositionUpdateData>();
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
    }

    /// <summary>
    /// Cross margin position data
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapCrossPositionUpdateData : HTXUsdtMarginSwapIsolatedPositionUpdateData
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
