namespace Huobi.Net.Objects
{
    /// <summary>
    /// Interval for klines
    /// </summary>
    public enum HuobiPeriod
    {
        /// <summary>
        /// 1m
        /// </summary>
        OneMinute,
        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes,
        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes,
        /// <summary>
        /// 30m
        /// </summary>
        ThirtyMinutes,
        /// <summary>
        /// 1h
        /// </summary>
        OneHour,
        /// <summary>
        /// 4h
        /// </summary>
        FourHours,
        /// <summary>
        /// 1d
        /// </summary>
        OneDay,
        /// <summary>
        /// 1w
        /// </summary>
        OneWeek,
        /// <summary>
        /// 1m
        /// </summary>
        OneMonth,
        /// <summary>
        /// 1y
        /// </summary>
        OneYear
    }

    /// <summary>
    /// Filter direction
    /// </summary>
    public enum HuobiFilterDirection
    {
        /// <summary>
        /// Get results after
        /// </summary>
        Next,
        /// <summary>
        /// Get results before
        /// </summary>
        Previous
    }

    /// <summary>
    /// Order side
    /// </summary>
    public enum HuobiOrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        Sell
    }

    /// <summary>
    /// Account state
    /// </summary>
    public enum HuobiAccountState
    {
        /// <summary>
        /// Working
        /// </summary>
        Working,
        /// <summary>
        /// Locked
        /// </summary>
        Locked
    }

    /// <summary>
    /// Account type
    /// </summary>
    public enum HuobiAccountType
    {
        /// <summary>
        /// Spot account
        /// </summary>
        Spot,
        /// <summary>
        /// Margin account
        /// </summary>
        Margin,
        /// <summary>
        /// Otc account
        /// </summary>
        Otc,
        /// <summary>
        /// Point account
        /// </summary>
        Point
    }

    /// <summary>
    /// Balance type
    /// </summary>
    public enum HuobiBalanceType
    {
        /// <summary>
        /// Trade balance
        /// </summary>
        Trade,
        /// <summary>
        /// Frozen balance
        /// </summary>
        Frozen,
        /// <summary>
        /// Loan balance
        /// </summary>
        Loan,
        /// <summary>
        /// Interest balance
        /// </summary>
        Interest
    }

    /// <summary>
    /// Order role
    /// </summary>
    public enum HuobiOrderRole
    {
        /// <summary>
        /// Maker of an order book entry
        /// </summary>
        Maker,
        /// <summary>
        /// Taker of an order book entry
        /// </summary>
        Taker
    }

    /// <summary>
    /// Order type
    /// </summary>
    public enum HuobiOrderType
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
        StopLimitSell
    }

    /// <summary>
    /// Order state
    /// </summary>
    public enum HuobiOrderState
    {
        /// <summary>
        /// Pre-submitted
        /// </summary>
        PreSubmitted,
        /// <summary>
        /// Submitted, nothing filled yet
        /// </summary>
        Submitted,
        /// <summary>
        /// Partially filled
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// Partially filled, then cancelled
        /// </summary>
        PartiallyCanceled,
        /// <summary>
        /// Filled completely
        /// </summary>
        Filled,
        /// <summary>
        /// Cancelled without fill
        /// </summary>
        Canceled,
        /// <summary>
        /// Created
        /// </summary>
        Created,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected
    }

    /// <summary>
    /// Event type
    /// </summary>
    public enum HuobiAccountEventType
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
        /// Order cancelled event
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

    /// <summary>
    /// Transfer type
    /// </summary>
    public enum HuobiTransferType
    {
        /// <summary>
        /// From sub account
        /// </summary>
        FromSubAccount,
        /// <summary>
        /// To sub account
        /// </summary>
        ToSubAccount,
        /// <summary>
        /// Point from sub account
        /// </summary>
        PointFromSubAccount,
        /// <summary>
        /// Point to sub account
        /// </summary>
        PointToSubAccount
    }

    /// <summary>
    /// Symbol state
    /// </summary>
    public enum HuobiSymbolState
    {
        /// <summary>
        /// Not yet online
        /// </summary>
        PreOnline,
        /// <summary>
        /// Online
        /// </summary>
        Online,
        /// <summary>
        /// Offline
        /// </summary>
        Offline,
        /// <summary>
        /// Suspended
        /// </summary>
        Suspended
    }

    /// <summary>
    /// Transaction type
    /// </summary>
    public enum HuobiTransactionType
    {
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// ETF
        /// </summary>
        Etf,
        /// <summary>
        /// Transaction fee
        /// </summary>
        TransactionFee,
        /// <summary>
        /// Deduction
        /// </summary>
        Deduction,
        /// <summary>
        /// Transfer between accounts
        /// </summary>
        Transfer,
        /// <summary>
        /// Credit
        /// </summary>
        Credit,
        /// <summary>
        /// Liquidation
        /// </summary>
        Liquidation,
        /// <summary>
        /// Interest
        /// </summary>
        Interest,
        /// <summary>
        /// Deposit
        /// </summary>
        Deposit,
        /// <summary>
        /// Withdraw
        /// </summary>
        Withdraw,
        /// <summary>
        /// Withdraw fee
        /// </summary>
        WithdrawFee,
        /// <summary>
        /// Exchange
        /// </summary>
        Exchange,
        /// <summary>
        /// Other types
        /// </summary>
        Other,
        /// <summary>
        /// Rebate
        /// </summary>
        Rebate
    }

    /// <summary>
    /// Sorting order
    /// </summary>
    public enum HuobiSortingType
    {
        /// <summary>
        /// Ascending
        /// </summary>
        Ascending,
        /// <summary>
        /// Descending
        /// </summary>
        Descending
    }
}
