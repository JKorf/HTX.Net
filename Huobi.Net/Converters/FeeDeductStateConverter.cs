using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class FeeDeductStateConverter : BaseConverter<HuobiFeeDeductState>
    {
        public FeeDeductStateConverter() : this(true) { }
        public FeeDeductStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiFeeDeductState, string>> Mapping => new List<KeyValuePair<HuobiFeeDeductState, string>>
        {
            new KeyValuePair<HuobiFeeDeductState, string>(HuobiFeeDeductState.Ongoing, "ongoing"),
            new KeyValuePair<HuobiFeeDeductState, string>(HuobiFeeDeductState.Done, "done")
        };
    }
}
