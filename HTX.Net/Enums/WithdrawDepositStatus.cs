using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// The status of a transfer 
    /// </summary>
    public enum WithdrawDepositStatus
	{
		/// <summary>
		/// Awaiting verification
		/// </summary>
		[Map("verifying")]
		Verifying,
		/// <summary>
		/// Verification failed
		/// </summary>
		[Map("failed")]
		Failed,
        /// <summary>
        /// Withdraw request submitted successfully
        /// </summary>
		[Map("submitted")]
        Submitted,
        /// <summary>
        /// Under examination for withdraw validation
        /// </summary>
        [Map("reexamine")]
		Reexamine,
        /// <summary>
        /// Withdraw canceled by user
        /// </summary>
        [Map("canceled")]
		Canceled,
        /// <summary>
        /// Withdraw validation passed
        /// </summary>
        [Map("pass")]
		Pass,
        /// <summary>
        /// Withdraw validation rejected
        /// </summary>
        [Map("reject")]
		Reject,
        /// <summary>
        /// Withdraw is about to be released
        /// </summary>
        [Map("pre-transfer")]
		PreTransfer,
        /// <summary>
        /// On-chain transfer initiated
        /// </summary>
        [Map("wallet-transfer")]
		WalletTransfer,
        /// <summary>
        /// Transfer rejected by chain
        /// </summary>
        [Map("wallet-reject")]
		WalletReject,
        /// <summary>
        /// On-chain transfer completed with one confirmation for withdraw or for at least one block for deposit
        /// </summary>
        [Map("confirmed")]
		Confirmed,
        /// <summary>
        /// On-chain transfer faied to get confirmation
        /// </summary>
		[Map("confirm-error")]
        ConfirmError,
        /// <summary>
        /// Withdraw terminated by system
        /// </summary>
        [Map("repealed")]
		Repealed,
        /// <summary>
        /// On-chain transfer has not been received
        /// </summary>
        [Map("unknown")]
		Unknown,
        /// <summary>
        /// On-chain transfer waits for first confirmation
        /// </summary>
        [Map("confirming")]
		Confirming,
        /// <summary>
        /// Multiple on-chain confirmation happened
        /// </summary>
        [Map("safe")]
		Safe,
        /// <summary>
        /// Confirmed but currently in an orphan branch
        /// </summary>
        [Map("orphan")]
        Orphan
    }
}
