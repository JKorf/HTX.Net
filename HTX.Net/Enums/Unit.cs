using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Cont
        /// </summary>
        [Map("1")]
        Cont,
        /// <summary>
        /// Crypto currency
        /// </summary>
        [Map("2")]
        CryptoCurrency
    }
}
