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
        /// Get funding rate
        /// </summary>
        Task<WebCallResult<HTXFundingRateV5>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate history
        /// </summary>
        Task<WebCallResult<HTXFundingRateHistoryV5[]>> GetFundingRateHistoryAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// </summary>
        Task<WebCallResult<HTXOpenInterestV5>> GetOpenInterestAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get price limits
        /// </summary>
        Task<WebCallResult<HTXPriceLimitV5[]>> GetPriceLimitsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get market risk limits
        /// </summary>
        Task<WebCallResult<HTXRiskLimitV5[]>> GetRiskLimitsAsync(string contractCode, MarginMode? marginMode = null, string? tier = null, CancellationToken ct = default);
    }
}
