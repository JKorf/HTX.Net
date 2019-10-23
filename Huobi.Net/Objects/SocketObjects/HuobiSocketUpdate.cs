using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.SocketObjects
{
    internal class HuobiSocketUpdate<T>
    {
        /// <summary>
        /// The name of the data channel
        /// </summary>
        [JsonProperty("ch")]
        public string Channel { get; set; } = "";
        /// <summary>
        /// The timestamp of the update
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data of the update
        /// </summary>
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }
}
