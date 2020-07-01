using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;

namespace Huobi.Net.Converters
{
    internal class TransferTypeConverter : BaseConverter<HuobiTransferType>
    {
        public TransferTypeConverter() : this(true) { }
        public TransferTypeConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<HuobiTransferType, string>> Mapping => new List<KeyValuePair<HuobiTransferType, string>>
        {
            new KeyValuePair<HuobiTransferType, string>(HuobiTransferType.FromSubAccount, "master-transfer-in"),
            new KeyValuePair<HuobiTransferType, string>(HuobiTransferType.ToSubAccount, "master-transfer-out"),
            new KeyValuePair<HuobiTransferType, string>(HuobiTransferType.PointFromSubAccount, "master-point-transfer-in"),
            new KeyValuePair<HuobiTransferType, string>(HuobiTransferType.PointToSubAccount, "master-point-transfer-out")
        };
    }
}
