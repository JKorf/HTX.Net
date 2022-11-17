using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin order info
    /// </summary>
    public class HuobiMarginOrderDetails : HuobiIsolatedMarginOrder
    {
        /// <summary>
        /// Instrument price
        /// </summary>
        [JsonProperty("instrument_price")]
        public decimal IntrumentPrice { get; set; }
        /// <summary>
        /// Final interest
        /// </summary>
        [JsonProperty("final_interest")]
        public decimal FinalInterest { get; set; }
        /// <summary>
        /// Adjust value
        /// </summary>
        [JsonProperty("adjust_value")]
        public decimal AdjustValue { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total amount of records
        /// </summary>
        [JsonProperty("total_size")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Trades
        /// </summary>
        public new IEnumerable<HuobiMarginTrade> Trades { get; set; } = Array.Empty<HuobiMarginTrade>();
    }
}
