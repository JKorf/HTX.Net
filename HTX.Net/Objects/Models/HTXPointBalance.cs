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
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>groupIds</c>"] Group ids
        /// </summary>
        [JsonPropertyName("groupIds")]
        public HTXPointBalanceGroup[] GroupIds { get; set; } = Array.Empty<HTXPointBalanceGroup>();
        /// <summary>
        /// ["<c>acctBalance</c>"] Accountt balance
        /// </summary>
        [JsonPropertyName("acctBalance")]
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// ["<c>accountStatus</c>"] Account status
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
        /// ["<c>groupId</c>"] Group id
        /// </summary>
        [JsonPropertyName("groupId")]
        public long GroupId { get; set; }
        /// <summary>
        /// ["<c>expiryDate</c>"] Expiry date
        /// </summary>
        [JsonPropertyName("expiryDate")]
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// ["<c>remainAmt</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("remainAmt")]
        public decimal RemainingQuantity { get; set; }
    }


}
