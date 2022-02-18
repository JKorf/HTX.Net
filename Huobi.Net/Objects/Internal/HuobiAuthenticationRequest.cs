using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiAuthenticationRequest: HuobiAuthenticatedSubscribeRequest
    {
        [JsonProperty("params")] public HuobiAuthParams Parameters { get; set; }

        public HuobiAuthenticationRequest(string accessKey, string timestamp, string signature): base("auth", "req")
        {
            Parameters = new HuobiAuthParams()
            {
                AccessKey = accessKey,
                Timestamp = timestamp,
                Signature = signature
            };
        }
    }

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
