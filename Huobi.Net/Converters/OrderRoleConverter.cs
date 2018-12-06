using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class OrderRoleConverter : BaseConverter<HuobiOrderRole>
    {
        public OrderRoleConverter() : this(true) { }
        public OrderRoleConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiOrderRole, string> Mapping => new Dictionary<HuobiOrderRole, string>
        {
            { HuobiOrderRole.Maker, "maker" },
            { HuobiOrderRole.Taker, "taker" }
        };
    }
}
