using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class TransferTypeConverter : BaseConverter<TransferType>
    {
        public TransferTypeConverter() : this(true) { }
        public TransferTypeConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<TransferType, string>> Mapping => new List<KeyValuePair<TransferType, string>>
        {
            new KeyValuePair<TransferType, string>(TransferType.FromSubAccount, "master-transfer-in"),
            new KeyValuePair<TransferType, string>(TransferType.ToSubAccount, "master-transfer-out"),
            new KeyValuePair<TransferType, string>(TransferType.PointFromSubAccount, "master-point-transfer-in"),
            new KeyValuePair<TransferType, string>(TransferType.PointToSubAccount, "master-point-transfer-out")
        };
    }
}
