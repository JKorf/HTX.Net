using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Market data
    /// </summary>
    public class HuobiMarketData: HuobiSymbolData
    {
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HuobiOrderBookEntry? Ask { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HuobiOrderBookEntry? Bid { get; set; }
    }
}
