using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Funding fee type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FundingFeeType>))]
    public enum FundingFeeType
    {
        /// <summary>
        /// ["<c>normal</c>"] Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// ["<c>dividend_settlement</c>"] Divident settlement
        /// </summary>
        [Map("dividend_settlement")]
        DeividendSettlement
    }
}
