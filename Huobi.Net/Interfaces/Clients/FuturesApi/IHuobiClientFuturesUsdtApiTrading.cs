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
    public interface IHuobiClientFuturesUsdtApiTrading
    {
        /// <summary>
        /// Gets a list of isolated trades for a specific symbol
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-acquire-history-match-results" /></para>
        /// </summary>
        /// <param name="contractCode">The contract code to retrieve trades for</param>
        /// <param name="tradeType">Only return trades with specific trade types</param>
        /// <param name="daysLookback">Number of days to look back. Maximum range is 90 days and this will be used by default</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Page limit (min 1, max 50, default 20)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesIsolatedAsync(string contractCode, TradeType? tradeType = null, int daysLookback = 90, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of cross margin trades for a specific symbol
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-match-results" /></para>
        /// </summary>
        /// <param name="contractCode">The contract code to retrieve trades for</param>
        /// <param name="pair">The pair code to retrieve trades for</param>
        /// <param name="tradeType">Only return trades with specific trade types</param>
        /// <param name="daysLookback">Number of days to look back. Maximum range is 90 days and this will be used by default</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Page limit (min 1, max 50, default 20)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesCrossAsync(string? contractCode = null, string? pair = null, TradeType? tradeType = null, int daysLookback = 90, int? page = null, int? limit = null, CancellationToken ct = default);

    }
}
