using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Stop price operator
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Operator>))]
    public enum Operator
    {
        /// <summary>
        /// Greater than or equal to stop price
        /// </summary>
        [Map("gte")]
        GreaterThanOrEqual,
        /// <summary>
        /// Less than or equal to stop price
        /// </summary>
        [Map("lte")]
        LesserThanOrEqual
    }
}
