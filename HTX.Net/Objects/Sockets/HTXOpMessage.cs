namespace HTX.Net.Objects.Sockets
{
    /// <summary>
    /// Message
    /// </summary>
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
        [JsonPropertyName("cid")]
        public string? RequestId { get; set; }
        /// <summary>
        /// Topic
        /// </summary>
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;
    }

    internal record HTXOpPingMessage : HTXOpMessage
    {
        [JsonPropertyName("ts")]
        public long Timestamp { get; set; }
    }

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
