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
        /// ["<c>1</c>"] Loan
        /// </summary>
        [Map("1")]
        AutomaticLoan,
        /// <summary>
        /// ["<c>2</c>"] Repayment
        /// </summary>
        [Map("2")]
        AutomaticRepayment
    }
}
