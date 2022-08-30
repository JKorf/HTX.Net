using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Liquidation order page
    /// </summary>
    public class HuobiLiquidationOrderPage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalSize { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HuobiLiquidationOrder> Orders { get; set; } = Array.Empty<HuobiLiquidationOrder>();
    }

    /// <summary>
    /// Liquidation order
    /// </summary>
    public class HuobiLiquidationOrder
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Direction
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public Offset Offset { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal Turnover { get; set; }
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
    }
}
