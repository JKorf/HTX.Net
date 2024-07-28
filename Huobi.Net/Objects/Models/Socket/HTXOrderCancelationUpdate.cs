using System;
using CryptoExchange.Net.Converters;

using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Cancelation details
    /// </summary>
    public record HTXOrderCancelationUpdate
    {
        /// <summary>
        /// Event type
        /// </summary>
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("orderSide"), JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("orderType"), JsonConverter(typeof(EnumConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Order price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("orderSize")]
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        public string? Operator { get; set; }
        /// <summary>
        /// Order creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderCreateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderState OrderStatus { get; set; }

        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal QuantityRemaining { get; set; }
    }
}
