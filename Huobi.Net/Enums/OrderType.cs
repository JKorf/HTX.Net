namespace Huobi.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit
        /// </summary>
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        Market,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        IOC,
        /// <summary>
        /// Limit maker
        /// </summary>
        LimitMaker,
        /// <summary>
        /// Stop limit
        /// </summary>
        StopLimit,
        /// <summary>
        /// Fill or kill limit
        /// </summary>
        FillOrKillLimit,
        /// <summary>
        /// Fill or kill stop limit
        /// </summary>
        FillOrKillStopLimit,
    }
}
