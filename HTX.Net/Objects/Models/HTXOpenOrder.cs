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
        /// ["<c>id</c>"] The id of the order
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>client-order-id</c>"] The order id as specified by the client
        /// </summary>
        [JsonPropertyName("client-order-id")]
        [JsonConverter(typeof(ClientIdConverter))]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>account-id</c>"] The id of the account that placed the order
        /// </summary>
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>price</c>"] The price of the order
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
        /// ["<c>type</c>"] The raw type string
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
        /// ["<c>source</c>"] The source of the order
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>state</c>"] The state of the order
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>filled-amount</c>"] The quantity of the order that is filled
        /// </summary>
        [JsonPropertyName("filled-amount")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// ["<c>filled-cash-amount</c>"] Filled cash quantity
        /// </summary>
        [JsonPropertyName("filled-cash-amount")]
        public decimal QuoteQuantityFilled { get; set; }

        /// <summary>
        /// ["<c>filled-fees</c>"] The quantity of fees paid for the filled quantity
        /// </summary>
        [JsonPropertyName("filled-fees")]
        public decimal Fee { get; set; }
    }
}
