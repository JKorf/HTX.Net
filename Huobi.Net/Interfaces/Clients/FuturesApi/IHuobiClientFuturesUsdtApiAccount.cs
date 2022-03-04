using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Objects.Models.Futures;
using Huobi.Net.Objects.Models.Swaps;

namespace Huobi.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IHuobiClientFuturesUsdtApiAccount
    {
        /// <summary>
        /// Gets a list of balances
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-account-information" /></para>
        /// </summary>
        /// <param name="contractCode">The contract code to query balances for, returns all if null</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesUsdtBalance>>> GetBalancesIsolatedAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-account-information" /></para>
        /// </summary>
        /// <param name="marginAccount">The margin account to query balances for, i.e. "USDT", returns all if null</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesUsdtCrossBalance>>> GetBalancesCrossAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of positions
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-user-s-position-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesUsdtPosition>>> GetPositionsIsolatedAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of positions
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-user-39-s-position-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesUsdtCrossPosition>>> GetPositionsCrossAsync(string? symbol = null, CancellationToken ct = default);
    }
}
