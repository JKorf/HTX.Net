using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters.Futures;
using Huobi.Net.Enums.Futures;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Swaps
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSwapsSymbol
    {
        /// <summary>
        /// The product code
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// The contract size
        /// </summary>
        [JsonProperty("contract_size")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// The price tick
        /// </summary>
        [JsonProperty("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// The create date
        /// </summary>
        [JsonProperty("create_date"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// The delivery time
        /// </summary>
        [JsonProperty("delivery_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// The contract status
        /// </summary>
        [JsonProperty("contract_status"), JsonConverter(typeof(ContractStatusConverter))]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// The settlement date
        /// </summary>
        [JsonProperty("settlement_date"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementDate { get; set; }
    }
}
