using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.SocketObjects
{
    public abstract class HuobiResponse
    {
        public abstract bool IsSuccessfull { get; }
        public string Id { get; set; }
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }
        [JsonProperty("err-msg")]
        public string ErrorMessage { get; set; }
    }

    public class HuobiSocketResponse<T>: HuobiResponse
    {
        public override bool IsSuccessfull => Status == "ok";
        public string Status { get; set; }
        
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        [JsonOptionalProperty]
        public T Data { get; set; }
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    public class HuobiSubscribeResponse: HuobiResponse
    {
        public override bool IsSuccessfull => Status == "ok";
        public string Status { get; set; }
        public string Subbed { get; set; }
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("ts")]
        public DateTime Timestamp { get; set; }
    }

    public class HuobiSocketAuthResponse: HuobiResponse
    {
        public override bool IsSuccessfull => ErrorCode == 0;
        [JsonProperty("err-code")]
        public new int ErrorCode { get; set; }

        [JsonProperty("op")]
        public string Operation { get; set; }
        public string Topic { get; set; }
        [JsonProperty("cid")]
        public new string Id { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    public class HuobiSocketAuthDataResponse<T>: HuobiSocketAuthResponse
    {
        [JsonOptionalProperty]
        public T Data { get; set; }
    }
}
