using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Withdraw or Deposit
    /// </summary>
    [SerializationModel]
    public record HTXWithdrawDeposit
	{
		/// <summary>
		/// ["<c>id</c>"] Transfer id
		/// </summary>
        [JsonPropertyName("id")]
		public long Id { get; set; }
        /// <summary>
        /// ["<c>client-order-id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client-order-id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Define transfer type to search, possible values: [deposit, withdraw]
        /// </summary>
        [JsonPropertyName("type")]
		public WithdrawDepositType Type { get; set; }
		/// <summary>
		/// ["<c>sub-type</c>"] Sub type
		/// </summary>
		[JsonPropertyName("sub-type")]
		public string SubType { get; set; } = string.Empty;
		/// <summary>
		/// ["<c>currency</c>"] The crypto asset to withdraw
		/// </summary>
        [JsonPropertyName("currency")]
		public string? Asset { get; set; }
		/// <summary>
		/// ["<c>tx-hash</c>"] The on-chain transaction hash
		/// </summary>
		[JsonPropertyName("tx-hash")]
		public string? TransactionHash { get; set; }
		/// <summary>
		/// ["<c>chain</c>"] Block chain name
		/// </summary>
        [JsonPropertyName("chain")]
		public string? Network { get; set; }
		/// <summary>
		/// ["<c>amount</c>"] The number of crypto asset transfered in its minimum unit
		/// </summary>
		[JsonPropertyName("amount")]
		public decimal Quantity { get; set; }
		/// <summary>
		/// ["<c>address</c>"] The deposit or withdraw target address
		/// </summary>
        [JsonPropertyName("address")]
		public string? Address { get; set; }
		/// <summary>
		/// ["<c>address-tag</c>"] The user defined address tag
		/// </summary>
		[JsonPropertyName("address-tag")]
		public string? AddressTag { get; set; }
		/// <summary>
		/// ["<c>from-addr-tag</c>"] The address tag of the address its from
		/// </summary>
		[JsonPropertyName("from-addr-tag")]
		public string? FromAddressTag { get; set; }
		/// <summary>
		/// ["<c>fee</c>"] Withdraw fee
		/// </summary>
        [JsonPropertyName("fee")]
		public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>state</c>"] The status of this transfer
        /// </summary>
        [JsonPropertyName("state")]
		public WithdrawDepositStatus Status { get; set; }
		/// <summary>
		/// ["<c>error-code</c>"] Error code for withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
		/// </summary>
		[JsonPropertyName("error-code")]
		public string? ErrorCode { get; set; }
		/// <summary>
		/// ["<c>error-msg</c>"] Error description of withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
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
