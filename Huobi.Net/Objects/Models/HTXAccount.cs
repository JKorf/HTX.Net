using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX account info
    /// </summary>
    public record HTXAccount
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The state of the account
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountState State { get; set; }
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Sub state
        /// </summary>
        public string? SubType { get; set; }
    }
}
