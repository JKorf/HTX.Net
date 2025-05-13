using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
