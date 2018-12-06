using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class AccountEventConverter : BaseConverter<HuobiAccountEventType>
    {
        public AccountEventConverter() : this(true) { }
        public AccountEventConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiAccountEventType, string> Mapping => new Dictionary<HuobiAccountEventType, string>
        {
            { HuobiAccountEventType.OrderPlaced, "order.place" },
            { HuobiAccountEventType.OrderMatched, "order.match" },
            { HuobiAccountEventType.OrderCanceled, "order.cancel" },
            { HuobiAccountEventType.OrderRefunded, "order.refund" },
            { HuobiAccountEventType.OrderFeeRefunded, "order.fee-refund" },
            { HuobiAccountEventType.MarginInterest, "margin.interest" },
            { HuobiAccountEventType.MarginLoan, "margin.loan" },
            { HuobiAccountEventType.MarginRepay, "margin.repay" },
            { HuobiAccountEventType.MarginTransfer, "margin.transfer" },
            { HuobiAccountEventType.Other, "other" }
        };
    }
}
