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
        /// ["<c>symbol</c>"] The asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_size</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>price_tick</c>"] Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// ["<c>delivery_date</c>"] Deliverty date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>delivery_time</c>"] Delivery time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// ["<c>create_date</c>"] Created date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// ["<c>contract_status</c>"] Status
        /// </summary>
        [JsonPropertyName("contract_status")]

        public ContractStatus Status { get; set; }
        /// <summary>
        /// ["<c>settlement_date</c>"] Settlement date
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// ["<c>support_margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("support_margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>

        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
    }
}
