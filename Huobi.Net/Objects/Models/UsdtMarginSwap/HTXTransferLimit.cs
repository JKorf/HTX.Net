using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Transfer limit
    /// </summary>
    public record HTXTransferLimit
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Transfer in max per transfer
        /// </summary>
        [JsonPropertyName("transfer_in_max_each")]
        public decimal TransferInMaxEach { get; set; }
        /// <summary>
        /// Transfer in min per transfer
        /// </summary>
        [JsonPropertyName("transfer_in_min_each")]
        public decimal TransferInMinEach { get; set; }
        /// <summary>
        /// Transfer out max per transfer
        /// </summary>
        [JsonPropertyName("transfer_out_max_each")]
        public decimal TransferOutMaxEach { get; set; }
        /// <summary>
        /// Transfer out min per transfer
        /// </summary>
        [JsonPropertyName("transfer_out_min_each")]
        public decimal TransferOutMinEach { get; set; }
        /// <summary>
        /// Transfer in max daily
        /// </summary>
        [JsonPropertyName("transfer_in_max_daily")]
        public decimal TransferInMaxDaily { get; set; }
        /// <summary>
        /// Transfer out max daily
        /// </summary>
        [JsonPropertyName("transfer_out_max_daily")]
        public decimal TransferOutMaxDaily { get; set; }
        /// <summary>
        /// Net transfer in max daily
        /// </summary>
        [JsonPropertyName("net_transfer_in_max_daily")]
        public decimal NetTransferInMaxDaily { get; set; }
        /// <summary>
        /// Net transfer out max daily
        /// </summary>
        [JsonPropertyName("net_transfer_out_max_daily")]
        public decimal NetTransferOutMaxDaily { get; set; }
    }


}
