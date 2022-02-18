using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderStateConverter : BaseConverter<OrderState>
    {
        public OrderStateConverter() : this(true) { }
        public OrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderState, string>> Mapping => new List<KeyValuePair<OrderState, string>>
        {
            new KeyValuePair<OrderState, string>(OrderState.PreSubmitted, "pre-submitted"),
            new KeyValuePair<OrderState, string>(OrderState.Submitted, "submitted"),
            new KeyValuePair<OrderState, string>(OrderState.PartiallyFilled, "partial-filled"),
            new KeyValuePair<OrderState, string>(OrderState.PartiallyCanceled, "partial-canceled"),
            new KeyValuePair<OrderState, string>(OrderState.Filled, "filled"),
            new KeyValuePair<OrderState, string>(OrderState.Canceled, "canceled"),
            new KeyValuePair<OrderState, string>(OrderState.Rejected, "rejected"),
            new KeyValuePair<OrderState, string>(OrderState.Created, "created")
        };
    }
}
