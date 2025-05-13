using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract info
    /// </summary>
    [SerializationModel]
    public record HTXContractInfo
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// Deliverty date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Delivery time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// Created date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("contract_status")]

        public ContractStatus Status { get; set; }
        /// <summary>
        /// Settlement date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("support_margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>

        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
