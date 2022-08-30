using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Estimated settlement price
    /// </summary>
    public class HuobiEstimatedSettlementPrice
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Estimated settlement price
        /// </summary>
        [JsonProperty("estimated_settlement_price")]
        public decimal? EstimatedSettlementPrice { get; set; }
        /// <summary>
        /// Settlement type
        /// </summary>
        [JsonProperty("settlement_type")]
        [JsonConverter(typeof(EnumConverter))]
        public SettlementType SettlementType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
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
    }
}
