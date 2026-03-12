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
        /// ["<c>gte</c>"] Greater than or equal to stop price
        /// </summary>
        [Map("gte")]
        GreaterThanOrEqual,
        /// <summary>
        /// ["<c>lte</c>"] Less than or equal to stop price
        /// </summary>
        [Map("lte")]
        LesserThanOrEqual
    }
}
