using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiBalance
    {
        public string Currency { get; set; }
        [JsonConverter(typeof(BalanceTypeConverter))]
        public HuobiBalanceType Type { get; set; }
        public decimal Balance { get; set; }
    }
}
