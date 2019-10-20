using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderRoleConverter : BaseConverter<HuobiOrderRole>
    {
        public OrderRoleConverter() : this(true) { }
        public OrderRoleConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiOrderRole, string>> Mapping => new List<KeyValuePair<HuobiOrderRole, string>>
        {
            new KeyValuePair<HuobiOrderRole, string>(HuobiOrderRole.Maker, "maker"),
            new KeyValuePair<HuobiOrderRole, string>(HuobiOrderRole.Taker, "taker")
        };
    }
}
