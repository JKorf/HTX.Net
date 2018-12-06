using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class PeriodConverter : BaseConverter<HuobiPeriod>
    {
        public PeriodConverter() : this(true) { }
        public PeriodConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiPeriod, string> Mapping => new Dictionary<HuobiPeriod, string>
        {
            { HuobiPeriod.OneMinute, "1min" },
            { HuobiPeriod.FiveMinutes, "5min" },
            { HuobiPeriod.FiveteenMinutes, "15min" },
            { HuobiPeriod.ThirtyMinutes, "30min" },
            { HuobiPeriod.OneHour, "60min" },
            { HuobiPeriod.OneDay, "1day" },
            { HuobiPeriod.OneWeek, "1week" },
            { HuobiPeriod.OneMonth, "1mon" },
            { HuobiPeriod.OneYear, "1year" }
        };
    }
}
