using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account info
    /// </summary>
    [SerializationModel]
    public record HTXSubUserAccount
    {
        /// <summary>
        /// ["<c>accountType</c>"] The type of the account
        /// </summary>
        [JsonPropertyName("accountType")]
        public SubAccountMarketType Type { get; set; }
        /// <summary>
        /// ["<c>activation</c>"] Whether the account is active of not
        /// </summary>

        [JsonPropertyName("activation")]
        public AccountActivation Activation { get; set; }
        /// <summary>
        /// ["<c>transferrable</c>"] Whether transfers are allowed (only for spot account type)
        /// </summary>
        [JsonPropertyName("transferrable")]
        public bool? Transferrable { get; set; }
        /// <summary>
        /// ["<c>accountIds</c>"] Account ids
        /// </summary>
        [JsonPropertyName("accountIds")]
        public HTXSubUserAccountId[] AccountIds { get; set; } = Array.Empty<HTXSubUserAccountId>();
    }
}
