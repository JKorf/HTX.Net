using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ElementInstrumentType>))]
    public enum ElementInstrumentType
    {
        /// <summary>
        /// ["<c>0</c>"] Perpetual futures
        /// </summary>
        [Map("0")]
        PerpetualFutures,
        /// <summary>
        /// ["<c>1</c>"] Weekly futures
        /// </summary>
        [Map("1")]
        WeeklyFutures,
        /// <summary>
        /// ["<c>2</c>"] Bi-weekly futures
        /// </summary>
        [Map("2")]
        BiWeeklyFutures,
        /// <summary>
        /// ["<c>3</c>"] Quarterly futures
        /// </summary>
        [Map("3")]
        QuarterlyFutures,
        /// <summary>
        /// ["<c>4</c>"] Bi-quareterly futures
        /// </summary>
        [Map("4")]
        BiQuarterlyFutures,
    }
}
