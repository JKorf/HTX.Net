namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketResponse2: HTXResponse
    {
        internal override bool IsSuccessful => string.Equals(ErrorCode, "0", StringComparison.Ordinal);
        [JsonInclude, JsonPropertyName("status")]
        internal string Status { get; set; } = string.Empty;

        [JsonInclude, JsonPropertyName("cid")]
        internal string ClientId { get; set; } = string.Empty;

        [JsonInclude, JsonPropertyName("topic")]
        internal string Topic { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }

    internal class HTXSocketResponse2<T>: HTXSocketResponse2
    {
        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonInclude, JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HTXAuthResponse
    {
        internal bool IsSuccessful => Code == 0;
        [JsonPropertyName("err-code")]
        public int Code { get; set; }
        [JsonPropertyName("err-msg")]
        public string Message { get; set; } = String.Empty;
    }
}
