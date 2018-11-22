using System;

namespace Huobi.Net.Objects
{
    public enum HuobiPeriod
    {
        OneMinute,
        FiveMinutes,
        FiveteenMinutes,
        ThirtyMinutes,
        OneHour,
        OneDay,
        OneWeek,
        OneMonth,
        OneYear
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
        Margin
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
}
