namespace Huobi.Net.Enums
{
    /// <summary>
    /// Event type
    /// </summary>
    public enum AccountEventType
    {
        /// <summary>
        /// Order placed event
        /// </summary>
        OrderPlaced,
        /// <summary>
        /// Order matched event
        /// </summary>
        OrderMatched,
        /// <summary>
        /// Order refunded event
        /// </summary>
        OrderRefunded,
        /// <summary>
        /// Order canceled event
        /// </summary>
        OrderCanceled,
        /// <summary>
        /// Order fee refunded event
        /// </summary>
        OrderFeeRefunded,
        /// <summary>
        /// Margin transfer event
        /// </summary>
        MarginTransfer,
        /// <summary>
        /// Margin loan event
        /// </summary>
        MarginLoan,
        /// <summary>
        /// Margin interest event
        /// </summary>
        MarginInterest,
        /// <summary>
        /// Margin repay event
        /// </summary>
        MarginRepay,
        /// <summary>
        /// Other event
        /// </summary>
        Other,
        /// <summary>
        /// Deposit event
        /// </summary>
        Deposit,
        /// <summary>
        /// Withdraw event
        /// </summary>
        Withdraw
    }
}
