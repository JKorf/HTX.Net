using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class BalanceTypeConverter : BaseConverter<HuobiBalanceType>
    {
        public BalanceTypeConverter() : this(true) { }
        public BalanceTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiBalanceType, string> Mapping => new Dictionary<HuobiBalanceType, string>
        {
            { HuobiBalanceType.Frozen, "frozen" },
            { HuobiBalanceType.Trade, "trade" }
        };
    }
}
