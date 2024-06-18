﻿using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Contract status
    /// </summary>
    public record HuobiContractStatus
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Open order access
        /// </summary>
        public bool Open { get; set; }
        /// <summary>
        /// Close order access
        /// </summary>
        public bool Close { get; set; }
        /// <summary>
        /// Cancel order access
        /// </summary>
        public bool Cancel { get; set; }
        /// <summary>
        /// Deposit access
        /// </summary>
        [JsonProperty("transfer_in")]
        public bool TranferIn { get; set; }
        /// <summary>
        /// Withdrawal access
        /// </summary>
        [JsonProperty("transfer_out")]
        public bool TransferOut { get; set; }
        /// <summary>
        /// Transfer from master to sub
        /// </summary>
        [JsonProperty("master_transfer_sub")]
        public bool MasterTransferSub { get; set; }
        /// <summary>
        /// Transfer from sub to master
        /// </summary>
        [JsonProperty("sub_transfer_master")]
        public bool SubTransferMaster { get; set; }
        /// <summary>
        /// Master transfer sub inner in
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_in")]
        public bool MasterTransferSubInnerIn { get; set; }
        /// <summary>
        /// Master transfer sub inner out
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_out")]
        public bool MasterTransferSubInnerOut { get; set; }
        /// <summary>
        /// Sub transfer master inner in
        /// </summary>
        [JsonProperty("sub_transfer_master_inner_in")]
        public bool SubTransferMasterInnerIn { get; set; }
        /// <summary>
        /// Sub transfer master inner out
        /// </summary>
        [JsonProperty("sub_transfer_master_inner_out")]
        public bool SubTransferMasterInnerOut { get; set; }
        /// <summary>
        /// Transfer inner in
        /// </summary>
        [JsonProperty("transfer_inner_in")]
        public bool TransferInnerIn { get; set; }
        /// <summary>
        /// Tranfer inner out
        /// </summary>
        [JsonProperty("transfer_inner_out")]
        public bool TransferInnerOut { get; set; }
    }
}
