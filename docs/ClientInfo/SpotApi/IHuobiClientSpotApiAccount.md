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

## GetCrossLoanInterestRateAndQuotaAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-cross](https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-cross)  
<p>

*Get cross margin interest rates and quotas*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetCrossLoanInterestRateAndQuotaAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiLoanInfoAsset>>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct||

</p>

***

## GetCrossMarginBalanceAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-cross](https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-cross)  
<p>

*Get cross margin account balance*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetCrossMarginBalanceAsync();  
```  

```csharp  
Task<WebCallResult<HuobiMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subUserId|Sub user id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginClosedOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-cross](https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-cross)  
<p>

*Get cross margin order history*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetCrossMarginClosedOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetCrossMarginClosedOrdersAsync(string? asset = default, MarginOrderStatus? state = default, DateTime? startDate = default, DateTime? endDate = default, string? from = default, FilterDirection? direction = default, int? limit = default, int? subUserId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ state|Filter by state|
|_[Optional]_ startDate|Filter by start date|
|_[Optional]_ endDate|Filter by end date|
|_[Optional]_ from|Start order id for use in combination with direction|
|_[Optional]_ direction|Direction of results in combination with from parameter|
|_[Optional]_ limit|Max amount of results|
|_[Optional]_ subUserId|Sub user id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCurrentFeeRatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-current-fee-rate-applied-to-the-user](https://huobiapi.github.io/docs/spot/v1/en/#get-current-fee-rate-applied-to-the-user)  
<p>

*Get Current Fee Rate Applied to The User*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetCurrentFeeRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiFeeRate>>> GetCurrentFeeRatesAsync(IEnumerable<string> symbols, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|Filter on symbols|
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

## GetIsolatedLoanInterestRateAndQuotaAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-isolated](https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-isolated)  
<p>

*Get isolated loan interest rate and quotas*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetIsolatedLoanInterestRateAndQuotaAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiLoanInfo>>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols|Filter on symbols|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginBalanceAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-isolated](https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-isolated)  
<p>

*Get isolated margin account balance*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginBalanceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiMarginBalances>>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ subUserId|Sub user id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginClosedOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-isolated](https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-isolated)  
<p>

*Get isolated margin orders history*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginClosedOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(string symbol, IEnumerable<MarginOrderStatus>? states = default, DateTime? startDate = default, DateTime? endDate = default, string? from = default, FilterDirection? direction = default, int? limit = default, int? subUserId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get history for|
|_[Optional]_ states|Filter by states|
|_[Optional]_ startDate|Filter by start date|
|_[Optional]_ endDate|Filter by end date|
|_[Optional]_ from|Start order id for use in combination with direction|
|_[Optional]_ direction|Direction of results in combination with from parameter|
|_[Optional]_ limit|Max amount of results|
|_[Optional]_ subUserId|Sub user id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRepaymentHistoryAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#repayment-record-reference](https://huobiapi.github.io/docs/spot/v1/en/#repayment-record-reference)  
<p>

*Get repayment history*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetRepaymentHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiRepayment>>> GetRepaymentHistoryAsync(long? repayId = default, long? accountId = default, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, string? sort = default, int? limit = default, long? fromId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ repayId|Filter by repay id|
|_[Optional]_ accountId|Filter by account id|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ startTime|Only show records after this|
|_[Optional]_ endTime|Only show records before this|
|_[Optional]_ sort|Sort direction|
|_[Optional]_ limit|Result limit|
|_[Optional]_ fromId|Search id|
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

## GetSubAccountUsersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-list](https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-list)  
<p>

*Gets a list of users associated with the apikey/secret*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetSubAccountUsersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiUser>>> GetSubAccountUsersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubUserAccountsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-account-list](https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-account-list)  
<p>

*Gets a list of sub-user accounts associated with the sub-user id*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetSubUserAccountsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subUserId|The if of the user to get accounts for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserIdAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-uid](https://huobiapi.github.io/docs/spot/v1/en/#get-uid)  
<p>

*Get the user id associated with the apikey/secret*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.GetUserIdAsync();  
```  

```csharp  
Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
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
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayCrossMarginLoanAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross](https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross)  
<p>

*Repay a isolated margin loan*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.RepayCrossMarginLoanAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id to repay|
|quantity|Quantity|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayIsolatedMarginLoanAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-isolated](https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-isolated)  
<p>

*Repay a isolated margin loan*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.RepayIsolatedMarginLoanAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id to repay|
|quantity|Quantity|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayMarginLoanAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross-isolated](https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross-isolated)  
<p>

*Repay a margin loan*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.RepayMarginLoanAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiRepaymentResult>>> RepayMarginLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|Account id|
|asset|Asset to repay|
|quantity|Quantity to repay|
|_[Optional]_ transactionId|Loan transaction ID|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RequestCrossMarginLoanAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-cross](https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-cross)  
<p>

*Request a loan on cross margin*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.RequestCrossMarginLoanAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset|
|quantity|The quantity|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RequestIsolatedMarginLoanAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-isolated](https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-isolated)  
<p>

*Request a loan on isolated margin*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.RequestIsolatedMarginLoanAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|asset|The asset|
|quantity|The quantity|
|_[Optional]_ ct|Cancellation token|

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

## TransferCrossMarginToSpotAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-cross-margin-account-to-spot-trading-account-cross](https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-cross-margin-account-to-spot-trading-account-cross)  
<p>

*Transfer from cross margin account to spot account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferCrossMarginToSpotAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to transfer|
|quantity|Quantity to transfer|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferIsolatedMarginToSpotAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-isolated-margin-account-to-spot-trading-account-isolated](https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-isolated-margin-account-to-spot-trading-account-isolated)  
<p>

*Transfer asset from isolated margin to spot account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferIsolatedMarginToSpotAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Trading symbol|
|asset|Asset to transfer|
|quantity|Quantity to transfer|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSpotToCrossMarginAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-cross-margin-account-cross](https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-cross-margin-account-cross)  
<p>

*Transfer from spot account to cross margin account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferSpotToCrossMarginAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to transfer|
|quantity|Quantity to transfer|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSpotToIsolatedMarginAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-isolated-margin-account-isolated](https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-isolated-margin-account-isolated)  
<p>

*Transfer asset from spot account to isolated margin account*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Account.TransferSpotToIsolatedMarginAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Trading symbol|
|asset|Asset to transfer|
|quantity|Quantity to transfer|
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
|_[Optional]_ ct|Cancellation token|

</p>
