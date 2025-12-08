using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{ 
    
    /// <summary>
    /// Sub transfer page
    /// </summary>
    [SerializationModel]
    public record HTXMasterSubTransferPage : HTXPage
    {
        /// <summary>
        /// Transfers
        /// </summary>
        [JsonPropertyName("transfer_record")]
        public HTXMasterSubTransfer[] Transfers { get; set; } = Array.Empty<HTXMasterSubTransfer>();
    }

    /// <summary>
    /// Transfer between master and sub account
    /// </summary>
    [SerializationModel]
    public record HTXMasterSubTransfer
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// From margin account
        /// </summary>
        [JsonPropertyName("from_margin_account")]
        public string FromMarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// To margin account
        /// </summary>
        [JsonPropertyName("to_margin_account")]
        public string ToMarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Sub account name
        /// </summary>
        [JsonPropertyName("sub_account_name")]
        public string SubAccountName { get; set; } = string.Empty;
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonPropertyName("transfer_type")]

        public MasterSubTransferType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
