using System;
using System.Collections.Generic;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Order book
    /// </summary>
    public class HuobiOrderBook: ICommonOrderBook
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonIgnore]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of bids
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();
        /// <summary>
        /// List of asks
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();

        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonBids => Bids;
        IEnumerable<ISymbolOrderBookEntry> ICommonOrderBook.CommonAsks => Asks;
    }

    /// <summary>
    /// Incremental order book update
    /// </summary>
    public class HuobiIncementalOrderBook
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonProperty("seqNum")]
        public long SequenceNumber { get; set; }
        /// <summary>
        /// Previous sequence number
        /// </summary>
        [JsonProperty("prevSeqNum")]
        public long? PreviousSequenceNumber { get; set; }
        /// <summary>
        /// List of changed bids
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();
        /// <summary>
        /// List of changed asks
        /// </summary>
        public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();
    }
}
