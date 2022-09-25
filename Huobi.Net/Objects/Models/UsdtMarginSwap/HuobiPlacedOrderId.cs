using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Placed order id 
    /// </summary>
    public class HuobiPlacedOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("client_order_id")]
        public long? ClientOrderId { get; set; }
    }
}
