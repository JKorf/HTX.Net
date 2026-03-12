using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Transfer type between master and sub account
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MasterSubTransferType>))]
    public enum MasterSubTransferType
    {
        /// <summary>
        /// ["<c>34</c>"] Transfer from master to sub
        /// </summary>
        [Map("34")]
        MasterToSub,
        /// <summary>
        /// ["<c>35</c>"] Transfer from sub to master
        /// </summary>
        [Map("35")]
        SubToMaster
    }
}
