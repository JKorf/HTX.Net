using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trading fee info
    /// </summary>
    public class HuobiTradingFee
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
        /// Open position maker fee
        /// </summary>
        [JsonProperty("open_maker_fee")]
        public decimal OpenMakerFee { get; set; }
        /// <summary>
        /// Open position taker fee
        /// </summary>
        [JsonProperty("open_taker_fee")]
        public decimal OpenTakerFee { get; set; }
        /// <summary>
        /// Close position maker fee
        /// </summary>
        [JsonProperty("close_maker_fee")]
        public decimal CloseMakerfee { get; set; }
        /// <summary>
        /// Close position taker fee
        /// </summary>
        [JsonProperty("close_taker_fee")]
        public decimal CloseTakerFee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Delivery fee
        /// </summary>
        [JsonProperty("delivery_fee")]
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
