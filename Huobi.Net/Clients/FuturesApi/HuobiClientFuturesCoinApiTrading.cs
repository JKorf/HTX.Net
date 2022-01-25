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
    public class HuobiClientFuturesCoinApiTrading : IHuobiClientFuturesCoinApiTrading
    {
        private const string SymbolTradesEndpoint = "contract_matchresults";

        private readonly HuobiClientFuturesCoinApi _baseClient;

        internal HuobiClientFuturesCoinApiTrading(HuobiClientFuturesCoinApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiFuturesTradeResponse>> GetUserTradesAsync(string symbol, TradeType? tradeType, int daysLookback = 90, string? contractCode = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            daysLookback.ValidateIntBetween(nameof(daysLookback), 1, 90);
            limit?.ValidateIntBetween(nameof(limit), 1, 50);

            var tradeTypeConverter = new TradeTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol},
                { "trade_type", JsonConvert.SerializeObject(TradeType.All, tradeTypeConverter) },
                { "create_date", daysLookback}
            };

            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", limit);

            return await _baseClient.SendHuobiFuturesRequest<HuobiFuturesTradeResponse>(_baseClient.GetUrl(SymbolTradesEndpoint, "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
    }
}
