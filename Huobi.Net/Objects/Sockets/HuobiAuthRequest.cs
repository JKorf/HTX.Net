using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiAuthRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
    }

    internal class HuobiAuthRequest<T> : HuobiAuthRequest
    {
        [JsonProperty("params")]
        public T Params { get; set; } = default!;
    }
}
