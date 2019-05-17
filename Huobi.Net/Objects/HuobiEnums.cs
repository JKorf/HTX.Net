namespace Huobi.Net.Objects
{
    public enum HuobiPeriod
    {
        OneMinute,
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinutes,
        OneHour,
        FourHours,
        OneDay,
        OneWeek,
        OneMonth,
        OneYear
    }

    public enum HuobiFilterDirection
    {
        Next,
        Previous
    }

    public enum HuobiOrderSide
    {
        Buy,
        Sell
    }

    public enum HuobiAccountState
    {
        Working,
        Locked
    }

    public enum HuobiAccountType
    {
        Spot,
        Margin,
        Otc,
        Point
    }

    public enum HuobiBalanceType
    {
        Trade,
        Frozen
    }

    public enum HuobiOrderRole
    {
        Maker,
        Taker
    }

    public enum HuobiOrderType
    {
        LimitBuy,
        LimitSell,
        MarketBuy,
        MarketSell,
        IOCBuy,
        IOCSell,
        LimitMakerBuy,
        LimitMakerSell
    }

    public enum HuobiOrderState
    {
        PreSubmitted,
        Submitted,
        PartiallyFilled,
        PartiallyCanceled,
        Filled,
        Canceled
    }

    public enum HuobiAccountEventType
    {
        OrderPlaced,
        OrderMatched,
        OrderRefunded,
        OrderCanceled,
        OrderFeeRefunded,
        MarginTransfer,
        MarginLoan,
        MarginInterest,
        MarginRepay,
        Other
    }

    public enum HuobiTransferType
    {
        FromSubAccount,
        ToSubAccount,
        PointFromSubAccount,
        PointToSubAccount
    }
}
