using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums.Futures;

namespace Huobi.Net.Converters.Futures
{
    internal class ContractTypeConverter : BaseConverter<ContractType>
    {
        public ContractTypeConverter() : this(true) { }

        public ContractTypeConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<ContractType, string>> Mapping => new List<KeyValuePair<ContractType, string>>
        {
            new KeyValuePair<ContractType, string>(ContractType.ThisWeek, "this_week"),
            new KeyValuePair<ContractType, string>(ContractType.NextWeek, "next_week"),
            new KeyValuePair<ContractType, string>(ContractType.Quarter, "quarter"),
            new KeyValuePair<ContractType, string>(ContractType.NextQuarter, "next_quarter"),
            new KeyValuePair<ContractType, string>(ContractType.Swap, "swap"),
        };
    }
}
