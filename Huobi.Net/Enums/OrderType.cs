namespace Huobi.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit buy
        /// </summary>
        LimitBuy,
        /// <summary>
        /// Limit sell
        /// </summary>
        LimitSell,
        /// <summary>
        /// Market buy
        /// </summary>
        MarketBuy,
        /// <summary>
        /// Market sell
        /// </summary>
        MarketSell,
        /// <summary>
        /// Immediate or cancel guy
        /// </summary>
        IOCBuy,
        /// <summary>
        /// Immediate or cancel sell
        /// </summary>
        IOCSell,
        /// <summary>
        /// Limit maker buy
        /// </summary>
        LimitMakerBuy,
        /// <summary>
        /// Limit maker sell
        /// </summary>
        LimitMakerSell,
        /// <summary>
        /// Stop limit buy
        /// </summary>
        StopLimitBuy,
        /// <summary>
        /// Stop limit sell
        /// </summary>
        StopLimitSell,
        /// <summary>
        /// Fill or kill limit buy
        /// </summary>
        FillOrKillLimitBuy,
        /// <summary>
        /// Fill or kill limit sell
        /// </summary>
        FillOrKillLimitSell,
        /// <summary>
        /// Fill or kill stop limit buy
        /// </summary>
        FillOrKillStopLimitBuy,
        /// <summary>
        /// Fill or kill stop limit sell
        /// </summary>
        FillOrKillStopLimitSell,
    }
}
