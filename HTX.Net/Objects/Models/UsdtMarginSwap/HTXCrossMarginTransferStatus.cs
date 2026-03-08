using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin transfer status
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginTransferStatus
    {
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]

        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transfer_in</c>"] Deposit access
        /// </summary>
        [JsonPropertyName("transfer_in")]
        public bool TranferIn { get; set; }
        /// <summary>
        /// ["<c>transfer_out</c>"] Withdrawal access
        /// </summary>
        [JsonPropertyName("transfer_out")]
        public bool TransferOut { get; set; }
        /// <summary>
        /// ["<c>master_transfer_sub</c>"] Transfer from master to sub
        /// </summary>
        [JsonPropertyName("master_transfer_sub")]
        public bool MasterTransferSub { get; set; }
        /// <summary>
        /// ["<c>sub_transfer_master</c>"] Transfer from sub to master
        /// </summary>
        [JsonPropertyName("sub_transfer_master")]
        public bool SubTransferMaster { get; set; }
        /// <summary>
        /// ["<c>master_transfer_sub_inner_in</c>"] Master transfer sub inner in
        /// </summary>
        [JsonPropertyName("master_transfer_sub_inner_in")]
        public bool MasterTransferSubInnerIn { get; set; }
        /// <summary>
        /// ["<c>master_transfer_sub_inner_out</c>"] Master transfer sub inner out
        /// </summary>
        [JsonPropertyName("master_transfer_sub_inner_out")]
        public bool MasterTransferSubInnerOut { get; set; }
        /// <summary>
        /// ["<c>sub_transfer_master_inner_in</c>"] Sub transfer master inner in
        /// </summary>
        [JsonPropertyName("sub_transfer_master_inner_in")]
        public bool SubTransferMasterInnerIn { get; set; }
        /// <summary>
        /// ["<c>sub_transfer_master_inner_out</c>"] Sub transfer master inner out
        /// </summary>
        [JsonPropertyName("sub_transfer_master_inner_out")]
        public bool SubTransferMasterInnerOut { get; set; }
        /// <summary>
        /// ["<c>transfer_inner_in</c>"] Transfer inner in
        /// </summary>
        [JsonPropertyName("transfer_inner_in")]
        public bool TransferInnerIn { get; set; }
        /// <summary>
        /// ["<c>transfer_inner_out</c>"] Tranfer inner out
        /// </summary>
        [JsonPropertyName("transfer_inner_out")]
        public bool TransferInnerOut { get; set; }
    }
}
