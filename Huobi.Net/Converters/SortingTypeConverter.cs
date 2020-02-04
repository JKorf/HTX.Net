using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;

namespace Huobi.Net.Converters
{
    internal class SortingTypeConverter : BaseConverter<HuobiSortingType>
    {
        public SortingTypeConverter() : this(true) { }

        public SortingTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiSortingType, string>> Mapping => new List<KeyValuePair<HuobiSortingType, string>>
        {
            new KeyValuePair<HuobiSortingType, string>(HuobiSortingType.Ascending, "asc"),
            new KeyValuePair<HuobiSortingType, string>(HuobiSortingType.Descending, "desc")
        };
    }
}