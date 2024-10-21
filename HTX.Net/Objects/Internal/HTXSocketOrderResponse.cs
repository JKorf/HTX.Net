using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketOrderResponse<T>
    {
        [JsonIgnore()]
        public bool Success => Status.Equals("ok", StringComparison.Ordinal);

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public T? Data { get; set; }
        [JsonPropertyName("cid")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
    }
}
