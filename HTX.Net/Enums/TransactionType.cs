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
        /// Trade
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// ETF
        /// </summary>
        [Map("etf")]
        Etf,
        /// <summary>
        /// Transaction fee
        /// </summary>
        [Map("transact-fee")]
        TransactionFee,
        /// <summary>
        /// Deduction
        /// </summary>
        [Map("fee-deduction")]
        Deduction,
        /// <summary>
        /// Transfer between accounts
        /// </summary>
        [Map("transfer")]
        Transfer,
        /// <summary>
        /// Credit
        /// </summary>
        [Map("credit")]
        Credit,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("liquidation")]
        Liquidation,
        /// <summary>
        /// Interest
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// Withdraw
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// Withdraw fee
        /// </summary>
        [Map("withdraw-fee")]
        WithdrawFee,
        /// <summary>
        /// Exchange
        /// </summary>
        [Map("exchange")]
        Exchange,
        /// <summary>
        /// Other types
        /// </summary>
        [Map("other-types")]
        Other,
        /// <summary>
        /// Rebate
        /// </summary>
        [Map("rebate")]
        Rebate
    }
}
