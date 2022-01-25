using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums.Futures;
using Huobi.Net.Objects.Models.Futures;

namespace Huobi.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Huobi trading endpoints, placing and managing orders.
    /// </summary>
    public interface IHuobiClientFuturesCoinApiTrading
    {

        /// <summary>
        /// Gets a list of trades for a specific symbol
        /// <para><a href="https://huobiapi.github.io/docs/dm/v1/en/#get-history-match-results" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to retrieve trades for</param>
        /// <param name="tradeType">Only return trades with specific trade types</param>
        /// <param name="daysLookback">Number of days to look back. Maximum range is 90 days and this will be used by default</param>
        /// <param name="contractCode">The contract code to retrieve trades for</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Page limit (min 1, max 50, default 20)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesAsync(string symbol, TradeType? tradeType = null, int daysLookback = 90, string? contractCode = null, int? page = null, int? limit = null, CancellationToken ct = default);
    }
}
