using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Basis data
    /// </summary>
    public class HuobiBasisData
    {
        /// <summary>
        /// Basis
        /// </summary>
        public decimal Basis { get; set; }
        /// <summary>
        /// Basis rate
        /// </summary>
        [JsonProperty("basis_rate")]
        public decimal BasisRate { get; set; }
        /// <summary>
        /// Contract price
        /// </summary>
        [JsonProperty("contract_price")]
        public decimal ContractPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonProperty("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Unique id
        /// </summary>
        public long Id { get; set; }
    }
}
