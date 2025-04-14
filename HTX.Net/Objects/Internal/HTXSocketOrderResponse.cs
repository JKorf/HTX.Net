using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketOrderResponse<T>
    {
        [JsonInclude, JsonPropertyName("success")]
        internal bool SuccessInt { get; set; }
        [JsonIgnore()]
        public bool Success => SuccessInt || Status.Equals("ok", StringComparison.Ordinal);

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public T? Data { get; set; }
        [JsonPropertyName("cid")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        [JsonInclude, JsonPropertyName("code")]
        internal int? ErrorCodeInt
        {
            get => ErrorCode == null ? null: int.Parse(ErrorCode);
            set => ErrorCode = value?.ToString();
        }
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
        [JsonInclude, JsonPropertyName("message")]
        internal string? ErrorMessageInt
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }
    }
}
