using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;

namespace Huobi.Net.Converters
{
    public class TransferTypeConverter : BaseConverter<HuobiTransferType>
    {
        public TransferTypeConverter() : this(true) { }
        public TransferTypeConverter(bool useQuotes) : base(useQuotes) { }

        protected override Dictionary<HuobiTransferType, string> Mapping => new Dictionary<HuobiTransferType, string>
        {
            { HuobiTransferType.FromSubAccount, "master-transfer-in" },
            { HuobiTransferType.ToSubAccount, "master-transfer-out" },
            { HuobiTransferType.PointFromSubAccount, "master-point-transfer-in" },
            { HuobiTransferType.PointToSubAccount, "master-point-transfer-out" },
        };
    }
}
