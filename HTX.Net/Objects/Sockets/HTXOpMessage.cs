using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Sockets
{
    /// <summary>
    /// Message
    /// </summary>
    [SerializationModel]
    public record HTXOpMessage
    {
        /// <summary>
        /// Operation
        /// </summary>
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        /// <summary>
        /// Request id
        /// </summary>
        [JsonPropertyName("cid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RequestId { get; set; }
        /// <summary>
        /// Topic
        /// </summary>
        [JsonPropertyName("topic"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Topic { get; set; } = string.Empty;
    }

    [SerializationModel]
    internal record HTXOpPingMessage : HTXOpMessage
    {
        [JsonPropertyName("ts")]
        public long Timestamp { get; set; }
    }

    [SerializationModel]
    internal record HTXOpResponse : HTXOpMessage
    {
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("err-code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("err-msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
