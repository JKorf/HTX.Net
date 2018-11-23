using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiSymbol
    {
        /// <summary>
        /// The symbol name
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The base currency
        /// </summary>
        [JsonProperty("base-currency")]
        public string BaseCurrency { get; set; }
        /// <summary>
        /// The quote currency
        /// </summary>
        [JsonProperty("quote-currency")]
        public string QuoteCurrency { get; set; }
        /// <summary>
        /// The precision of the price in decimal numbers
        /// </summary>
        [JsonProperty("price-precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The precision of the amount in decimal numbers
        /// </summary>
        [JsonProperty("amount-precision")]
        public int AmountPrecision { get; set; }
        [JsonProperty("symbol-partition")]
        public string SymbolPartition { get; set; }
    }
}
