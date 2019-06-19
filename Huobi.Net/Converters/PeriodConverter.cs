using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class PeriodConverter : BaseConverter<HuobiPeriod>
    {
        public PeriodConverter() : this(true) { }
        public PeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiPeriod, string>> Mapping => new List<KeyValuePair<HuobiPeriod, string>>
        {
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneMinute, "1min"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.FiveMinutes, "5min"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.FifteenMinutes, "15min"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.ThirtyMinutes, "30min"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneHour, "60min"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.FourHours, "4hour"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneDay, "1day"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneWeek, "1week"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneMonth, "1mon"),
            new KeyValuePair<HuobiPeriod, string>(HuobiPeriod.OneYear, "1year")
        };
    }
}
