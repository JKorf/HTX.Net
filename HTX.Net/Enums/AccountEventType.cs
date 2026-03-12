using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Event type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountEventType>))]
    public enum AccountEventType
    {
        /// <summary>
        /// ["<c>order.place</c>"] Order placed event
        /// </summary>
        [Map("order.place")]
        OrderPlaced,
        /// <summary>
        /// ["<c>order.match</c>"] Order matched event
        /// </summary>
        [Map("order.match")]
        OrderMatched,
        /// <summary>
        /// ["<c>order.refund</c>"] Order refunded event
        /// </summary>
        [Map("order.refund")]
        OrderRefunded,
        /// <summary>
        /// ["<c>order.cancel</c>"] Order canceled event
        /// </summary>
        [Map("order.cancel")]
        OrderCanceled,
        /// <summary>
        /// ["<c>order.fee-refund</c>"] Order fee refunded event
        /// </summary>
        [Map("order.fee-refund")]
        OrderFeeRefunded,
        /// <summary>
        /// ["<c>margin.transfer</c>"] Margin transfer event
        /// </summary>
        [Map("margin.transfer")]
        MarginTransfer,
        /// <summary>
        /// ["<c>margin.loan</c>"] Margin loan event
        /// </summary>
        [Map("margin.loan")]
        MarginLoan,
        /// <summary>
        /// ["<c>margin.interest</c>"] Margin interest event
        /// </summary>
        [Map("margin.interest")]
        MarginInterest,
        /// <summary>
        /// ["<c>margin.repay</c>"] Margin repay event
        /// </summary>
        [Map("margin.repay")]
        MarginRepay,
        /// <summary>
        /// ["<c>other</c>"] Other event
        /// </summary>
        [Map("other")]
        Other,
        /// <summary>
        /// ["<c>deposit</c>"] Deposit event
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdraw</c>"] Withdraw event
        /// </summary>
        [Map("withdraw")]
        Withdraw
    }
}
