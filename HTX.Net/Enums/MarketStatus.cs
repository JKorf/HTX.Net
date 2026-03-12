using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Status of the market
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarketStatus>))]
    public enum MarketStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Operating normally
        /// </summary>
        [Map("1")]
        Normal = 1,
        /// <summary>
        /// ["<c>2</c>"] Trading halted
        /// </summary>
        [Map("2")]
        Halted = 2,
        /// <summary>
        /// ["<c>3</c>"] Only cancelation is possible
        /// </summary>
        [Map("3")]
        CancelOnly = 3
    }
}
