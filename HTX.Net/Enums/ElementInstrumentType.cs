using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Instrument type
    /// </summary>
    public enum ElementInstrumentType
    {
        /// <summary>
        /// Perpetual futures
        /// </summary>
        [Map("0")]
        PerpetualFutures,
        /// <summary>
        /// Weekly futures
        /// </summary>
        [Map("1")]
        WeeklyFutures,
        /// <summary>
        /// Bi-weekly futures
        /// </summary>
        [Map("2")]
        BiWeeklyFutures,
        /// <summary>
        /// Quarterly futures
        /// </summary>
        [Map("3")]
        QuarterlyFutures,
        /// <summary>
        /// Bi-quareterly futures
        /// </summary>
        [Map("4")]
        BiQuarterlyFutures,
    }
}
