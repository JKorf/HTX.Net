using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class FilterDirectionConverter : BaseConverter<FilterDirection>
    {
        public FilterDirectionConverter() : this(true) { }
        public FilterDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FilterDirection, string>> Mapping => new List<KeyValuePair<FilterDirection, string>>
        {
            new KeyValuePair<FilterDirection, string>(FilterDirection.Next, "next"),
            new KeyValuePair<FilterDirection, string>(FilterDirection.Previous, "prev")
        };
    }
}
