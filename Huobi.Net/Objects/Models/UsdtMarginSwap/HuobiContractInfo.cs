using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract info
    /// </summary>
    public class HuobiContractInfo
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
        /// Contract size
        /// </summary>
        [JsonProperty("contract_size")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Price tick
        /// </summary>
        [JsonProperty("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// Deliverty date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Delivery time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// Created date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("contract_status")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractStatus Status { get; set; }
        /// <summary>
        /// Settlement date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("support_margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
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
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
