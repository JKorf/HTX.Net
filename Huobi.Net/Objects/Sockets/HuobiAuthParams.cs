using Newtonsoft.Json;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiAuthParams
    {
        [JsonProperty("authType")] public string AuthType { get; set; } = "api";
        [JsonProperty("accessKey")] public string AccessKey { get; set; } = string.Empty;
        [JsonProperty("signatureMethod")] public string SignatureMethod { get; set; } = "HmacSHA256";
        [JsonProperty("signatureVersion")] public string SignatureVersion { get; set; } = "2.1";
        [JsonProperty("timestamp")] public string Timestamp { get; set; } = string.Empty;
        [JsonProperty("signature")] public string Signature { get; set; } = string.Empty;
    }
}
