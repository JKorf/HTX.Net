using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiDataEvent<T>
    {
        /// <summary>
        /// The name of the data channel
        /// </summary>
        [JsonProperty("ch")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the update
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data of the update
        /// </summary>
        public T Data { get; set; } = default!;
        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }
    }
}
