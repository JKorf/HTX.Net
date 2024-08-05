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
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52d6c-7773-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b291-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXAccount>>> GetAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b429-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default);

        /// <summary>
        /// Get platform asset valuation
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5058c-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="valuationAsset">Valuation asset, only BTC supported at the moment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<HTXPlatformValuation>> GetPlatformValuationAsync(AccountType? accountType = null, string? valuationAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the valuation of all assets
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4ff6d-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountType">Type of account to valuate</param>
        /// <param name="valuationCurrency">The currency to get the value in</param>
        /// <param name="subUserId">The id of the sub user</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balance changes of specified user's account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b85b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blance change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="limit">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// This endpoint returns the balance changes of specified user's account.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec501f7-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the ledger for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blanace change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="limit">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=10000096-77b7-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="fromAccount">Source account type</param>
        /// <param name="toAccount">Target account type</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="marginAccount">Margin account. Use `USDT` for cross margin</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> TransferAsync(TransferAccount fromAccount, TransferAccount toAccount, string asset, decimal quantity, string marginAccount, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user could query deposit address of corresponding chain, for a specific crypto currency (except IOTA).
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50029-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Parent user creates a withdraw request from spot account to an external address (exists in your withdraw address list), which doesn't require two-factor-authentication.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4cc41-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="address">The desination address of this withdraw</param>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">The quantity of asset to withdraw</param>
        /// <param name="fee">The fee to pay with this withdraw</param>
        /// <param name="network">Set as "usdt" to withdraw USDT to OMNI, set as "trc20usdt" to withdraw USDT to TRX</param>
        /// <param name="addressTag">A tag specified for this address</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user searche for all existed withdraws and deposits and return their latest status.
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4f050-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="type">Define transfer type to search</param>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="from">The transfer id to begin search</param>
        /// <param name="size">The number of items to return</param>
        /// <param name="direction">the order of response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Get current trading fees
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51870-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXFeeRate>>> GetTradingFeesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default);

        /// <summary>
        /// Get point balance
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec514e2-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id to request for</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPointBalance>> GetPointBalanceAsync(string? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer points to another user
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec515bf-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="fromUserId">From user id</param>
        /// <param name="toUserId">To user id</param>
        /// <param name="groupId">Group id</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXPointTransfer>> TransferPointsAsync(string fromUserId, string toUserId, string groupId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get user deduction info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c48b051" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXDeductInfo>> GetUserDeductionInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deduction assets
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c4cea32" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXDeductionAssets>> GetDeductAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Set deduction switch
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=8cb89359-77b5-11ed-9966-18f7c4ff921" /></para>
        /// </summary>
        /// <param name="switchType">Deduction switch type</param>
        /// <param name="deductionAsset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetDeductionSwitchAsync(DeductionSwitchType switchType, string? deductionAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal quota
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50799-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">Filter by asse</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXWithdrawalQuota>> GetWithdrawalQuotasAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal addresses
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec50654-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="network">Filter by network</param>
        /// <param name="note">Filter by note</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXWithdrawalAddress>>> GetWithdrawalAddressesAsync(string asset, string? network = null, string? note = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get a withdrawal by client order id
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4f198-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXWithdrawDeposit>> GetWithdrawalByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a pending withdrawal
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4cda7-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="id">The withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> CancelWithdrawalAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Get API key info
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52c92-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXApiKeyInfo>>> GetApiKeyInfoAsync(long userId, string ? apiKey = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets between accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b9db-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="fromUserId">From user id</param>
        /// <param name="fromAccountType">From account type</param>
        /// <param name="fromAccountId">From account id</param>
        /// <param name="toUserId">To user id</param>
        /// <param name="toAccountType">To account type</param>
        /// <param name="toAccountId">To account id</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Amount to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXTransactionResult>> InternalTransferAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default);

    }
}
