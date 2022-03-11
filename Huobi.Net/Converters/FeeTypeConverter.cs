using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class FeeTypeConverter : BaseConverter<FeeType>
    {
        public FeeTypeConverter() : this(true) { }
        public FeeTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FeeType, string>> Mapping => new List<KeyValuePair<FeeType, string>>
        {
            new KeyValuePair<FeeType, string>(FeeType.Fixed, "fixed"),
            new KeyValuePair<FeeType, string>(FeeType.Circulated, "circulated"),
            new KeyValuePair<FeeType, string>(FeeType.Ratio, "ratio")
            
        };
    }
}