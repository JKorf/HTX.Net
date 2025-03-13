using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Point balance
    /// </summary>
    [SerializationModel]
    public record HTXPointBalance
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Group ids
        /// </summary>
        [JsonPropertyName("groupIds")]
        public HTXPointBalanceGroup[] GroupIds { get; set; } = Array.Empty<HTXPointBalanceGroup>();
        /// <summary>
        /// Accountt balance
        /// </summary>
        [JsonPropertyName("acctBalance")]
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// Account status
        /// </summary>
        [JsonPropertyName("accountStatus")]
        public PointAccountStatus AccountStatus { get; set; }
    }

    /// <summary>
    /// Group info
    /// </summary>
    [SerializationModel]
    public record HTXPointBalanceGroup
    {
        /// <summary>
        /// Group id
        /// </summary>
        [JsonPropertyName("groupId")]
        public long GroupId { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        [JsonPropertyName("expiryDate")]
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal RemainingQuantity { get; set; }
    }


}
