using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// ["<c>trade</c>"] Trade
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// ["<c>etf</c>"] ETF
        /// </summary>
        [Map("etf")]
        Etf,
        /// <summary>
        /// ["<c>transact-fee</c>"] Transaction fee
        /// </summary>
        [Map("transact-fee")]
        TransactionFee,
        /// <summary>
        /// ["<c>fee-deduction</c>"] Deduction
        /// </summary>
        [Map("fee-deduction")]
        Deduction,
        /// <summary>
        /// ["<c>transfer</c>"] Transfer between accounts
        /// </summary>
        [Map("transfer")]
        Transfer,
        /// <summary>
        /// ["<c>credit</c>"] Credit
        /// </summary>
        [Map("credit")]
        Credit,
        /// <summary>
        /// ["<c>liquidation</c>"] Liquidation
        /// </summary>
        [Map("liquidation")]
        Liquidation,
        /// <summary>
        /// ["<c>interest</c>"] Interest
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdraw</c>"] Withdraw
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// ["<c>withdraw-fee</c>"] Withdraw fee
        /// </summary>
        [Map("withdraw-fee")]
        WithdrawFee,
        /// <summary>
        /// ["<c>exchange</c>"] Exchange
        /// </summary>
        [Map("exchange")]
        Exchange,
        /// <summary>
        /// ["<c>other-types</c>"] Other types
        /// </summary>
        [Map("other-types")]
        Other,
        /// <summary>
        /// ["<c>rebate</c>"] Rebate
        /// </summary>
        [Map("rebate")]
        Rebate
    }
}
