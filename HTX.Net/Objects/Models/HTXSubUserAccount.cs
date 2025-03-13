using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The type of the account
        /// </summary>
        [JsonPropertyName("accountType")]
        public SubAccountMarketType Type { get; set; }
        /// <summary>
        /// Whether the account is active of not
        /// </summary>

        [JsonPropertyName("activation")]
        public AccountActivation Activation { get; set; }
        /// <summary>
        /// Whether transfers are allowed (only for spot account type)
        /// </summary>
        [JsonPropertyName("transferrable")]
        public bool? Transferrable { get; set; }
        /// <summary>
        /// Account ids
        /// </summary>
        [JsonPropertyName("accountIds")]
        public HTXSubUserAccountId[] AccountIds { get; set; } = Array.Empty<HTXSubUserAccountId>();
    }
}
