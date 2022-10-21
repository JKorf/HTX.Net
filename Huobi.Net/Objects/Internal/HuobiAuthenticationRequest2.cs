using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiAuthenticationRequest2
    {
        [JsonProperty("op")] public string Operation { get; set; } = "auth";
        [JsonProperty("type")] public string AuthType { get; set; } = "api";
        [JsonProperty("AccessKeyId")] public string AccessKey { get; set; } = string.Empty;
        [JsonProperty("SignatureMethod")] public string SignatureMethod { get; set; } = "HmacSHA256";
        [JsonProperty("SignatureVersion")] public string SignatureVersion { get; set; } = "2";
        [JsonProperty("Timestamp")] public string Timestamp { get; set; } = string.Empty;
        [JsonProperty("Signature")] public string Signature { get; set; } = string.Empty;

        public HuobiAuthenticationRequest2(string accessKey, string timestamp, string signature)
        {
            AccessKey = accessKey;
            Timestamp = timestamp;
            Signature = signature;
        }
    }
}
