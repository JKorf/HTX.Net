using CryptoExchange.Net.Objects.Options;
using System;

namespace Huobi.Net.Objects.Options
{
    /// <summary>
    /// Options for the Huobi SymbolOrderBook
    /// </summary>
    public class HuobiOrderBookOptions: OrderBookOptions
    {
        /// <summary>
        /// Default options for the Huobi SymbolOrderBook
        /// </summary>
        public static HuobiOrderBookOptions Default { get; set; } = new HuobiOrderBookOptions();

        /// <summary>
        /// The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places
        /// </summary>
        public int? MergeStep { get; set; }

        /// <summary>
        /// The amount of entries to maintain. Either 5, 20 or 150. Level 5 and 20 are currently only supported for the following symbols: btcusdt, ethusdt, xrpusdt, eosusdt, ltcusdt, etcusdt, adausdt, dashusdt, bsvusdt.
        /// </summary>
        public int? Levels { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal HuobiOrderBookOptions Copy()
        {
            var options = Copy<HuobiOrderBookOptions>();
            options.MergeStep = MergeStep;
            options.Levels = Levels;
            options.InitialDataTimeout = InitialDataTimeout;
            return options;
        }
    }
}
