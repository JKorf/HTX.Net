using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;

namespace HTX.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// HTX usdt futures sub account endpoints
    /// </summary>
    public interface IHTXRestClientUsdtFuturesApiSubAccount
    {
        /// <summary>
        /// Get cross margin sub account assets
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8243c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetCrossMarginAssetsAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin sub account assets
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb822f5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXIsolatedMarginSubAccountAssets>>> GetIsolatedMarginAssetsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get master sub account transfer records
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83c2c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="daysInHistory">Days in history</param>
        /// <param name="type">Filter by type</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMasterSubTransferPage>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Set sub account trading permissions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81ffc-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subAccountUids">Uids of the subaccounts</param>
        /// <param name="enabled">Enable trading</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSubAccountResult>> SetTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Transfer between master and sub account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83b3e-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUid">Sub account uid</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="fromMarginAccount">From account</param>
        /// <param name="toMarginAccount">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="type">Type</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get sub accounts trade permissions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18d119ebd6b" /></para>
        /// </summary>
        /// <param name="subAccountUids">Filter by sub user ids</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="filterDirection">Filter direction</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubTradePermissions>> GetTradePermissionsAsync(IEnumerable<string>? subAccountUids = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin asset information
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb822f5-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubAccountAssetInfoPage>> GetIsolatedMarginAssetInfoAsync(string? contractCode = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin asset info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8243c-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubAccountCrossAssetInfoPage>> GetCrossMarginAssetInfoAsync(string? marginAccount = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin positions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb827d0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="contractCode">Filter by contract code</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXPosition>>> GetIsolatedMarginPositionsAsync(long subUserId, string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin positions
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb827d0-77b5-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="contractCode">Filter by contract code</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="contractType">Filter by contract type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXCrossPosition>>> GetCrossMarginPositionsAsync(long subUserId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

    }
}