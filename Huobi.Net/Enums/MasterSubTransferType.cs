using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Transfer type between master and sub account
    /// </summary>
    public enum MasterSubTransferType
    {
        /// <summary>
        /// Transfer from master to sub
        /// </summary>
        [Map("34")]
        MasterToSub,
        /// <summary>
        /// Transfer from sub to master
        /// </summary>
        [Map("35")]
        SubToMaster
    }
}
