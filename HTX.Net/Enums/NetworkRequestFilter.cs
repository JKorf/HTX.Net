using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// GetNetworksAsync filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NetworkRequestFilter>))]
    public enum NetworkRequestFilter
    {
        /// <summary>
        /// ["<c>0</c>"] Do not return descriptions
        /// </summary>
        [Map("0")]
        NoDescriptions,
        /// <summary>
        /// ["<c>1</c>"] Include all descriptions
        /// </summary>
        [Map("1")]
        AllDescriptions,
        /// <summary>
        /// ["<c>2</c>"] Only include suspended withdrawal/deposit descriptions
        /// </summary>
        [Map("2")]
        SuspendedDescriptions
    }
}
