using HTX.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Open order
    /// </summary>
    [SerializationModel]
    public record HTXOpenOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// The order id as specified by the client
        /// </summary>
        [JsonPropertyName("client-order-id")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the account that placed the order
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonPropertyName("created-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// The time the order was canceled
        /// </summary>
        [JsonPropertyName("canceled-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// The time the order was completed
        /// </summary>
        [JsonPropertyName("finished-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType)!.Value;

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType)!.Value;

        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// The quantity of the order that is filled
        /// </summary>
        [JsonPropertyName("filled-amount")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Filled cash quantity
        /// </summary>
        [JsonPropertyName("filled-cash-amount")]
        public decimal QuoteQuantityFilled { get; set; }

        /// <summary>
        /// The quantity of fees paid for the filled quantity
        /// </summary>
        [JsonPropertyName("filled-fees")]
        public decimal Fee { get; set; }
    }
}
