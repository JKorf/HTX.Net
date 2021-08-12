using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class OperatorConverter : BaseConverter<Operator>
    {
        public OperatorConverter() : this(true) { }
        public OperatorConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<Operator, string>> Mapping => new List<KeyValuePair<Operator, string>>
        {
            new KeyValuePair<Operator, string>(Operator.LesserThanOrEqual, "lte"),
            new KeyValuePair<Operator, string>(Operator.GreaterThanOrEqual, "gte")
        };
    }
}
