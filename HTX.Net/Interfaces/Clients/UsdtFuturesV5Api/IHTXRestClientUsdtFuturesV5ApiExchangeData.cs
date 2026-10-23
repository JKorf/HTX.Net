using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// HTX usdt futures V5 exchange data endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesV5ApiExchangeData
    {
        /// <summary>
        /// Get funding rate info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19b97d0c0bf" /><br />
        /// Endpoint:<br />
        /// GET /v5/market/funding_rate
        /// </para>
        /// </summary>
        /// <param name="contractCode">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXFundingRateV5[]>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19b97ea5941" /><br />
        /// Endpoint:<br />
        /// GET /v5/market/funding_rate_history 
        /// </para>
        /// </summary>
        /// <param name="contractCode">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="fromId">Filter by from ID</param>
        /// <param name="limit">Limit the number of results, max 100</param>
        /// <param name="direction">Direction</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXFundingRateHistoryV5[]>> GetFundingRateHistoryAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19b97fb53f2" /><br />
        /// Endpoint:<br />
        /// GET /v5/market/open_interest 
        /// </para>
        /// </summary>
        /// <param name="contractCode">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXOpenInterestV5>> GetOpenInterestAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get price limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19b980aed1a" /><br />
        /// Endpoint:<br />
        /// GET /v5/market/price_limit 
        /// </para>
        /// </summary>
        /// <param name="contractCode">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXPriceLimitV5[]>> GetPriceLimitsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get market risk limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-195a1c64aef" /><br />
        /// Endpoint:<br />
        /// GET /v5/market/risk/limit
        /// </para>
        /// </summary>
        /// <param name="contractCode">Symbol</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="tier">Tier</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXRiskLimitV5[]>> GetRiskLimitsAsync(string contractCode, MarginMode? marginMode = null, string? tier = null, CancellationToken ct = default);
    }
}
