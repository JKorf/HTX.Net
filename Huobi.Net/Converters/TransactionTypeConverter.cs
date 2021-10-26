using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class TransactionTypeConverter : BaseConverter<TransactionType>
    {
        public TransactionTypeConverter() : this(true) { }
        public TransactionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<TransactionType, string>> Mapping => new List<KeyValuePair<TransactionType, string>>
        {
            new KeyValuePair<TransactionType, string>(TransactionType.Trade, "trade"),
            new KeyValuePair<TransactionType, string>(TransactionType.Etf, "etf"),
            new KeyValuePair<TransactionType, string>(TransactionType.TransactionFee, "transact-fee"),
            new KeyValuePair<TransactionType, string>(TransactionType.Deduction, "fee-deduction"),
            new KeyValuePair<TransactionType, string>(TransactionType.Transfer, "transfer"),
            new KeyValuePair<TransactionType, string>(TransactionType.Credit, "credit"),
            new KeyValuePair<TransactionType, string>(TransactionType.Liquidation, "liquidation"),
            new KeyValuePair<TransactionType, string>(TransactionType.Interest, "interest"),
            new KeyValuePair<TransactionType, string>(TransactionType.Deposit, "deposit"),
            new KeyValuePair<TransactionType, string>(TransactionType.Withdraw, "withdraw"),
            new KeyValuePair<TransactionType, string>(TransactionType.WithdrawFee, "withdraw-fee"),
            new KeyValuePair<TransactionType, string>(TransactionType.Exchange, "exchange"),
            new KeyValuePair<TransactionType, string>(TransactionType.Other, "other-types"),
            new KeyValuePair<TransactionType, string>(TransactionType.Rebate, "rebate")
        };
    }
}