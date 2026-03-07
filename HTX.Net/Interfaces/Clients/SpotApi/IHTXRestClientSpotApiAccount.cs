using HTX.Net.Enums;
using HTX.Net.Objects.Models;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// HTX account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IHTXRestClientSpotApiAccount
    {
        /// <summary>
        /// Get the user id associated with the apikey/secret
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52d6c-7773-11ed-9966-0242ac110003"/><br />
        /// Endpoint:<br />
        /// GET /v2/user/uid
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of accounts associated with the apikey/secret
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b291-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/account/accounts
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXAccount[]>> GetAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of balances for a specific account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b429-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/account/accounts/{accountId}/balance
        /// </para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for, account ids can be retrieved with <see cref="GetAccountsAsync">GetAccountsAsync</see>.</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXBalance[]>> GetBalancesAsync(long accountId, CancellationToken ct = default);

        /// <summary>
        /// Get platform asset valuation
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5058c-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/valuation
        /// </para>
        /// </summary>
        /// <param name="accountType">["<c>accountType</c>"] Filter by account type</param>
        /// <param name="valuationAsset">["<c>valuationCurrency</c>"] Valuation asset, only BTC supported at the moment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<HTXPlatformValuation>> GetPlatformValuationAsync(AccountType? accountType = null, string? valuationAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get the valuation of all assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ff6d-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/asset-valuation
        /// </para>
        /// </summary>
        /// <param name="accountType">["<c>accountType</c>"] Type of account to valuate</param>
        /// <param name="valuationAsset">["<c>valuationCurrency</c>"] The asset to get the value in</param>
        /// <param name="subUserId">["<c>subUid</c>"] The id of the sub user</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationAsset = null, long? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of balance changes of specified user's account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b85b-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/account/history
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>account-id</c>"] The id of the account to get the account history for, account ids can be retrieved with <see cref="GetAccountsAsync">GetAccountsAsync</see>.</param>
        /// <param name="asset">["<c>currency</c>"] Asset name, for example `ETH`</param>
        /// <param name="transactionTypes">["<c>transact-types</c>"] Blance change types</param>
        /// <param name="startTime">["<c>start-time</c>"] Far point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="endTime">["<c>end-time</c>"] Near point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="sort">["<c>sort</c>"] Sorting order (Ascending by default)</param>
        /// <param name="limit">["<c>size</c>"] Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXAccountHistory[]>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the balance changes of specified user's account.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec501f7-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/ledger
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>accountId</c>"] The id of the account to get the ledger for, account ids can be retrieved with <see cref="GetAccountsAsync">GetAccountsAsync</see>.</param>
        /// <param name="asset">["<c>currency</c>"] Asset name, for example `ETH`</param>
        /// <param name="transactionTypes">["<c>transactTypes</c>"] Blanace change types</param>
        /// <param name="startTime">["<c>startTime</c>"] Far point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="endTime">["<c>endTime</c>"] Near point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="sort">["<c>sort</c>"] Sorting order (Ascending by default)</param>
        /// <param name="limit">["<c>limit</c>"] Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="fromId">["<c>fromId</c>"] Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXLedgerEntry[]>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000096-77b7-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v2/account/transfer
        /// </para>
        /// </summary>
        /// <param name="fromAccount">["<c>from</c>"] Source account type</param>
        /// <param name="toAccount">["<c>to</c>"] Target account type</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="marginAccount">["<c>margin-account</c>"] Margin account. Use `USDT` for cross margin</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> TransferAsync(TransferAccount fromAccount, TransferAccount toAccount, string asset, decimal quantity, string marginAccount, CancellationToken ct = default);

        /// <summary>
        /// Get the deposit addresses for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50029-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXDepositAddress[]>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset from the account to an address
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4cc41-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/dw/withdraw/api/create
        /// </para>
        /// </summary>
        /// <param name="address">["<c>address</c>"] The desination address of this withdraw</param>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity of asset to withdraw</param>
        /// <param name="fee">["<c>fee</c>"] The fee to pay with this withdraw</param>
        /// <param name="network">["<c>chain</c>"] Set as "usdt" to withdraw USDT to OMNI, set as "trc20usdt" to withdraw USDT to TRX</param>
        /// <param name="addressTag">["<c>addr-tag</c>"] A tag specified for this address</param>
        /// <param name="clientOrderId">["<c>client-order-id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal/deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4f050-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/query/deposit-withdraw
        /// </para>
        /// </summary>
        /// <param name="type">["<c>type</c>"] Transfer type to search</param>
        /// <param name="asset">["<c>currency</c>"] The asset to withdraw, for example `ETH`</param>
        /// <param name="from">["<c>from</c>"] The transfer id to begin search</param>
        /// <param name="size">["<c>size</c>"] The number of items to return</param>
        /// <param name="direction">["<c>direct</c>"] the order of response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXWithdrawDeposit[]>> GetWithdrawDepositHistoryAsync(WithdrawDepositType type, string? asset = null, long? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Get current trading fees for symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51870-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/reference/transact-fee-rate
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter on symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXFeeRate[]>> GetTradingFeesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default);

        /// <summary>
        /// Get point balance
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec514e2-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/point/account
        /// </para>
        /// </summary>
        /// <param name="subUserId">["<c>subUid</c>"] Sub user id to request for</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPointBalance>> GetPointBalanceAsync(string? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer points to another user
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec515bf-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v2/point/transfer
        /// </para>
        /// </summary>
        /// <param name="fromUserId">["<c>fromUid</c>"] From user id</param>
        /// <param name="toUserId">["<c>toUid</c>"] To user id</param>
        /// <param name="groupId">["<c>groupId</c>"] Group id</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPointTransfer>> TransferPointsAsync(string fromUserId, string toUserId, string groupId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get user deduction info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c48b051" /><br />
        /// Endpoint:<br />
        /// GET /v1/account/switch/user/info
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXDeductInfo>> GetUserDeductionInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deduction assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c4cea32" /><br />
        /// Endpoint:<br />
        /// GET /v1/account/overview/info
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXDeductionAssets>> GetDeductAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Set deduction switch
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c4ff921" /><br />
        /// Endpoint:<br />
        /// POST /v1/account/fee/switch
        /// </para>
        /// </summary>
        /// <param name="switchType">["<c>switchType</c>"] Deduction switch type</param>
        /// <param name="deductionAsset">["<c>deductionCurrency</c>"] Asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetDeductionSwitchAsync(DeductionSwitchType switchType, string? deductionAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal quota
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50799-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/withdraw/quota
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXWithdrawalQuota>> GetWithdrawalQuotasAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50654-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/account/withdraw/address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>chain</c>"] Filter by network</param>
        /// <param name="note">["<c>note</c>"] Filter by note</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXWithdrawalAddress[]>> GetWithdrawalAddressesAsync(string asset, string? network = null, string? note = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get a withdrawal by client order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4f198-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v1/query/withdraw/client-order-id
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] The client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXWithdrawDeposit>> GetWithdrawalByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a pending withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4cda7-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/dw/withdraw-virtual/{id}/cancel
        /// </para>
        /// </summary>
        /// <param name="id">The withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> CancelWithdrawalAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Get API key info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52c92-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// GET /v2/user/api-key
        /// </para>
        /// </summary>
        /// <param name="userId">["<c>uid</c>"] User id</param>
        /// <param name="apiKey">["<c>accessKey</c>"] The API key</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXApiKeyInfo[]>> GetApiKeyInfoAsync(long userId, string? apiKey = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b9db-7773-11ed-9966-0242ac110003" /><br />
        /// Endpoint:<br />
        /// POST /v1/account/transfer
        /// </para>
        /// </summary>
        /// <param name="fromUserId">["<c>from-user</c>"] From user id</param>
        /// <param name="fromAccountType">["<c>from-account-type</c>"] From account type</param>
        /// <param name="fromAccountId">["<c>from-account</c>"] From account id</param>
        /// <param name="toUserId">["<c>to-user</c>"] To user id</param>
        /// <param name="toAccountType">["<c>to-account-type</c>"] To account type</param>
        /// <param name="toAccountId">["<c>to-account</c>"] To account id</param>
        /// <param name="asset">["<c>currency</c>"] Asset to transfer, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Amount to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTransactionResult>> InternalTransferAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default);

    }
}
