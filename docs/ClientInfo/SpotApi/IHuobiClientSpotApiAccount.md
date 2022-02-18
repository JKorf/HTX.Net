---
title: IHuobiClientSpotApiAccount
has_children: false
parent: IHuobiClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiClient > SpotApi > Account`  
*Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## GetAccountHistoryAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-account-history](https://huobiapi.github.io/docs/spot/v1/en/#get-account-history)  
<p>

*Gets a list of balance changes of specified user's account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetAccountHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = default, IEnumerable<TransactionType>? transactionTypes = default, DateTime? startTime = default, DateTime? endTime = default, SortingType? sort = default, int? size = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The id of the account to get the balances for|
|_[Optional]_ asset|Asset name|
|_[Optional]_ transactionTypes|Blance change types|
|_[Optional]_ startTime|Far point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days|
|_[Optional]_ endTime|Near point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days|
|_[Optional]_ sort|Sorting order (Ascending by default)|
|_[Optional]_ size|Maximum number of items in each response (from 1 to 500, default is 100)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountLedgerAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-account-ledger](https://huobiapi.github.io/docs/spot/v1/en/#get-account-ledger)  
<p>

*This endpoint returns the balance changes of specified user's account.*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetAccountLedgerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = default, IEnumerable<TransactionType>? transactionTypes = default, DateTime? startTime = default, DateTime? endTime = default, SortingType? sort = default, int? size = default, long? fromId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The id of the account to get the ledger for|
|_[Optional]_ asset|Asset name|
|_[Optional]_ transactionTypes|Blanace change types|
|_[Optional]_ startTime|Far point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days|
|_[Optional]_ endTime|Near point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days|
|_[Optional]_ sort|Sorting order (Ascending by default)|
|_[Optional]_ size|Maximum number of items in each response (from 1 to 500, default is 100)|
|_[Optional]_ fromId|Only get orders with ID before or after this. Used together with the direction parameter|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-all-accounts-of-the-current-user](https://huobiapi.github.io/docs/spot/v1/en/#get-all-accounts-of-the-current-user)  
<p>

*Gets a list of accounts associated with the apikey/secret*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetAccountsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetValuationAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-total-valuation-of-platform-assets](https://huobiapi.github.io/docs/spot/v1/en/#get-the-total-valuation-of-platform-assets)  
<p>

*Gets the valuation of all assets*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetAssetValuationAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = default, long? subUserId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountType|Type of account to valuate|
|_[Optional]_ valuationCurrency|The currency to get the value in|
|_[Optional]_ subUserId|The id of the sub user|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBalancesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-specific-account](https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-specific-account)  
<p>

*Gets a list of balances for a specific account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetBalancesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The id of the account to get the balances for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositAddressesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#query-deposit-address](https://huobiapi.github.io/docs/spot/v1/en/#query-deposit-address)  
<p>

*Parent user and sub user could query deposit address of corresponding chain, for a specific crypto currency (except IOTA).*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetDepositAddressesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountBalancesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-sub-user](https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-sub-user)  
<p>

*Gets a list of balances for a specific sub account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetSubAccountBalancesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|The id of the sub account to get the balances for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawDepositAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-for-existed-withdraws-and-deposits](https://huobiapi.github.io/docs/spot/v1/en/#search-for-existed-withdraws-and-deposits)  
<p>

*Parent user and sub user searche for all existed withdraws and deposits and return their latest status.*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetWithdrawDepositAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = default, int? from = default, int? size = default, FilterDirection? direction = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|type|Define transfer type to search|
|_[Optional]_ asset|The asset to withdraw|
|_[Optional]_ from|The transfer id to begin search|
|_[Optional]_ size|The number of items to return|
|_[Optional]_ direction|the order of response|
|_[Optional]_ ct||

</p>

***

## TransferAssetAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#asset-transfer](https://huobiapi.github.io/docs/spot/v1/en/#asset-transfer)  
<p>

*Transfer assets between accounts*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferAssetAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiTransactionResult>> TransferAssetAsync(long fromUserId, AccountType fromAccountType, long fromAccountId, long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|fromUserId|From user id|
|fromAccountType|From account type|
|fromAccountId|From account id|
|toUserId|To user id|
|toAccountType|To account type|
|toAccountId|To account id|
|asset|Asset to transfer|
|quantity|Amount to transfer|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferWithSubAccountAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-between-parent-and-sub-account](https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-between-parent-and-sub-account)  
<p>

*Transfer asset between parent and sub account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferWithSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|The target sub account id to transfer to or from|
|asset|The asset to transfer|
|quantity|The quantity of asset to transfer|
|transferType|The type of transfer|
|_[Optional]_ ct|Cancellation token|

</p>

***

## WithdrawAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#create-a-withdraw-request](https://huobiapi.github.io/docs/spot/v1/en/#create-a-withdraw-request)  
<p>

*Parent user creates a withdraw request from spot account to an external address (exists in your withdraw address list), which doesn't require two-factor-authentication.*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.WithdrawAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = default, string? addressTag = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|address|The desination address of this withdraw|
|asset|Asset|
|quantity|The quantity of asset to withdraw|
|fee|The fee to pay with this withdraw|
|_[Optional]_ network|Set as "usdt" to withdraw USDT to OMNI, set as "trc20usdt" to withdraw USDT to TRX|
|_[Optional]_ addressTag|A tag specified for this address|
|_[Optional]_ ct||

</p>
