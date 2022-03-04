using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Objects.Models.Futures;
using Huobi.Net.Objects.Models.Swaps;

namespace Huobi.Net.Interfaces.Clients.SwapsApi
{
    /// <summary>
    /// Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IHuobiClientSwapsCoinApiAccount
    {
        /// <summary>
        /// Gets a list of balances
        /// <para><a href="https://huobiapi.github.io/docs/coin_margined_swap/v1/en/#query-user-s-account-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapsBalance>>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of positions
        /// <para><a href="https://huobiapi.github.io/docs/dm/v1/en/#query-user-s-position-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapsPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);
    }
}
