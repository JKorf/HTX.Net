using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Define transfer type
    /// </summary>
    public enum WithdrawDepositType
	{
		/// <summary>
		/// Deposit
		/// </summary>
		[Map("deposit")]
		Deposit,
		/// <summary>
		/// Withdraw
		/// </summary>
		[Map("withdraw")]
        Withdraw
    }
}
