using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Margin order purpose
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginPurpose>))]
    public enum MarginPurpose
    {
        /// <summary>
        /// Loan
        /// </summary>
        [Map("1")]
        AutomaticLoan,
        /// <summary>
        /// Repayment
        /// </summary>
        [Map("2")]
        AutomaticRepayment
    }
}
