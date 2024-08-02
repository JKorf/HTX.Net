using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Event type
    /// </summary>
    public enum AccountEventType
    {
        /// <summary>
        /// Order placed event
        /// </summary>
        [Map("order.place")]
        OrderPlaced,
        /// <summary>
        /// Order matched event
        /// </summary>
        [Map("order.match")]
        OrderMatched,
        /// <summary>
        /// Order refunded event
        /// </summary>
        [Map("order.refund")]
        OrderRefunded,
        /// <summary>
        /// Order canceled event
        /// </summary>
        [Map("order.cancel")]
        OrderCanceled,
        /// <summary>
        /// Order fee refunded event
        /// </summary>
        [Map("order.fee-refund")]
        OrderFeeRefunded,
        /// <summary>
        /// Margin transfer event
        /// </summary>
        [Map("margin.transfer")]
        MarginTransfer,
        /// <summary>
        /// Margin loan event
        /// </summary>
        [Map("margin.loan")]
        MarginLoan,
        /// <summary>
        /// Margin interest event
        /// </summary>
        [Map("margin.interest")]
        MarginInterest,
        /// <summary>
        /// Margin repay event
        /// </summary>
        [Map("margin.repay")]
        MarginRepay,
        /// <summary>
        /// Other event
        /// </summary>
        [Map("other")]
        Other,
        /// <summary>
        /// Deposit event
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// Withdraw event
        /// </summary>
        [Map("withdraw")]
        Withdraw
    }
}
