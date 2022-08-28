using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Contract status
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// Delisting
        /// </summary>
        [Map("0")]
        Delisting,
        /// <summary>
        /// Listing
        /// </summary>
        [Map("1")]
        Listing,
        /// <summary>
        /// Pending listing
        /// </summary>
        [Map("2")]
        PendingListing,
        /// <summary>
        /// Suspension
        /// </summary>
        [Map("3")]
        Suspension,
        /// <summary>
        /// Suspending of listing
        /// </summary>
        [Map("4")]
        SuspendingOfListing,
        /// <summary>
        /// In settlement
        /// </summary>
        [Map("5")]
        InSettlement,
        /// <summary>
        /// Delivering
        /// </summary>
        [Map("6")]
        Delivering,
        /// <summary>
        /// Settlement completed
        /// </summary>
        [Map("7")]
        SettlementCompleted,
        /// <summary>
        /// Delivered
        /// </summary>
        [Map("8")]
        Delivered
    }
}
