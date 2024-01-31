using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiSocketResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? Status { get; set; }
        [JsonProperty("err-code")]
        public string? ErrorCode { get; set; }
        [JsonProperty("err-msg")]
        public string? ErrorMessage { get; set; }
    }
}
