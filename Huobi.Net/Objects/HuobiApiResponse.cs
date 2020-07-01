using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects
{
    internal abstract class HuobiApiResponse
    {
        [JsonOptionalProperty, JsonProperty("status")]
        internal string? Status { get; set; }


        [JsonOptionalProperty, JsonProperty("err-msg")]
        internal string? ErrorMessage { get; set; }
        [JsonOptionalProperty, JsonProperty("err-code")]
        internal string? ErrorCode { get; set; }
    }

    internal class HuobiBasicResponse<T> : HuobiApiResponse
    {
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; } = "";
    }
}
