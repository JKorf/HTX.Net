using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiSocketUpdate<T>
    {
        [JsonProperty("ch")]
        public string Channel { get; set; }
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        [JsonOptionalProperty]
        public T Data { get; set; }
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }
}
