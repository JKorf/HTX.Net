

namespace HTX.Net.Objects.Sockets
{
    internal class HTXAuthRequest
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
    }

    internal class HTXAuthRequest<T> : HTXAuthRequest
    {
        [JsonPropertyName("params")]
        public T Params { get; set; } = default!;
    }
}
