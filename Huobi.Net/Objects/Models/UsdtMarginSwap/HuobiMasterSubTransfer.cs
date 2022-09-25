using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Transfer between master and sub account
    /// </summary>
    public class HuobiMasterSubTransfer
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// From margin account
        /// </summary>
        [JsonProperty("from_margin_account")]
        public string FromMarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// To margin account
        /// </summary>
        [JsonProperty("to_margin_account")]
        public string ToMarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonProperty("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Sub account name
        /// </summary>
        [JsonProperty("sub_account_name")]
        public string SubAccountName { get; set; } = string.Empty;
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonProperty("tranfer_type")]
        [JsonConverter(typeof(EnumConverter))]
        public MasterSubTransferType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
