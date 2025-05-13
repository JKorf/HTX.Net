using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Fee deduction status.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeDeductStatus>))]
    public enum FeeDeductStatus
    {
        /// <summary>
        /// In deduction
        /// </summary>
        [Map("ongoing")]
        Ongoing,
        /// <summary>
        /// Deduction completed
        /// </summary>
        [Map("done")]
        Done,
    }
}
