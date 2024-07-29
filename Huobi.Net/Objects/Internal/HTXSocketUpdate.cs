namespace HTX.Net.Objects.Internal
{
    internal class HTXDataEvent<T>
    {
        /// <summary>
        /// The action
        /// </summary>
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;
        /// <summary>
        /// The name of the data channel
        /// </summary>
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the update
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data of the update
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonInclude, JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
    }
}
