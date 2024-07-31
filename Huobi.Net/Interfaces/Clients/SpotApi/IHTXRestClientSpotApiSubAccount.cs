using CryptoExchange.Net.Objects;
using HTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HTX.Net.Objects.Models;

namespace HTX.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// HTX sub-account endpoints.
    /// </summary>
    public interface IHTXRestClientSpotApiSubAccount
    {
        /// <summary>
        /// Set fee deduct mode for sub accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52497-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserIds">Sub user ids</param>
        /// <param name="deductMode">Deduct from account</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXSubDeductMode>>> SetDeductModeAsync(IEnumerable<string> subUserIds, DeductMode deductMode, CancellationToken ct = default);

        /// <summary>
        /// Create new sub accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52336-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="accounts">Accounts to create</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXSubAccountInfo>>> CreateSubAccountsAsync(IEnumerable<HTXSubAccountRequest> accounts, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of users associated with the apikey/secret
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52a87-7773-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXUser>>> GetSubUserListAsync(CancellationToken ct = default);

        /// <summary>
        /// Set (un)lock status on a sub account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52620-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="lockAction">Lock action</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubAccountLock>> SetLockAsync(long subUserId, LockAction lockAction, CancellationToken ct = default);

        /// <summary>
        /// Get sub user by id
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52b46-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXUser>> GetSubUserAsync(long subUserId, CancellationToken ct = default);

        /// <summary>
        /// Set tradable market for sub accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52859-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserIds">Sub user ids</param>
        /// <param name="accountType">Account type</param>
        /// <param name="enabled">Enabled or not</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXSubMarketTradable>>> SetTradableMarketAsync(IEnumerable<string> subUserIds, SubAccountMarketType accountType, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Set asset transfer permissions for sub accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec529c3-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserIds">Sub user ids</param>
        /// <param name="enabled">Enabled</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXSubTransferPermission>>> SetAssetTransferPermissionsAsync(IEnumerable<string> subUserIds, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub-user accounts associated with the sub-user id
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec51da3-7773-11ed-9966-0242ac110003"/></para>
        /// </summary>
        /// <param name="subUserId">The if of the user to get accounts for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HTXSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default);

        /// <summary>
        /// Create a new API key
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52185-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="otpToken">Two factor authentication code</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="note">Note</param>
        /// <param name="permissions">Permissions</param>
        /// <param name="ipAddresses">Ip addresses</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubApiKey>> CreateApiKeyAsync(string otpToken, long subUserId, string note, IEnumerable<string> permissions, IEnumerable<string> ipAddresses, CancellationToken ct = default);

        /// <summary>
        /// Edit an API key
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec52249-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="apiKey">Api key to edit</param>
        /// <param name="note">Note</param>
        /// <param name="permissions">Permissions</param>
        /// <param name="ipAddresses">Ip addresses</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HTXSubApiKeyEdit>> EditApiKeyAsync(long subUserId, string apiKey, string note, IEnumerable<string> permissions, IEnumerable<string> ipAddresses, CancellationToken ct = default);

        /// <summary>
        /// Delete an API key
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5208e-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="apiKey">Api key to remove</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> DeleteApiKeyAsync(long subUserId, string apiKey, CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for a sub account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5255a-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="asset">The asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXDepositAddress>>> GetDepositAddressAsync(long subUserId, string asset, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec5278c-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="sort">Sort</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="fromId">Return results after this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXSubDeposit>>> GetDepositHistoryAsync(long subUserId, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get aggregate balances of all sub accounts
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4fd28-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<HTXBalance>>> GetAggregateBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4b62b-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HTXBalance>>> GetBalancesAsync(long subAccountId, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between parent and sub account
        /// <para><a href="https://www.htx.com/en-us/opend/newApiPages/?id=7ec4feac-7773-11ed-9966-0242ac110003" /></para>
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Unique transfer id</returns>
        Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default);

    }
}
