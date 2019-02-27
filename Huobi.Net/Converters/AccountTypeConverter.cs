using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class AccountTypeConverter : BaseConverter<HuobiAccountType>
    {
        public AccountTypeConverter() : this(true) { }
        public AccountTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiAccountType, string> Mapping => new Dictionary<HuobiAccountType, string>
        {
            { HuobiAccountType.Margin, "margin" },
            { HuobiAccountType.Spot, "spot" },
            { HuobiAccountType.Otc, "otc" },
            { HuobiAccountType.Point, "point" }
        };
    }
}
