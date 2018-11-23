using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiBalance
    {
        /// <summary>
        /// The currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The type of the balance
        /// </summary>
        [JsonConverter(typeof(BalanceTypeConverter))]
        public HuobiBalanceType Type { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Balance { get; set; }
    }
}
