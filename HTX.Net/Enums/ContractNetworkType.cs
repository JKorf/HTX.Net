using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Contract network type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractNetworkType>))]
    public enum ContractNetworkType
    {
        /// <summary>
        /// ["<c>0</c>"] Coin
        /// </summary>
        [Map("0")]
        Coin,
        /// <summary>
        /// ["<c>1</c>"] Token
        /// </summary>
        [Map("1")]
        Token
    }
}
