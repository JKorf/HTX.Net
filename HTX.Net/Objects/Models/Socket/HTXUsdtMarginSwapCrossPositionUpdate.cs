using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapCrossPositionUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossPositionUpdateData>();
        /// <summary>
        /// User id
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
