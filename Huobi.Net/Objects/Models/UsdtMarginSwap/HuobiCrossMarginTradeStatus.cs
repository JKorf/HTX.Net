using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin trade status
    /// </summary>
    public class HuobiCrossMarginTradeStatus
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
        /// Margin mode
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Open order access
        /// </summary>
        public bool Open { get; set; }
        /// <summary>
        /// Close order access
        /// </summary>
        public bool Close { get; set; }
        /// <summary>
        /// Cancel order access
        /// </summary>
        public bool Cancel { get; set; }
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
