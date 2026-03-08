using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Transfer limit
    /// </summary>
    [SerializationModel]
    public record HTXTransferLimit
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
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
        /// ["<c>transfer_in_max_each</c>"] Transfer in max per transfer
        /// </summary>
        [JsonPropertyName("transfer_in_max_each")]
        public decimal TransferInMaxEach { get; set; }
        /// <summary>
        /// ["<c>transfer_in_min_each</c>"] Transfer in min per transfer
        /// </summary>
        [JsonPropertyName("transfer_in_min_each")]
        public decimal TransferInMinEach { get; set; }
        /// <summary>
        /// ["<c>transfer_out_max_each</c>"] Transfer out max per transfer
        /// </summary>
        [JsonPropertyName("transfer_out_max_each")]
        public decimal TransferOutMaxEach { get; set; }
        /// <summary>
        /// ["<c>transfer_out_min_each</c>"] Transfer out min per transfer
        /// </summary>
        [JsonPropertyName("transfer_out_min_each")]
        public decimal TransferOutMinEach { get; set; }
        /// <summary>
        /// ["<c>transfer_in_max_daily</c>"] Transfer in max daily
        /// </summary>
        [JsonPropertyName("transfer_in_max_daily")]
        public decimal TransferInMaxDaily { get; set; }
        /// <summary>
        /// ["<c>transfer_out_max_daily</c>"] Transfer out max daily
        /// </summary>
        [JsonPropertyName("transfer_out_max_daily")]
        public decimal TransferOutMaxDaily { get; set; }
        /// <summary>
        /// ["<c>net_transfer_in_max_daily</c>"] Net transfer in max daily
        /// </summary>
        [JsonPropertyName("net_transfer_in_max_daily")]
        public decimal NetTransferInMaxDaily { get; set; }
        /// <summary>
        /// ["<c>net_transfer_out_max_daily</c>"] Net transfer out max daily
        /// </summary>
        [JsonPropertyName("net_transfer_out_max_daily")]
        public decimal NetTransferOutMaxDaily { get; set; }
    }


}
