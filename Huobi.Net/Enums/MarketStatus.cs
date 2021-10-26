namespace Huobi.Net.Enums
{
    /// <summary>
    /// Status of the market
    /// </summary>
    public enum MarketStatus
    {
        /// <summary>
        /// Operating normally
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Trading halted
        /// </summary>
        Halted = 2,
        /// <summary>
        /// Only cancelation is possible
        /// </summary>
        CancelOnly = 3
    }
}
