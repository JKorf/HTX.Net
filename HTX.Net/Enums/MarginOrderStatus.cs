using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of a margin order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderStatus>))]
    public enum MarginOrderStatus
    {
        /// <summary>
        /// Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// Loaned
        /// </summary>
        [Map("accrual")]
        Accural,
        /// <summary>
        /// Paid
        /// </summary>
        [Map("cleared")]
        Cleared,
        /// <summary>
        /// Invalid
        /// </summary>
        [Map("invalid")]
        Invalid,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("failed")]
        Failed
    }
}
