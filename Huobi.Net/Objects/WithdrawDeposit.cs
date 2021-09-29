using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Withdraw or Deposit
    /// </summary>
    public class WithdrawDeposit
	{
		/// <summary>
		/// Transfer id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Define transfer type to search, possible values: [deposit, withdraw]
		/// </summary>
		[JsonProperty("type"), JsonConverter(typeof(WithdrawDepositTypeConverter))]
		public WithdrawDepositType Type { get; set; }
		/// <summary>
		/// The crypto currency to withdraw
		/// </summary>
		public string? Currency { get; set; }
		/// <summary>
		/// The on-chain transaction hash
		/// </summary>
		[JsonProperty("tx-hash")]
		public string? TxHash { get; set; }
		/// <summary>
		/// Block chain name
		/// </summary>
		public string? Chain { get; set; }
		/// <summary>
		/// The number of crypto asset transfered in its minimum unit
		/// </summary>
		[JsonProperty("amount")]
		public decimal Quantity { get; set; }
		/// <summary>
		/// The deposit or withdraw target address
		/// </summary>
		public string? Address { get; set; }
		/// <summary>
		/// The user defined address tag
		/// </summary>
		public string? AddressTag { get; set; }
		/// <summary>
		/// Withdraw fee
		/// </summary>
		public decimal Fee { get; set; }
		/// <summary>
		/// The state of this transfer
		/// </summary>
		[JsonProperty("state"), JsonConverter(typeof(WithdrawDepositStateConverter))]
		public WithdrawDepositState State { get; set; }
		/// <summary>
		/// Error code for withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
		/// </summary>
		[JsonProperty("error-code")]
		public string? ErrorCode { get; set; }
		/// <summary>
		/// Error description of withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
		/// </summary>
		[JsonProperty("error-msg")]
		public string? ErrorMessage { get; set; }
		/// <summary>
		/// The timestamp in milliseconds for the transfer creation
		/// </summary>
		[JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// The timestamp in milliseconds for the transfer's latest update
		/// </summary>
		[JsonProperty("updated-at"), JsonConverter(typeof(TimestampConverter))]
		public DateTime UpdatedAt { get; set; }
	}
}
