using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderStateConverter : BaseConverter<HuobiOrderState>
    {
        public OrderStateConverter() : this(true) { }
        public OrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiOrderState, string>> Mapping => new List<KeyValuePair<HuobiOrderState, string>>
        {
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.PreSubmitted, "pre-submitted"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.Submitted, "submitted"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.PartiallyFilled, "partial-filled"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.PartiallyCanceled, "partial-canceled"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.Filled, "filled"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.Canceled, "canceled"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.Rejected, "rejected"),
            new KeyValuePair<HuobiOrderState, string>(HuobiOrderState.Created, "created")
        };
    }
}
