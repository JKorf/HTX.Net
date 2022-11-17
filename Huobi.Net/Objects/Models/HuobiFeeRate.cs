namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Current transaction fee rate applied to the user
    /// </summary>
    public class HuobiFeeRate
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Basic fee rate – passive side
        /// </summary>
        public decimal ActualMakerRate { get; set; }
        
        /// <summary>
        /// Basic fee rate – aggressive side
        /// </summary>
        public decimal ActualTakerRate { get; set; }
        
        /// <summary>
        /// Deducted fee rate – passive side
        /// </summary>
        public decimal MakerFeeRate { get; set; }
        
        /// <summary>
        /// Basic fee rate – aggressive side
        /// </summary>
        public decimal TakerFeeRate { get; set; }
    }
}