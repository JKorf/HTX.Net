using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class UserStateConverter : BaseConverter<UserState>
    {
        public UserStateConverter() : this(true) { }
        public UserStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<UserState, string>> Mapping => new List<KeyValuePair<UserState, string>>
        {
            new KeyValuePair<UserState, string>(UserState.Locked, "lock"),
            new KeyValuePair<UserState, string>(UserState.Normal, "normal")
        };
    }
}
