

namespace HTX.Net.Objects.Sockets
{
    internal class HTXSubscribeRequest
    {
        [JsonPropertyName("sub")]
        public string Topic { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
