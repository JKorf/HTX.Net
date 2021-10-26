using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderRoleConverter : BaseConverter<OrderRole>
    {
        public OrderRoleConverter() : this(true) { }
        public OrderRoleConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderRole, string>> Mapping => new List<KeyValuePair<OrderRole, string>>
        {
            new KeyValuePair<OrderRole, string>(OrderRole.Maker, "maker"),
            new KeyValuePair<OrderRole, string>(OrderRole.Taker, "taker")
        };
    }
}
