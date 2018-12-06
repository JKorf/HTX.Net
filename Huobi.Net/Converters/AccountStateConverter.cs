using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class AccountStateConverter : BaseConverter<HuobiAccountState>
    {
        public AccountStateConverter() : this(true) { }
        public AccountStateConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiAccountState, string> Mapping => new Dictionary<HuobiAccountState, string>
        {
            { HuobiAccountState.Locked, "lock" },
            { HuobiAccountState.Working, "working" }
        };
    }
}
