using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;

namespace Huobi.Net.Converters
{
    internal class TransactionTypeConverter : BaseConverter<HuobiTransactionType>
    {
        public TransactionTypeConverter() : this(true) { }
        public TransactionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiTransactionType, string>> Mapping => new List<KeyValuePair<HuobiTransactionType, string>>
        {
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Trade, "trade"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Etf, "etf"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.TransactionFee, "transact-fee"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Deduction, "deduction"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Transfer, "transfer"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Credit, "credit"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Liquidation, "liquidation"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Interest, "interest"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.DepositWithdraw, "deposit-withdraw"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.WithdrawFee, "withdraw-fee"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Exchange, "exchange"),
            new KeyValuePair<HuobiTransactionType, string>(HuobiTransactionType.Other, "other-types")
        };
    }
}