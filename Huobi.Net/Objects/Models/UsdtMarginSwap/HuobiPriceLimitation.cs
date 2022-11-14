using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Price limitation
    /// </summary>
    public class HuobiPriceLimitation
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// High limit
        /// </summary>
        [JsonProperty("high_limit")]
        public decimal HighLimit { get; set; }
        /// <summary>
        /// Low limit
        /// </summary>
        [JsonProperty("low_limit")]
        public decimal LowLimit { get; set; }
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
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
