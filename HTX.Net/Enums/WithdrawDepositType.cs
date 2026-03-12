using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Define transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawDepositType>))]
    public enum WithdrawDepositType
	{
		/// <summary>
		/// ["<c>deposit</c>"] Deposit
		/// </summary>
		[Map("deposit")]
		Deposit,
		/// <summary>
		/// ["<c>withdraw</c>"] Withdraw
		/// </summary>
		[Map("withdraw")]
        Withdraw
    }
}
