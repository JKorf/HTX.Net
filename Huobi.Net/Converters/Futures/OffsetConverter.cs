using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums.Futures;

namespace Huobi.Net.Converters.Futures
{
    internal class OffsetConverter : BaseConverter<Offset>
    {
        public OffsetConverter() : this(true) { }

        public OffsetConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<Offset, string>> Mapping => new List<KeyValuePair<Offset, string>>
        {
            new KeyValuePair<Offset, string>(Offset.Open, "open"),
            new KeyValuePair<Offset, string>(Offset.Close, "close")
        };
    }
}
