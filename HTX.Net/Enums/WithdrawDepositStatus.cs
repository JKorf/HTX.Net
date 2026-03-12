using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// The status of a transfer 
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawDepositStatus>))]
    public enum WithdrawDepositStatus
    {
        /// <summary>
        /// ["<c>verifying</c>"] Awaiting verification
        /// </summary>
        [Map("verifying")]
        Verifying,
        /// <summary>
        /// ["<c>failed</c>"] Verification failed
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// ["<c>submitted</c>"] Withdraw request submitted successfully
        /// </summary>
		[Map("submitted")]
        Submitted,
        /// <summary>
        /// ["<c>reexamine</c>"] Under examination for withdraw validation
        /// </summary>
        [Map("reexamine")]
        Reexamine,
        /// <summary>
        /// ["<c>canceled</c>"] Withdraw canceled by user
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>pass</c>"] Withdraw validation passed
        /// </summary>
        [Map("pass")]
        Pass,
        /// <summary>
        /// ["<c>reject</c>"] Withdraw validation rejected
        /// </summary>
        [Map("reject")]
        Reject,
        /// <summary>
        /// ["<c>pre-transfer</c>"] Withdraw is about to be released
        /// </summary>
        [Map("pre-transfer")]
        PreTransfer,
        /// <summary>
        /// ["<c>wallet-transfer</c>"] On-chain transfer initiated
        /// </summary>
        [Map("wallet-transfer")]
        WalletTransfer,
        /// <summary>
        /// ["<c>wallet-reject</c>"] Transfer rejected by chain
        /// </summary>
        [Map("wallet-reject")]
        WalletReject,
        /// <summary>
        /// ["<c>confirmed</c>"] On-chain transfer completed with one confirmation for withdraw or for at least one block for deposit
        /// </summary>
        [Map("confirmed")]
        Confirmed,
        /// <summary>
        /// ["<c>confirm-error</c>"] On-chain transfer failed to get confirmation
        /// </summary>
		[Map("confirm-error")]
        ConfirmError,
        /// <summary>
        /// ["<c>repealed</c>"] Withdraw terminated by system
        /// </summary>
        [Map("repealed")]
        Repealed,
        /// <summary>
        /// ["<c>unknown</c>"] On-chain transfer has not been received
        /// </summary>
        [Map("unknown")]
        Unknown,
        /// <summary>
        /// ["<c>confirming</c>"] On-chain transfer waits for first confirmation
        /// </summary>
        [Map("confirming")]
        Confirming,
        /// <summary>
        /// ["<c>safe</c>"] Multiple on-chain confirmation happened
        /// </summary>
        [Map("safe")]
        Safe,
        /// <summary>
        /// ["<c>orphan</c>"] Confirmed but currently in an orphan branch
        /// </summary>
        [Map("orphan")]
        Orphan,
        /// <summary>
        /// ["<c>waiting-tiny-amount</c>"] Waiting tiny amount
        /// </summary>
        [Map("waiting-tiny-amount")]
        WaitingTinyAmount
    }
}
