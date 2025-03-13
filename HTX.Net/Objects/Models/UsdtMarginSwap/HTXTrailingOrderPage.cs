using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trailing order page
    /// </summary>
    [SerializationModel]
    public record HTXTrailingOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXTrailingOrder[] Orders { get; set; } = Array.Empty<HTXTrailingOrder>();
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total results
        /// </summary>
        [JsonPropertyName("total_size")]
        public int Total { get; set; }
    }

    /// <summary>
    /// Trailing order info
    /// </summary>
    [SerializationModel]
    public record HTXTrailingOrder
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
        /// Quantity
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public MarginOrderType OrderType { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public int LeverageRate { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order id str
        /// </summary>
        [JsonPropertyName("order_id_str")]
        public string OrderIdStr { get; set; } = string.Empty;
        /// <summary>
        /// Order source
        /// </summary>
        [JsonPropertyName("order_source")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType OrderPriceType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public TpSlStatus Status { get; set; }
        /// <summary>
        /// Callback rate
        /// </summary>
        [JsonPropertyName("callback_rate")]
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// Active price
        /// </summary>
        [JsonPropertyName("active_price")]
        public decimal ActivePrice { get; set; }
        /// <summary>
        /// Is active
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }


}
