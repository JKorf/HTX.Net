using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class AccountActivationConverter : BaseConverter<AccountActivation>
    {
        public AccountActivationConverter() : this(true) { }
        public AccountActivationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountActivation, string>> Mapping => new List<KeyValuePair<AccountActivation, string>>
        {
            new KeyValuePair<AccountActivation, string>(AccountActivation.Activated, "activated"),
            new KeyValuePair<AccountActivation, string>(AccountActivation.Deactivated, "deactivated")
        };
    }
}
