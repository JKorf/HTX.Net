using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Withdraw or Deposit
    /// </summary>
    public record HTXWithdrawDeposit
	{
		/// <summary>
		/// Transfer id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Define transfer type to search, possible values: [deposit, withdraw]
		/// </summary>
		[JsonPropertyName("type"), JsonConverter(typeof(EnumConverter))]
		public WithdrawDepositType Type { get; set; }
		/// <summary>
		/// Sub type
		/// </summary>
		[JsonPropertyName("sub-type")]
		public string SubType { get; set; } = string.Empty;
		/// <summary>
		/// The crypto asset to withdraw
		/// </summary>
        [JsonPropertyName("currency")]
		public string? Asset { get; set; }
		/// <summary>
		/// The on-chain transaction hash
		/// </summary>
		[JsonPropertyName("tx-hash")]
		public string? TransactionHash { get; set; }
		/// <summary>
		/// Block chain name
		/// </summary>
        [JsonPropertyName("chain")]
		public string? Network { get; set; }
		/// <summary>
		/// The number of crypto asset transfered in its minimum unit
		/// </summary>
		[JsonPropertyName("amount")]
		public decimal Quantity { get; set; }
		/// <summary>
		/// The deposit or withdraw target address
		/// </summary>
		public string? Address { get; set; }
		/// <summary>
		/// The user defined address tag
		/// </summary>
		[JsonPropertyName("address-tag")]
		public string? AddressTag { get; set; }
		/// <summary>
		/// The address tag of the address its from
		/// </summary>
		[JsonPropertyName("from-addr-tag")]
		public string? FromAddressTag { get; set; }
		/// <summary>
		/// Withdraw fee
		/// </summary>
		public decimal Fee { get; set; }
		/// <summary>
		/// The state of this transfer
		/// </summary>
		[JsonPropertyName("state"), JsonConverter(typeof(EnumConverter))]
		public WithdrawDepositState State { get; set; }
		/// <summary>
		/// Error code for withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
		/// </summary>
		[JsonPropertyName("error-code")]
		public string? ErrorCode { get; set; }
		/// <summary>
		/// Error description of withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
		/// </summary>
		[JsonPropertyName("error-msg")]
		public string? ErrorMessage { get; set; }
		/// <summary>
		/// The timestamp in milliseconds for the transfer creation
		/// </summary>
		[JsonPropertyName("created-at"), JsonConverter(typeof(DateTimeConverter))]
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// The timestamp in milliseconds for the transfer's latest update
		/// </summary>
		[JsonPropertyName("updated-at"), JsonConverter(typeof(DateTimeConverter))]
		public DateTime UpdateTime { get; set; }
	}
}
