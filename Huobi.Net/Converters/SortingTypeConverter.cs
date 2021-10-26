using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class SortingTypeConverter : BaseConverter<SortingType>
    {
        public SortingTypeConverter() : this(true) { }

        public SortingTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SortingType, string>> Mapping => new List<KeyValuePair<SortingType, string>>
        {
            new KeyValuePair<SortingType, string>(SortingType.Ascending, "asc"),
            new KeyValuePair<SortingType, string>(SortingType.Descending, "desc")
        };
    }
}