using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountStateConverter : BaseConverter<HuobiAccountState>
    {
        public AccountStateConverter() : this(true) { }
        public AccountStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiAccountState, string>> Mapping => new List<KeyValuePair<HuobiAccountState, string>>
        {
            new KeyValuePair<HuobiAccountState, string>(HuobiAccountState.Locked, "lock"),
            new KeyValuePair<HuobiAccountState, string>(HuobiAccountState.Working, "working")
        };
    }
}
