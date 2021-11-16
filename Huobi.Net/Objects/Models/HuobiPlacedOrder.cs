using CryptoExchange.Net.ExchangeInterfaces;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Placed order
    /// </summary>
    public class HuobiPlacedOrder : ICommonOrderId
    {
        /// <summary>
        /// The id
        /// </summary>
        public long Id { get; set; }
        string ICommonOrderId.CommonId => Id.ToString();
    }
}
