using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class OrderStateConverter : BaseConverter<HuobiOrderState>
    {
        public OrderStateConverter() : this(true) { }
        public OrderStateConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiOrderState, string> Mapping => new Dictionary<HuobiOrderState, string>
        {
            { HuobiOrderState.PreSubmitted, "pre-submitted" },
            { HuobiOrderState.Submitted, "submitted" },
            { HuobiOrderState.PartiallyFilled, "partial-filled" },
            { HuobiOrderState.PartiallyCanceled, "partial-canceled" },
            { HuobiOrderState.Filled, "filled" },
            { HuobiOrderState.Canceled, "canceled" }
        };
    }
}
