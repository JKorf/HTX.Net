using Huobi.Net.Converters;
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
        /// <summary>
        /// The state of the symbol
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(SymbolStateConverter))]
        public HuobiSymbolState State { get; set; }
        /// <summary>
        /// Minimum value of the amount
        /// </summary>
        [JsonProperty("min-order-amt")]
        public decimal MinOrderAmount { get; set; }
        /// <summary>
        /// Maximum value of the amount
        /// </summary>
        [JsonProperty("max-order-amt")]
        public decimal MaxOrderAmount { get; set; }
        /// <summary>
        /// Minimum value of the order amount in quote currency
        /// </summary>
        [JsonProperty("min-order-value")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// The precision of the order amount in quote currency
        /// </summary>
        [JsonProperty("value-precision")]
        public int ValuePrecision { get; set; }
    }
}
