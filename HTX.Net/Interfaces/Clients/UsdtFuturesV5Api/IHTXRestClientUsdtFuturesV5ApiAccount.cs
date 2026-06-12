using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtFuturesV5;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesV5Api
{
    /// <summary>
    /// HTX usdt futures V5 account endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesV5ApiAccount
    {
        /// <summary>
        /// Get asset mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1958871f034" /><br />
        /// Endpoint:<br />
        /// GET /v5/account/asset_mode
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXAssetModeV5>> GetAssetModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Set asset mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-195886fed64" /><br />
        /// Endpoint:<br />
        /// POST /v5/account/asset_mode
        /// </para>
        /// </summary>
        /// <param name="assetMode">["<c>assets_mode</c>"] Asset mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXAssetModeUpdateV5>> SetAssetModeAsync(AssetMode assetMode, CancellationToken ct = default);

        /// <summary>
        /// Get account balance
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19588469969" /><br />
        /// Endpoint:<br />
        /// GET /v5/account/balance
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXAccountBalanceV5>> GetAccountBalanceAsync(CancellationToken ct = default);

        /// <summary>
        /// Query financial records
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19b930b8bee" /><br />
        /// Endpoint:<br />
        /// GET /v5/account/bills
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code</param>
        /// <param name="marginMode">["<c>margin_mode</c>"] Margin mode</param>
        /// <param name="types">["<c>type</c>"] Financial record types</param>
        /// <param name="startTime">["<c>start_time</c>"] Start time</param>
        /// <param name="endTime">["<c>end_time</c>"] End time</param>
        /// <param name="fromId">["<c>from</c>"] From id</param>
        /// <param name="limit">["<c>limit</c>"] Limit</param>
        /// <param name="direction">["<c>direct</c>"] Direction</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<HTXBillV5[]>> GetBillsAsync(string? contractCode = null, MarginMode? marginMode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Get position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1959442c16b" /><br />
        /// Endpoint:<br />
        /// GET /v5/position/mode
        /// </para>
        /// </summary>
        Task<HttpResult<HTXPositionModeV5>> GetPositionModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1959443cae3" /><br />
        /// Endpoint:<br />
        /// POST /v5/position/mode
        /// </para>
        /// </summary>
        Task<HttpResult<HTXPositionModeV5>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

        /// <summary>
        /// Get leverage list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1959436a93a" /><br />
        /// Endpoint:<br />
        /// GET /v5/position/lever
        /// </para>
        /// </summary>
        Task<HttpResult<HTXLeverageV5[]>> GetLeverageAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-1959439f997" /><br />
        /// Endpoint:<br />
        /// POST /v5/position/lever
        /// </para>
        /// </summary>
        Task<HttpResult<HTXLeverageV5>> SetLeverageAsync(string contractCode, MarginMode marginMode, int leverageRate, FuturesPositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get current position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19594266bd8" /><br />
        /// Endpoint:<br />
        /// GET /v5/trade/position/opens
        /// </para>
        /// </summary>
        Task<HttpResult<HTXPositionV5[]>> GetOpenPositionsAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get position risk limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-19594451302" /><br />
        /// Endpoint:<br />
        /// GET /v5/position/risk/limit
        /// </para>
        /// </summary>
        Task<HttpResult<HTXPositionRiskLimitV5[]>> GetRiskLimitsAsync(string? contractCode = null, MarginMode? marginMode = null, FuturesPositionSide? positionSide = null, CancellationToken ct = default);
    }
}
