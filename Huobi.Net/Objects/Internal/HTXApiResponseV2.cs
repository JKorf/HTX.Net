namespace HTX.Net.Objects.Internal
{
    internal class HTXApiResponseV2<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
#pragma warning disable 8618
        [JsonPropertyName("data")]
        public T Data { get; set; }
#pragma warning restore 8618
    }
}
