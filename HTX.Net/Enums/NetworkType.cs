using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Type of network
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NetworkType>))]
    public enum NetworkType
    {
        /// <summary>
        /// ["<c>plain</c>"] Plain
        /// </summary>
        [Map("plain")]
        Plain,
        /// <summary>
        /// ["<c>live</c>"] Live
        /// </summary>
        [Map("live")]
        Live,
        /// <summary>
        /// ["<c>old</c>"] Old
        /// </summary>
        [Map("old")]
        Old,
        /// <summary>
        /// ["<c>new</c>"] New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// ["<c>legal</c>"] Legal
        /// </summary>
        [Map("legal")]
        Legal,
        /// <summary>
        /// ["<c>tooold</c>"] Too old
        /// </summary>
        [Map("tooold")]
        TooOld
    }
}
