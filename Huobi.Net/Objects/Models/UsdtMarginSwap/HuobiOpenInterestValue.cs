using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Open interest value
    /// </summary>
    public class HuobiOpenInterestValue
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("business_type")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Tick
        /// </summary>
        public IEnumerable<HuobiOpenInterestValueTick> Tick { get; set; } = Array.Empty<HuobiOpenInterestValueTick>();
    }

    /// <summary>
    /// Open interest value tick
    /// </summary>
    public class HuobiOpenInterestValueTick
    {
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Unit
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("amount_type")]
        public Unit Unit { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }
    }
}
