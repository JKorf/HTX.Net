namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketOrderRequest<T>
    {
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public T? Params { get; set; }
        [JsonPropertyName("cid")]
        public string RequestId { get; set; } = string.Empty;
    }
}
