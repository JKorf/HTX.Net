using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Objects.Sockets;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Liquidation update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapLiquidationUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapLiquidationUpdateData[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapLiquidationUpdateData>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapLiquidationUpdateData
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Liquidation quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade turnover
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal TradeTurnover { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
    }


}
