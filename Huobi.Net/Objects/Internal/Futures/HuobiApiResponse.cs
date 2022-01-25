using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal.Futures
{
    internal abstract class HuobiFuturesApiResponse
    {
        [JsonProperty("status")]
        internal string? Status { get; set; }


        [JsonProperty("err_msg")]
        internal string? ErrorMessage { get; set; }
        [JsonProperty("err_code")]
        internal string? ErrorCode { get; set; }
    }

    internal class HuobiBasicFuturesResponse<T> : HuobiFuturesApiResponse
    {
        public T Data { get; set; } = default!;
        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
    }
}
