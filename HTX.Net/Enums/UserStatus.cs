using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// User status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UserStatus>))]
    public enum UserStatus
    {
        /// <summary>
        /// ["<c>normal</c>"] Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// ["<c>lock</c>"] Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
