using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountEventConverter : BaseConverter<AccountEventType>
    {
        public AccountEventConverter() : this(true) { }
        public AccountEventConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountEventType, string>> Mapping => new List<KeyValuePair<AccountEventType, string>>
        {
            new KeyValuePair<AccountEventType, string>(AccountEventType.OrderPlaced, "order.place"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.OrderMatched, "order.match"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.OrderCanceled, "order.cancel"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.OrderRefunded, "order.refund"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.OrderFeeRefunded, "order.fee-refund"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.MarginInterest, "margin.interest"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.MarginLoan, "margin.loan"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.MarginRepay, "margin.repay"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.MarginTransfer, "margin.transfer"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.Other, "other"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.Deposit, "deposit"),
            new KeyValuePair<AccountEventType, string>(AccountEventType.Withdraw, "withdraw")
        };
    }
}
