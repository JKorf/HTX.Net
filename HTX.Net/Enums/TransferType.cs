using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    public enum TransferType
    {
        /// <summary>
        /// From sub account
        /// </summary>
        [Map("master-transfer-in")]
        FromSubAccount,
        /// <summary>
        /// To sub account
        /// </summary>
        [Map("master-transfer-out")]
        ToSubAccount,
        /// <summary>
        /// Point from sub account
        /// </summary>
        [Map("master-point-transfer-in")]
        PointFromSubAccount,
        /// <summary>
        /// Point to sub account
        /// </summary>
        [Map("master-point-transfer-out")]
        PointToSubAccount
    }
}
