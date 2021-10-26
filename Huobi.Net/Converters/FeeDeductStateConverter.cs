using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class FeeDeductStateConverter : BaseConverter<FeeDeductState>
    {
        public FeeDeductStateConverter() : this(true) { }
        public FeeDeductStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FeeDeductState, string>> Mapping => new List<KeyValuePair<FeeDeductState, string>>
        {
            new KeyValuePair<FeeDeductState, string>(FeeDeductState.Ongoing, "ongoing"),
            new KeyValuePair<FeeDeductState, string>(FeeDeductState.Done, "done")
        };
    }
}
