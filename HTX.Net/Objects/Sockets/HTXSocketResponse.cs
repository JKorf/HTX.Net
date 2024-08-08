

namespace HTX.Net.Objects.Sockets
{
    internal class HTXSocketResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
    }
}
