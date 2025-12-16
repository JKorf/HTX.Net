namespace HTX.Net.Objects.Internal
{
    internal class HTXApiResponseV2
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }


    internal class HTXApiResponseV2<T> : HTXApiResponseV2
    {
#pragma warning disable 8618
        [JsonPropertyName("data")]
        public T Data { get; set; }
#pragma warning restore 8618
    }
}
