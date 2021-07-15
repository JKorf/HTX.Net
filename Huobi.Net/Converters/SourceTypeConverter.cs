using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class SourceTypeConverter : BaseConverter<SourceType>
    {
        public SourceTypeConverter() : this(true) { }
        public SourceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SourceType, string>> Mapping => new List<KeyValuePair<SourceType, string>>
        {
            new KeyValuePair<SourceType, string>(SourceType.Spot, "spot-api"),
            new KeyValuePair<SourceType, string>(SourceType.IsolatedMargin, "margin-api"),
            new KeyValuePair<SourceType, string>(SourceType.CrossMargin, "super-margin-api"),
            new KeyValuePair<SourceType, string>(SourceType.C2CMargin, "c2c-margin-api"),
        };
    }
}
