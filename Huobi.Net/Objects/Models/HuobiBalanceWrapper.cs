using CryptoExchange.Net.ExchangeInterfaces;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Balance info
    /// </summary>
    public class HuobiBalanceWrapper: ICommonBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Frozen
        /// </summary>
        public decimal Frozen { get; set; }
        /// <summary>
        /// Trade
        /// </summary>
        public decimal Trade { get; set; }

        string ICommonBalance.CommonAsset => Asset;
        decimal ICommonBalance.CommonAvailable => Trade;
        decimal ICommonBalance.CommonTotal => Frozen + Trade;
    }
}
