namespace Huobi.Net.Objects
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    public class HuobiDepositAddress 
    {
        /// <summary>
        /// Crypto currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// Block chain name
        /// </summary>
        public string Chain { get; set; } = string.Empty;
    }
}
