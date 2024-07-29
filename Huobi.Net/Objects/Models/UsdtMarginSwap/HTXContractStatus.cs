using CryptoExchange.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract status
    /// </summary>
    public record HTXContractStatus
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Open order access
        /// </summary>
        [JsonPropertyName("open")]
        public bool Open { get; set; }
        /// <summary>
        /// Close order access
        /// </summary>
        [JsonPropertyName("close")]
        public bool Close { get; set; }
        /// <summary>
        /// Cancel order access
        /// </summary>
        [JsonPropertyName("cancel")]
        public bool Cancel { get; set; }
        /// <summary>
        /// Deposit access
        /// </summary>
        [JsonPropertyName("transfer_in")]
        public bool TranferIn { get; set; }
        /// <summary>
        /// Withdrawal access
        /// </summary>
        [JsonPropertyName("transfer_out")]
        public bool TransferOut { get; set; }
        /// <summary>
        /// Transfer from master to sub
        /// </summary>
        [JsonPropertyName("master_transfer_sub")]
        public bool MasterTransferSub { get; set; }
        /// <summary>
        /// Transfer from sub to master
        /// </summary>
        [JsonPropertyName("sub_transfer_master")]
        public bool SubTransferMaster { get; set; }
        /// <summary>
        /// Master transfer sub inner in
        /// </summary>
        [JsonPropertyName("master_transfer_sub_inner_in")]
        public bool MasterTransferSubInnerIn { get; set; }
        /// <summary>
        /// Master transfer sub inner out
        /// </summary>
        [JsonPropertyName("master_transfer_sub_inner_out")]
        public bool MasterTransferSubInnerOut { get; set; }
        /// <summary>
        /// Sub transfer master inner in
        /// </summary>
        [JsonPropertyName("sub_transfer_master_inner_in")]
        public bool SubTransferMasterInnerIn { get; set; }
        /// <summary>
        /// Sub transfer master inner out
        /// </summary>
        [JsonPropertyName("sub_transfer_master_inner_out")]
        public bool SubTransferMasterInnerOut { get; set; }
        /// <summary>
        /// Transfer inner in
        /// </summary>
        [JsonPropertyName("transfer_inner_in")]
        public bool TransferInnerIn { get; set; }
        /// <summary>
        /// Tranfer inner out
        /// </summary>
        [JsonPropertyName("transfer_inner_out")]
        public bool TransferInnerOut { get; set; }
    }
}
