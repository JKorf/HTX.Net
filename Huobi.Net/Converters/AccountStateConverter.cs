using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountStateConverter : BaseConverter<AccountState>
    {
        public AccountStateConverter() : this(true) { }
        public AccountStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountState, string>> Mapping => new List<KeyValuePair<AccountState, string>>
        {
            new KeyValuePair<AccountState, string>(AccountState.Locked, "lock"),
            new KeyValuePair<AccountState, string>(AccountState.Working, "working")
        };
    }
}
