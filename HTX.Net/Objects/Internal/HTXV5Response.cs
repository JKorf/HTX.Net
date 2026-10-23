using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Internal
{
    internal class HTXV5Response
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("cid")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal class HTXV5Response<T> : HTXV5Response
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
