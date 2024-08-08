

namespace HTX.Net.Objects.Internal
{
    internal class HTXAuthenticationRequest2
    {
        [JsonPropertyName("op")] public string Operation { get; set; } = "auth";
        [JsonPropertyName("type")] public string AuthType { get; set; } = "api";
        [JsonPropertyName("AccessKeyId")] public string AccessKey { get; set; } = string.Empty;
        [JsonPropertyName("SignatureMethod")] public string SignatureMethod { get; set; } = "HmacSHA256";
        [JsonPropertyName("SignatureVersion")] public string SignatureVersion { get; set; } = "2";
        [JsonPropertyName("Timestamp")] public string Timestamp { get; set; } = string.Empty;
        [JsonPropertyName("Signature")] public string Signature { get; set; } = string.Empty;

        public HTXAuthenticationRequest2(string accessKey, string timestamp, string signature)
        {
            AccessKey = accessKey;
            Timestamp = timestamp;
            Signature = signature;
        }
    }
}
