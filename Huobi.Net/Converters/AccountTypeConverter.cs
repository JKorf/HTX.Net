using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountTypeConverter : BaseConverter<HuobiAccountType>
    {
        public AccountTypeConverter() : this(true) { }
        public AccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiAccountType, string>> Mapping => new List<KeyValuePair<HuobiAccountType, string>>
        {
            new KeyValuePair<HuobiAccountType, string>(HuobiAccountType.Margin, "margin"),
            new KeyValuePair<HuobiAccountType, string>(HuobiAccountType.Spot, "spot"),
            new KeyValuePair<HuobiAccountType, string>(HuobiAccountType.Otc, "otc"),
            new KeyValuePair<HuobiAccountType, string>(HuobiAccountType.Point, "point")
        };
    }
}
