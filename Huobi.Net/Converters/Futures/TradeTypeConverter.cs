using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums.Futures;

namespace Huobi.Net.Converters.Futures
{
    internal class TradeTypeConverter : BaseConverter<TradeType>
    {
        public TradeTypeConverter() : this(true) { }

        public TradeTypeConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<TradeType, string>> Mapping => new List<KeyValuePair<TradeType, string>>
        {
            new KeyValuePair<TradeType, string>(TradeType.All, "0"),
            new KeyValuePair<TradeType, string>(TradeType.OpenLong, "1"),
            new KeyValuePair<TradeType, string>(TradeType.OpenShort, "2"),
            new KeyValuePair<TradeType, string>(TradeType.CloseShort, "3"),
            new KeyValuePair<TradeType, string>(TradeType.CloseLong, "4"),
            new KeyValuePair<TradeType, string>(TradeType.LiquidateLongPositions, "5"),
            new KeyValuePair<TradeType, string>(TradeType.LiquidateShortPositions, "6")
        };
    }
}
