using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class SymbolStateConverter : BaseConverter<SymbolState>
    {
        public SymbolStateConverter() : this(true) { }
        public SymbolStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SymbolState, string>> Mapping => new List<KeyValuePair<SymbolState, string>>
        {
            new KeyValuePair<SymbolState, string>(SymbolState.Online, "online"),
            new KeyValuePair<SymbolState, string>(SymbolState.Offline, "offline"),
            new KeyValuePair<SymbolState, string>(SymbolState.Suspended, "suspend"),
            new KeyValuePair<SymbolState, string>(SymbolState.PreOnline, "pre-online")
        };
    }
}
