using HTX.Net.Objects.Sockets;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    internal record HTXUsdtMarginSwapContractUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapContractUpdate[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapContractUpdate>();
    }

    /// <summary>
    /// Contract update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapContractUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_size</c>"] Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// ["<c>price_tick</c>"] Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// ["<c>settlement_date</c>"] Settlement date
        /// </summary>
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// ["<c>create_date</c>"] Create date
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// ["<c>contract_status</c>"] Contract status
        /// </summary>
        [JsonPropertyName("contract_status")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// ["<c>support_margin_mode</c>"] Support margin mode
        /// </summary>
        [JsonPropertyName("support_margin_mode")]
        public MarginMode SupportMarginMode { get; set; }
        /// <summary>
        /// ["<c>delivery_time</c>"] Delivery time
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>delivery_date</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
    }


}
