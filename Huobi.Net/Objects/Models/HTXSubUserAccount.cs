using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account info
    /// </summary>
    public record HTXSubUserAccount
    {
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonPropertyName("accountType"), JsonConverter(typeof(EnumConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Whether the account is active of not
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountActivation Activation { get; set; }
        /// <summary>
        /// Whether transfers are allowed (only for spot account type)
        /// </summary>
        public bool? Transferrable { get; set; }
        /// <summary>
        /// Account ids
        /// </summary>
        public IEnumerable<HTXSubUserAccountId> AccountIds { get; set; } = Array.Empty<HTXSubUserAccountId>();
    }
}
