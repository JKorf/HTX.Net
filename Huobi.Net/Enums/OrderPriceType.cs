using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Order price type
    /// </summary>
    public enum OrderPriceType
    {
        /// <summary>
        /// Limit
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Best offer
        /// </summary>
        [Map("opponent")]
        BestOffer,
        /// <summary>
        /// Post only
        /// </summary>
        [Map("post_only")]
        PostOnly,
        /// <summary>
        /// Optimal 5
        /// </summary>
        [Map("optimal_5")]
        Optimal5,
        /// <summary>
        /// Optimal 10
        /// </summary>
        [Map("optimal_10")]
        Optimal10,
        /// <summary>
        /// Optimal 20
        /// </summary>
        [Map("optimal_20")]
        Optimal20,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("fok")]
        FillOrKill,
        /// <summary>
        /// Immediate or cancel at best bid
        /// </summary>
        [Map("opponent_ioc")]
        ImmediateOrCancelBestBid,
        /// <summary>
        /// Immediate or cancel optimal 5
        /// </summary>
        [Map("optimal_5_ioc")]
        ImmediateOrCancelOptimal5,
        /// <summary>
        /// Immediate or cancel optimal 10
        /// </summary>
        [Map("optimal_10_ioc")]
        ImmediateOrCancelOptimal10,
        /// <summary>
        /// Immediate or cancel optimal 20
        /// </summary>
        [Map("optimal_20_ioc")]
        ImmediateOrCancelOptimal20,
        /// <summary>
        /// Fill or kill best offer
        /// </summary>
        [Map("opponent_fok")]
        FillOrKillBestBid,
        /// <summary>
        /// Fill or kill optimal 5
        /// </summary>
        [Map("optimal_5_fok")]
        FillOrKillOptimal5,
        /// <summary>
        /// Fill or kill optimal 10
        /// </summary>
        [Map("optimal_10_fok")]
        FillOrKillOptimal10,
        /// <summary>
        /// Fill or kill optimal 20
        /// </summary>
        [Map("optimal_20_fok")]
        FillOrKillOptimal20,
    }
}
