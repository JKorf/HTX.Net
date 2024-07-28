
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account id and state
    /// </summary>
    public record HTXSubUserAccountId
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        [JsonPropertyName("accountId")]
        public long Id { get; set; }
        /// <summary>
        /// The state of the account
        /// </summary>
        [JsonPropertyName("accountStatus"), JsonConverter(typeof(EnumConverter))]
        public AccountState State { get; set; }
        /// <summary>
        /// Sub state
        /// </summary>
        public string? SubType { get; set; }
    }
}
