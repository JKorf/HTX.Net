using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
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
        [JsonProperty("status")] internal string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; } = default!;
        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HuobiSubscribeResponse: HuobiResponse
    {
        internal override bool IsSuccessful => Status == "ok";
        public string Status { get; set; } = string.Empty;
        public string Subbed { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter)), JsonProperty("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal class HuobiAuthSubscribeResponse
    {
        internal bool IsSuccessful => Code == 200;
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
    
}
