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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8243c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_sub_account_list
        /// </para>
        /// </summary>
        /// <param name="marginAccount">["<c>margin_account</c>"] Margin account, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginSubAccountAssets[]>> GetCrossMarginAssetsAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin sub account assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb822f5-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_sub_account_list
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXIsolatedMarginSubAccountAssets[]>> GetIsolatedMarginAssetsAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get master sub account transfer records
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83c2c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_master_sub_transfer_record
        /// </para>
        /// </summary>
        /// <param name="marginAccount">["<c>margin_account</c>"] Margin account, for example `USDT`</param>
        /// <param name="daysInHistory">["<c>create_date</c>"] Days in history</param>
        /// <param name="type">["<c>transfer_type</c>"] Filter by type</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXMasterSubTransferPage>> GetMasterSubTransferRecordsAsync(string marginAccount, int daysInHistory, MasterSubTransferType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Set sub account trading permissions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb81ffc-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_sub_auth
        /// </para>
        /// </summary>
        /// <param name="subAccountUids">["<c>sub_uid</c>"] Uids of the subaccounts</param>
        /// <param name="enabled">["<c>sub_auth</c>"] Enable trading</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSubAccountResult>> SetTradingPermissionsAsync(IEnumerable<string> subAccountUids, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Transfer between master and sub account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb83b3e-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_master_sub_transfer
        /// </para>
        /// </summary>
        /// <param name="subUid">["<c>sub_uid</c>"] Sub account uid</param>
        /// <param name="asset">["<c>asset</c>"] Asset to transfer, for example `ETH`</param>
        /// <param name="fromMarginAccount">["<c>from_margin_account</c>"] From account</param>
        /// <param name="toMarginAccount">["<c>to_margin_account</c>"] To account</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="type">["<c>type</c>"] Type</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSwapOrderId>> TransferMasterSubAsync(string subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal quantity, MasterSubTransferType type, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get sub accounts trade permissions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18d119ebd6b" /><br />
        /// Endpoint:<br />
        /// GET /linear-swap-api/v1/swap_sub_auth_list
        /// </para>
        /// </summary>
        /// <param name="subAccountUids">["<c>sub_uid</c>"] Filter by sub user ids</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="filterDirection">["<c>direction</c>"] Filter direction</param>
        /// <param name="fromId">["<c>from_id</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubTradePermissions>> GetTradePermissionsAsync(IEnumerable<string>? subAccountUids = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? filterDirection = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin asset information
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb822f5-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_sub_account_info_list
        /// </para>
        /// </summary>
        /// <param name="contractCode">["<c>contract_code</c>"] Contract code, for example `ETH-USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubAccountAssetInfoPage>> GetIsolatedMarginAssetInfoAsync(string? contractCode = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin asset info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb8243c-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_sub_account_info_list
        /// </para>
        /// </summary>
        /// <param name="marginAccount">["<c>margin_account</c>"] Margin account, for example `USDT`</param>
        /// <param name="page">["<c>page_index</c>"] Page</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubAccountCrossAssetInfoPage>> GetCrossMarginAssetInfoAsync(string? marginAccount = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb827d0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_sub_position_info
        /// </para>
        /// </summary>
        /// <param name="subUserId">["<c>sub_uid</c>"] Sub user id</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPosition[]>> GetIsolatedMarginPositionsAsync(long subUserId, string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb827d0-77b5-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /linear-swap-api/v1/swap_cross_sub_position_info
        /// </para>
        /// </summary>
        /// <param name="subUserId">["<c>sub_uid</c>"] Sub user id</param>
        /// <param name="contractCode">["<c>contract_code</c>"] Filter by contract code, for example `ETH-USDT`</param>
        /// <param name="pair">["<c>pair</c>"] Filter by pair, for example `ETH-USDT`</param>
        /// <param name="contractType">["<c>contract_type</c>"] Filter by contract type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXCrossPosition[]>> GetCrossMarginPositionsAsync(long subUserId, string? contractCode = null, string? pair = null, ContractType? contractType = null, CancellationToken ct = default);

    }
}
