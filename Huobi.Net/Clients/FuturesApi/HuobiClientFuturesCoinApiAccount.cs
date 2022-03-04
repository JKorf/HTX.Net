using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Futures;
using Huobi.Net.Objects.Models.Swaps;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientFuturesCoinApiAccount : IHuobiClientFuturesCoinApiAccount
    {
        private const string BalancesEndpoint = "contract_account_info";
        private const string PositionsEndpoint = "contract_position_info";

        private readonly HuobiClientFuturesCoinApi _baseClient;

        internal HuobiClientFuturesCoinApiAccount(HuobiClientFuturesCoinApi baseClient)
        {
            _baseClient = baseClient;
        }

        public async Task<WebCallResult<IEnumerable<HuobiFuturesBalance>>> GetBalancesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            return await _baseClient.SendHuobiFuturesRequest<IEnumerable<HuobiFuturesBalance>>(_baseClient.GetUrl(PositionsEndpoint, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiFuturesPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            return await _baseClient.SendHuobiFuturesRequest<IEnumerable<HuobiFuturesPosition>>(_baseClient.GetUrl(PositionsEndpoint, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
    }
}
