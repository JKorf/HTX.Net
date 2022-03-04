
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Futures;
using Huobi.Net.Objects.Models.Swaps;

namespace Huobi.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IHuobiClientFuturesCoinApiAccount
    {
        /// <summary>
        /// Gets a list of balances
        /// <para><a href="https://huobiapi.github.io/docs/dm/v1/en/#query-user-s-account-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesBalance>>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of positions
        /// <para><a href="https://huobiapi.github.io/docs/dm/v1/en/#query-user-s-position-information" /></para>
        /// </summary>
        /// <param name="symbol">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFuturesPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);
    }
}
