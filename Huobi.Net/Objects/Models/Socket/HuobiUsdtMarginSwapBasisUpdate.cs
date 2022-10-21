using Newtonsoft.Json;
namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Basis update
    /// </summary>
    public class HuobiUsdtMarginSwapBasisUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonProperty("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Contract price
        /// </summary>
        [JsonProperty("contract_price")]
        public decimal ContractPrice { get; set; }
        /// <summary>
        /// Basis
        /// </summary>
        public decimal Basis { get; set; }
        /// <summary>
        /// Basis rate
        /// </summary>
        [JsonProperty("basis_rate")]
        public decimal BasisRate { get; set; }
    }
}
