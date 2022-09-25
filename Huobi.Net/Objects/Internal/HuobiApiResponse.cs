using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal abstract class HuobiApiResponse
    {
        [JsonProperty("status")]
        internal string? Status { get; set; }


        [JsonProperty("err-msg")]
        internal string? ErrorMessage { get; set; }
        [JsonProperty("err_msg")]
        private string? ErrorMessageInternal
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }
        [JsonProperty("err-code")]
        internal string? ErrorCode { get; set; }
        [JsonProperty("err_code")]
        private string? ErrorCodeInternal
        {
            get => ErrorCode;
            set => ErrorCode = value;
        }
        [JsonProperty("code")]
        private string? ErrorCodeInternal2
        {
            get => ErrorCode;
            set => ErrorCode = value;
        }
    }

    internal class HuobiBasicResponse<T> : HuobiApiResponse
    {
        public T Data { get; set; } = default!;
        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
        [JsonProperty("ticks")]
        private T Ticks { set => Data = value; get => Data; }
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
        [JsonProperty("next-time"), JsonConverter(typeof(DateTimeConverter))]
        private DateTime NextTime { get => Timestamp; set => Timestamp = value; }
    }
}
