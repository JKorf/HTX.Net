using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.SocketObjects
{
    internal abstract class HuobiResponse
    {
        internal abstract bool IsSuccessful { get; }
        public string? Id { get; set; }
        [JsonProperty("err-code")]
        public string? ErrorCode { get; set; }
        [JsonProperty("err-msg")]
        public string? ErrorMessage { get; set; }
    }

    internal class HuobiSocketResponse<T>: HuobiResponse
    {
        internal override bool IsSuccessful => Status == "ok";
        [JsonProperty("status")] internal string Status { get; set; } = "";
        
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HuobiSubscribeResponse: HuobiResponse
    {
        internal override bool IsSuccessful => Status == "ok";
        public string Status { get; set; } = "";
        public string Subbed { get; set; } = "";
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal class HuobiAuthSubscribeResponse
    {
        internal bool IsSuccessful => Code == 200;
        public int Code { get; set; }
        public string Message { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; } = "";
        public string Action { get; set; } = "";
    }
    
}
