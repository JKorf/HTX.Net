using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Internal
{
    internal class HTXV5Request
    {
        [JsonPropertyName("op")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("cid")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public Parameters Data { get; set; } = default!;
    }
}
