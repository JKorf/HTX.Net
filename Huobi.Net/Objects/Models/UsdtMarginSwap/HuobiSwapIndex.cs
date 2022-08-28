using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Swap index
    /// </summary>
    public class HuobiSwapIndex
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Index price
        /// </summary>
        [JsonProperty("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("index_ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
