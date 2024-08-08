

namespace HTX.Net.Objects.Sockets
{
    internal class HTXAuthParams
    {
        [JsonPropertyName("authType")] public string AuthType { get; set; } = "api";
        [JsonPropertyName("accessKey")] public string AccessKey { get; set; } = string.Empty;
        [JsonPropertyName("signatureMethod")] public string SignatureMethod { get; set; } = "HmacSHA256";
        [JsonPropertyName("signatureVersion")] public string SignatureVersion { get; set; } = "2.1";
        [JsonPropertyName("timestamp")] public string Timestamp { get; set; } = string.Empty;
        [JsonPropertyName("signature")] public string Signature { get; set; } = string.Empty;
    }
}
