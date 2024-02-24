using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiSocketAuthResponse
    {
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
