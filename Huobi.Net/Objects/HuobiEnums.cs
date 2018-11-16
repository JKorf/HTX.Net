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
}
