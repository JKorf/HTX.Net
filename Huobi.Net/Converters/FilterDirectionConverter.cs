using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class FilterDirectionConverter : BaseConverter<HuobiFilterDirection>
    {
        public FilterDirectionConverter() : this(true) { }
        public FilterDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiFilterDirection, string>> Mapping => new List<KeyValuePair<HuobiFilterDirection, string>>
        {
            new KeyValuePair<HuobiFilterDirection, string>(HuobiFilterDirection.Next, "next"),
            new KeyValuePair<HuobiFilterDirection, string>(HuobiFilterDirection.Previous, "prev")
        };
    }
}
