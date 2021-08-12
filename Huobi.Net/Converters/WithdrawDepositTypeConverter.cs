using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class WithdrawDepositTypeConverter : BaseConverter<WithdrawDepositType>
    {
        public WithdrawDepositTypeConverter() : this(true) { }
        public WithdrawDepositTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WithdrawDepositType, string>> Mapping => new List<KeyValuePair<WithdrawDepositType, string>>
        {
            new KeyValuePair<WithdrawDepositType, string>(WithdrawDepositType.Deposit, "deposit"),
            new KeyValuePair<WithdrawDepositType, string>(WithdrawDepositType.Withdraw, "withdraw"),
        };
    }
}
