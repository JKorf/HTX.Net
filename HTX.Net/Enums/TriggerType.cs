using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// ["<c>ge</c>"] Greater than or equal to price
        /// </summary>
        [Map("ge")]
        GreaterThanOrEqual,
        /// <summary>
        /// ["<c>le</c>"] Lesser than or equal to price
        /// </summary>
        [Map("le")]
        LesserThanOrEqual
    }
}
