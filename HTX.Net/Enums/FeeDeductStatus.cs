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
        /// ["<c>ongoing</c>"] In deduction
        /// </summary>
        [Map("ongoing")]
        Ongoing,
        /// <summary>
        /// ["<c>done</c>"] Deduction completed
        /// </summary>
        [Map("done")]
        Done,
    }
}
