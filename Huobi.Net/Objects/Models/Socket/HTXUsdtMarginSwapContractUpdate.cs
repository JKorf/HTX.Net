using HTX.Net.Objects.Sockets;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// 
    /// </summary>
    internal record HTXUsdtMarginSwapContractUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapContractUpdate> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapContractUpdate>();
    }

    /// <summary>
    /// Contract update
    /// </summary>
    public record HTXUsdtMarginSwapContractUpdate
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
        /// Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// Price tick
        /// </summary>
        [JsonPropertyName("price_tick")]
        public decimal PriceTick { get; set; }
        /// <summary>
        /// Settlement date
        /// </summary>
        [JsonPropertyName("settlement_date")]
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// Create date
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Contract status
        /// </summary>
        [JsonPropertyName("contract_status")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// Support margin mode
        /// </summary>
        [JsonPropertyName("support_margin_mode")]
        public MarginMode SupportMarginMode { get; set; }
        /// <summary>
        /// Delivery time
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
    }


}
