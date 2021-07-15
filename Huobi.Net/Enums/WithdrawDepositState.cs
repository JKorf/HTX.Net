namespace Huobi.Net.Enums
{
    /// <summary>
    /// The state of a transfer 
    /// </summary>
    public enum WithdrawDepositState
	{
		/// <summary>
		/// Awaiting verification
		/// </summary>
		Verifying,
		/// <summary>
		/// Verification failed
		/// </summary>
		Failed,
		/// <summary>
		/// Withdraw request submitted successfully
		/// </summary>
		Submitted,
		/// <summary>
		/// Under examination for withdraw validation
		/// </summary>
		Reexamine,
		/// <summary>
		/// Withdraw canceled by user
		/// </summary>
		Canceled,
		/// <summary>
		/// Withdraw validation passed
		/// </summary>
		Pass,
		/// <summary>
		/// Withdraw validation rejected
		/// </summary>
		Reject,
		/// <summary>
		/// Withdraw is about to be released
		/// </summary>
		PreTransfer,
		/// <summary>
		/// On-chain transfer initiated
		/// </summary>
		WalletTransfer,
		/// <summary>
		/// Transfer rejected by chain
		/// </summary>
		WalletReject,
		/// <summary>
		/// On-chain transfer completed with one confirmation for withdraw or for at least one block for deposit
		/// </summary>
		Confirmed,
		/// <summary>
		/// On-chain transfer faied to get confirmation
		/// </summary>
		ConfirmError,
		/// <summary>
		/// Withdraw terminated by system
		/// </summary>
		Repealed,
		/// <summary>
		/// On-chain transfer has not been received
		/// </summary>
		Unknown,
		/// <summary>
		/// On-chain transfer waits for first confirmation
		/// </summary>
		Confirming,
		/// <summary>
		/// Multiple on-chain confirmation happened
		/// </summary>
		Safe,
		/// <summary>
		/// Confirmed but currently in an orphan branch
		/// </summary>
		Orphan
	}
}
