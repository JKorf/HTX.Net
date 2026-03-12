using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Unit type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Unit>))]
    public enum Unit
    {
        /// <summary>
        /// ["<c>1</c>"] Cont
        /// </summary>
        [Map("1")]
        Cont,
        /// <summary>
        /// ["<c>2</c>"] Crypto currency
        /// </summary>
        [Map("2")]
        CryptoCurrency
    }
}
