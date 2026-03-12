using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferType>))]
    public enum TransferType
    {
        /// <summary>
        /// ["<c>master-transfer-in</c>"] From sub account
        /// </summary>
        [Map("master-transfer-in")]
        FromSubAccount,
        /// <summary>
        /// ["<c>master-transfer-out</c>"] To sub account
        /// </summary>
        [Map("master-transfer-out")]
        ToSubAccount,
        /// <summary>
        /// ["<c>master-point-transfer-in</c>"] Point from sub account
        /// </summary>
        [Map("master-point-transfer-in")]
        PointFromSubAccount,
        /// <summary>
        /// ["<c>master-point-transfer-out</c>"] Point to sub account
        /// </summary>
        [Map("master-point-transfer-out")]
        PointToSubAccount
    }
}
