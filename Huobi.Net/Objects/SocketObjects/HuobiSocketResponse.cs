using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.SocketObjects
{
    public abstract class HuobiResponse
    {
        internal abstract bool IsSuccessfull { get; }
        internal string Id { get; set; }
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }
        [JsonProperty("err-msg")]
        public string ErrorMessage { get; set; }
    }

    public class HuobiSocketResponse<T>: HuobiResponse
    {
        internal override bool IsSuccessfull => Status == "ok";
        [JsonProperty("status")]
        internal string Status { get; set; }
        
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        [JsonOptionalProperty]
        public T Data { get; set; }
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HuobiSubscribeResponse: HuobiResponse
    {
        internal override bool IsSuccessfull => Status == "ok";
        public string Status { get; set; }
        public string Subbed { get; set; }
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("ts")]
        public DateTime Timestamp { get; set; }
    }

    public class HuobiSocketAuthResponse: HuobiResponse
    {
        internal override bool IsSuccessfull => ErrorCode == 0;
        [JsonProperty("err-code")]
        internal new int ErrorCode { get; set; }

        [JsonProperty("op")]
        internal string Operation { get; set; }
        [JsonProperty("topic")]
        internal string Topic { get; set; }
        [JsonProperty("cid")]
        internal new string Id { get; set; }

        /// <summary>
        /// The timestamp of the response
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    public class HuobiSocketAuthDataResponse<T>: HuobiSocketAuthResponse
    {
        /// <summary>
        /// The data
        /// </summary>
        [JsonOptionalProperty]
        public T Data { get; set; }
    }
}
