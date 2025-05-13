using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account lock
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountLock
    {
        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// User lock state
        /// </summary>
        [JsonPropertyName("userState")]
        public LockAction UserState { get; set; }
    }


}
