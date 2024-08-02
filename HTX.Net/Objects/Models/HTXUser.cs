
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX user info
    /// </summary>
    public record HTXUser
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        [JsonPropertyName("uid")]
        public long Id { get; set; }
        /// <summary>
        /// The status of the user
        /// </summary>
        [JsonPropertyName("userState"), JsonConverter(typeof(EnumConverter))]
        public UserStatus Status { get; set; }
    }
}
