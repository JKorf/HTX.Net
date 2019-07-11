using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class SymbolStateConverter : BaseConverter<HuobiSymbolState>
    {
        public SymbolStateConverter() : this(true) { }
        public SymbolStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiSymbolState, string>> Mapping => new List<KeyValuePair<HuobiSymbolState, string>>
        {
            new KeyValuePair<HuobiSymbolState, string>(HuobiSymbolState.Online, "online"),
            new KeyValuePair<HuobiSymbolState, string>(HuobiSymbolState.Offline, "offline"),
            new KeyValuePair<HuobiSymbolState, string>(HuobiSymbolState.Suspended, "suspend"),
            new KeyValuePair<HuobiSymbolState, string>(HuobiSymbolState.PreOnline, "pre-online")
        };
    }
}
