using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters.Futures;
using Huobi.Net.Enums.Futures;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Objects.Models.Futures;
using Newtonsoft.Json;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientFuturesUsdtApiTrading : IHuobiClientFuturesUsdtApiTrading
    {
        private const string Api = "linear-swap-api";

        private const string IsolatedTradesEndpoint = "swap_matchresults";
        private const string CrossTradesEndpoint = "swap_cross_matchresults";

        private readonly HuobiClientFuturesUsdtApi _baseClient;

        internal HuobiClientFuturesUsdtApiTrading(HuobiClientFuturesUsdtApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesIsolatedAsync(string contractCode, TradeType? tradeType, int daysLookback = 90, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            daysLookback.ValidateIntBetween(nameof(daysLookback), 1, 90);
            limit?.ValidateIntBetween(nameof(limit), 1, 50);

            var tradeTypeConverter = new TradeTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode},
                { "trade_type", JsonConvert.SerializeObject(TradeType.All, tradeTypeConverter) },
                { "create_date", daysLookback}
            };

            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", limit);

            return await _baseClient.SendHuobiFuturesRequest<HuobiFuturesTradeResponse>(_baseClient.GetUrl(IsolatedTradesEndpoint, Api, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        public async Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesCrossAsync(string? contractCode, string? pair, TradeType? tradeType = null, int daysLookback = 90, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            daysLookback.ValidateIntBetween(nameof(daysLookback), 1, 90);
            limit?.ValidateIntBetween(nameof(limit), 1, 50);

            var tradeTypeConverter = new TradeTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "trade_type", JsonConvert.SerializeObject(TradeType.All, tradeTypeConverter) },
                { "create_date", daysLookback}
            };

            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", limit);

            return await _baseClient.SendHuobiFuturesRequest<HuobiFuturesTradeResponse>(_baseClient.GetUrl(CrossTradesEndpoint, Api, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
    }
}
