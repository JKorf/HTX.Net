using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// System status indicator
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SystemStatusIndicator>))]
    public enum SystemStatusIndicator
    {
        /// <summary>
        /// ["<c>none</c>"] None
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// ["<c>minor</c>"] Minor
        /// </summary>
        [Map("minor")]
        Minor,
        /// <summary>
        /// ["<c>major</c>"] Major
        /// </summary>
        [Map("major")]
        Major,
        /// <summary>
        /// ["<c>critical</c>"] Critical
        /// </summary>
        [Map("critical")]
        Critical,
        /// <summary>
        /// ["<c>maintenance</c>"] Maintenance
        /// </summary>
        [Map("maintenance")]
        Maintenance
    }
}
