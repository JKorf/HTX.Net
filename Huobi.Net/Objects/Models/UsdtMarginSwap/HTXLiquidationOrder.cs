using CryptoExchange.Net.Converters;
using HTX.Net.Enums;

using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Liquidation order page
    /// </summary>
    public record HTXLiquidationOrderPage
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HTXLiquidationOrder> Orders { get; set; } = Array.Empty<HTXLiquidationOrder>();
    }

    /// <summary>
    /// Liquidation order
    /// </summary>
    public record HTXLiquidationOrder
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
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
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal Turnover { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
