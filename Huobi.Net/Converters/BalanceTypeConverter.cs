using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class BalanceTypeConverter : BaseConverter<HuobiBalanceType>
    {
        public BalanceTypeConverter() : this(true) { }
        public BalanceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiBalanceType, string>> Mapping => new List<KeyValuePair<HuobiBalanceType, string>>
        {
            new KeyValuePair<HuobiBalanceType, string>(HuobiBalanceType.Frozen, "frozen"),
            new KeyValuePair<HuobiBalanceType, string>(HuobiBalanceType.Trade, "trade")
        };
    }
}
