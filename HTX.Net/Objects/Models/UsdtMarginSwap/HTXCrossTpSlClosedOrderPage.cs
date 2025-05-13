using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Take profit / stop loss order page
    /// </summary>
    [SerializationModel]
    public record HTXCrossTpSlClosedOrderPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public HTXCrossTpSlClosedOrder[] Orders { get; set; } = Array.Empty<HTXCrossTpSlClosedOrder>();
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
    /// Tp/Sl order
    /// </summary>
    [SerializationModel]
    public record HTXCrossTpSlClosedOrder: HTXTpSlClosedOrder
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]

        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }


}
