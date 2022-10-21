using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiSocketResponse2: HuobiResponse
    {
        internal override bool IsSuccessful => ErrorCode == "0";
        [JsonProperty("status")]
        internal string Status { get; set; } = string.Empty;

        [JsonProperty("cid")]
        internal string ClientId { get; set; } = string.Empty;

        [JsonProperty("topic")]
        internal string Topic { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }

    internal class HuobiSocketResponse2<T>: HuobiSocketResponse2
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; } = default!;
        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HuobiAuthResponse
    {
        internal bool IsSuccessful => Code == 0;
        [JsonProperty("err-code")]
        public int Code { get; set; }
        [JsonProperty("err-msg")]
        public string Message { get; set; } = String.Empty;
    }
}
