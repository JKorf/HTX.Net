using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// HTX usdt futures endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApiAccount
    {
        /// <summary>
        /// Get asset valuation
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8172e-77b5-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXAssetValue>>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81843-77b5-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="contractCode">Optional contract code filter, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginAccountInfo>>> GetIsolatedMarginAccountInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin account info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb819b6-77b5-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="marginAccount">Optional margin account filter, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginAccountInfo>>> GetCrossMarginAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin assets and positions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81e77-77b5-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginAssetsAndPositions>> GetCrossMarginAssetsAndPositionsAsync(string marginAccount, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin available leverage
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb82f42-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossMarginLeverageAvailable>>> GetCrossMarginAvailableLeverageAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin positions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81c49-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXCrossPosition>>> GetCrossMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin settlement records
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb82cf8-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXCrossMarginUserSettlementRecordPage>> GetCrossMarginSettlementRecordsAsync(string marginAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get financial records
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb82988-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="types">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="direction">Direction</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXFinancialRecord>>> GetFinancialRecordsAsync(string marginAccount, string? contractCode = null, IEnumerable<FinancialRecordType>? types = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin assets and positions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81d85-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginAssetsAndPositions>>> GetIsolatedMarginAssetsAndPositionsAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin available leverage
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb82e6c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginLeverageAvailable>>> GetIsolatedMarginAvailableLeverageAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin position info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81b5a-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXPosition>>> GetIsolatedMarginPositionsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin settlement records
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb82ba7-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginUserSettlementRecordPage>> GetIsolatedMarginSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get trading fees
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb831de-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXTradingFee>>> GetTradingFeesAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Set cross margin position mode
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb843e0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="positionMode">Position mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXPositionMode>> SetCrossMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);
        /// <summary>
        /// Set isolated margin position mode
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb842fe-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="positionMode">Position mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXPositionMode>> SetIsolatedMarginPositionModeAsync(string marginAccount, PositionMode positionMode, CancellationToken ct = default);
        /// <summary>
        /// Transfer between margin accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83f97-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer, for example `USDT`</param>
        /// <param name="fromMarginAccount">From account</param>
        /// <param name="toMarginAccount">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderId>> TransferMarginAccountsAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83090-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="orderType">The order type</param>
        /// <param name="contractCode">Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Filter by pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Filter by contract type</param>
        /// <param name="businessType">Filter by businessType</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXOrderLimit>> GetOrderLimitsAsync(OrderPriceType orderType, string? contractCode = null, string? pair = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin transfer limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83330-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXTransferLimit>>> GetIsolatedMarginTransferLimitsAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin transfer limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83475-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXCrossTransferLimit>>> GetCrossMarginTransferLimitsAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin position limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb835b7-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXPositionLimit>>> GetIsolatedMarginPositionLimitAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin position limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb836ae-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="pair">Pair, for example `ETH-USDT`</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXCrossMarginPositionLimit>>> GetCrossMarginPositionLimitsAsync(string? contractCode = null, string? pair = null, ContractType? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin leverage position limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb838ef-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code, for example `ETH-USDT`</param>
        /// <param name="leverageRate">Leverage rate</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXLeveragePositionLimit>>> GetIsolatedMarginLeveragePositionLimitsAsync(string? contractCode = null, int? leverageRate = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin leverage position limits
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb839e5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXCrossLeveragePositionLimit>>> GetCrossMarginLeveragePositionLimitsAsync(BusinessType businessType, CancellationToken ct = default);

        /// <summary>
        /// Get user trading status
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb840a5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXTradingStatus>>> GetTradingStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin position mode
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000088-77b7-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPositionMode>> GetIsolatedMarginPositionModeAsync(string marginAccount, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin position mode
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000090-77b7-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPositionMode>> GetCrossMarginPositionModeAsync(string marginAccount, CancellationToken ct = default);

    }
}