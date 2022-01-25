using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums.Futures;

namespace Huobi.Net.Converters.Futures
{
    internal class ContractStatusConverter : BaseConverter<ContractStatus>
    {
        public ContractStatusConverter() : this(true) { }

        public ContractStatusConverter(bool useQuotes) : base(useQuotes) { }

        protected override List<KeyValuePair<ContractStatus, string>> Mapping => new List<KeyValuePair<ContractStatus, string>>
        {
            new KeyValuePair<ContractStatus, string>(ContractStatus.Delisting, "0"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.Listing, "1"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.PendingListing, "2"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.Suspension, "3"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.SuspendingOfListing, "4"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.InSettlement, "5"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.Delivering, "6"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.SettlementCompleted, "7"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.Delivered, "8"),
            new KeyValuePair<ContractStatus, string>(ContractStatus.SuspendingOfTrade, "9")
        };
    }
}
