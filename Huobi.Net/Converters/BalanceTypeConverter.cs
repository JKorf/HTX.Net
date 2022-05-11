using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class BalanceTypeConverter : BaseConverter<BalanceType>
    {
        public BalanceTypeConverter() : this(true) { }
        public BalanceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BalanceType, string>> Mapping => new List<KeyValuePair<BalanceType, string>>
        {
            new KeyValuePair<BalanceType, string>(BalanceType.Frozen, "frozen"),
            new KeyValuePair<BalanceType, string>(BalanceType.Trade, "trade"),
            new KeyValuePair<BalanceType, string>(BalanceType.Loan, "loan"),
            new KeyValuePair<BalanceType, string>(BalanceType.Interest, "interest"),
            new KeyValuePair<BalanceType, string>(BalanceType.TransferOutAvailable, "transfer-out-available"),
            new KeyValuePair<BalanceType, string>(BalanceType.LoanAvailable, "loan-available"),
        };
    }
}
