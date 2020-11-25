using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountEventConverter : BaseConverter<HuobiAccountEventType>
    {
        public AccountEventConverter() : this(true) { }
        public AccountEventConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiAccountEventType, string>> Mapping => new List<KeyValuePair<HuobiAccountEventType, string>>
        {
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.OrderPlaced, "order.place"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.OrderMatched, "order.match"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.OrderCanceled, "order.cancel"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.OrderRefunded, "order.refund"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.OrderFeeRefunded, "order.fee-refund"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.MarginInterest, "margin.interest"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.MarginLoan, "margin.loan"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.MarginRepay, "margin.repay"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.MarginTransfer, "margin.transfer"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.Other, "other"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.Deposit, "deposit"),
            new KeyValuePair<HuobiAccountEventType, string>(HuobiAccountEventType.Withdraw, "withdraw")
        };
    }
}
