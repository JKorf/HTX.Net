using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Converters.Futures;
using Huobi.Net.Enums;
using Huobi.Net.Enums.Futures;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Futures
{
    public class HuobiFuturesTradeResponse
    {
        public IEnumerable<HuobiFuturesOrderTrade> Trades { get; set; }
        [JsonProperty("total_page")]
        public int TotalPage { get; set; }
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        [JsonProperty("total_size")]
        public int TotalSize { get; set; }
    }

    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiFuturesOrderTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// The id of the match
        /// </summary>
        [JsonProperty("match_id")]
        public long MatchId { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The string id of the order
        /// </summary>
        [JsonProperty("order_id_str")]
        public string OrderIdString { get; set; }
        /// <summary>
        /// The symbol of the trade
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The order source of the order system, possible values: web, api, m, risk, settlement, ios, android, windows, mac, trigger, tpsl
        /// </summary>
        [JsonProperty("order_source")]
        public string OrderSource { get; set; }
        /// <summary>
        /// The contract type
        /// </summary>
        [JsonProperty("contract_type"), JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// The contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; }
        /// <summary>
        /// The direction
        /// </summary>
        [JsonProperty("direction"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// The offset
        /// </summary>
        [JsonProperty("offset"), JsonConverter(typeof(OffsetConverter))]
        public Offset Offset { get; set; }
        /// <summary>
        /// The trade volume
        /// </summary>
        [JsonProperty("trade_volume")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The trade price
        /// </summary>
        [JsonProperty("trade_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The trade turnover in USD
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal TurnoverUsd { get; set; }
        /// <summary>
        /// The timestamp in milliseconds when this record is created
        /// </summary>
        [JsonProperty("create_date"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The profits and losses generated from closing positions(calculated with the average price of position, exclude profit in history settlement.)
        /// </summary>
        [JsonProperty("offset_profitloss")]
        public decimal OffsetProfitLoss { get; set; }
        /// <summary>
        /// The fees charged by platform
        /// </summary>
        [JsonProperty("trade_fee")]
        public decimal TradeFee { get; set; }
        /// <summary>
        /// The role in the transaction: taker or maker
        /// </summary>
        [JsonProperty("role"), JsonConverter(typeof(OrderRoleConverter))]
        public OrderRole Role { get; set; }
        /// <summary>
        /// The corresponding cryptocurrency to the given fee
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The real profit (calculated with the opening average price, include profit in history settlement.)
        /// </summary>
        [JsonProperty("real_profit")]
        public decimal RealProfit { get; set; }
        /// <summary>
        /// The margin mode: "isolated"
        /// </summary>
        [JsonProperty("margin_mode")]
        public string MarginMode { get; set; }
        /// <summary>
        /// The margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; }
        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("pair")]
        public string Pair { get; set; }
        /// <summary>
        /// The business type: futures, swap
        /// </summary>
        [JsonProperty("business_type")]
        public string BusinessType { get; set; }
    }
}
