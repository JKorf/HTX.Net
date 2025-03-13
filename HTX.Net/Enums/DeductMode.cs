using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Sub account deduct mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DeductMode>))]
    public enum DeductMode
    {
        /// <summary>
        /// Deduct from master
        /// </summary>
        [Map("master")]
        Master,
        /// <summary>
        /// Deduct from sub
        /// </summary>
        [Map("sub")]
        Sub
    }
}
