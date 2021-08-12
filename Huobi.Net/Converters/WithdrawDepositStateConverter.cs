using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class WithdrawDepositStateConverter : BaseConverter<WithdrawDepositState>
    {
        public WithdrawDepositStateConverter() : this(true) { }
        public WithdrawDepositStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WithdrawDepositState, string>> Mapping => new List<KeyValuePair<WithdrawDepositState, string>>
        {
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Verifying, "verifying"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Failed, "failed"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Submitted, "submitted"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Reexamine, "reexamine"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Canceled, "canceled"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Pass, "pass"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Reject, "reject"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.PreTransfer, "pre-transfer"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.WalletTransfer, "wallet-transfer"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.WalletReject, "wallet-reject"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Confirmed, "confirmed"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.ConfirmError, "confirm-error"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Repealed, "repealed"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Unknown, "unknown"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Confirming, "confirming"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Safe, "safe"),
            new KeyValuePair<WithdrawDepositState, string>(WithdrawDepositState.Orphan, "orphan"),
        };
    }
}
