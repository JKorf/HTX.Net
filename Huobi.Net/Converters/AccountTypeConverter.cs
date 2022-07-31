using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class AccountTypeConverter : BaseConverter<AccountType>
    {
        public AccountTypeConverter() : this(true) { }
        public AccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountType, string>> Mapping => new List<KeyValuePair<AccountType, string>>
        {
            new KeyValuePair<AccountType, string>(AccountType.Margin, "margin"),
            new KeyValuePair<AccountType, string>(AccountType.SuperMargin, "super-margin"),
            new KeyValuePair<AccountType, string>(AccountType.Investment, "investment"),
            new KeyValuePair<AccountType, string>(AccountType.Borrow, "borrow"),
            new KeyValuePair<AccountType, string>(AccountType.Spot, "spot"),
            new KeyValuePair<AccountType, string>(AccountType.Otc, "otc"),
            new KeyValuePair<AccountType, string>(AccountType.Point, "point"),
            new KeyValuePair<AccountType, string>(AccountType.GridTrading, "grid-trading"),
            new KeyValuePair<AccountType, string>(AccountType.DepositEarning, "deposit-earning"),
            new KeyValuePair<AccountType, string>(AccountType.OtcOptions, "otc-options"),
            new KeyValuePair<AccountType, string>(AccountType.Minepool, "minepool"),
            new KeyValuePair<AccountType, string>(AccountType.Etf, "etf"),
            new KeyValuePair<AccountType, string>(AccountType.CryptoLoans, "crypto-loans"),
        };
    }
}
