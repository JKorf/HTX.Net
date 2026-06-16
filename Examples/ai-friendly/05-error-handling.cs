// 05-error-handling.cs
//
// Demonstrates: HttpResult patterns, retry logic, common error scenarios.
//
// Setup: dotnet add package JKorf.HTX.Net

using CryptoExchange.Net.Objects;
using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;

var client = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

// ---- 1. THE BASIC PATTERN ----
// Every REST method returns HttpResult<T> or HttpResult.
// Every socket subscription or socket request returns WebSocketResult<T> or WebSocketResult.
// .Data is only valid when .Success is true.

var result = await client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.ClosePrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only transient errors such as network glitches, rate limits, or server errors.
// Do not retry validation, signature, or insufficient-balance errors blindly.

async Task<HttpResult<T>> WithRetry<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success)
            return last;

        if (last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"));

if (!ticker.Success)
    Console.WriteLine($"Ticker still failed: {ticker.Error}");

// ---- 3. COMMON HTX ERROR SCENARIOS ----
//
// Authentication/signature errors:
//   Check that options.ApiCredentials uses HTXCredentials and the correct
//   exchange environment. Do not retry until credentials are fixed.
//
// Missing account id:
//   Spot order and balance methods require an account id. Call
//   SpotApi.Account.GetAccountsAsync() first and use the returned account id.
//
// Invalid symbol format:
//   Spot symbols use ETHUSDT. USDT futures contract codes use ETH-USDT.
//
// Invalid futures margin mode:
//   HTX futures has separate isolated/cross margin methods. Do not call an
//   isolated method when the user asked for cross margin, or the reverse.
//
// Validation / insufficient balance:
//   Usually permanent. Surface the error to the caller with the original code
//   and message instead of retrying in a loop.

// ---- 4. ORDER PLACEMENT WITH RESULT CHECKS ----
var accounts = await client.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine($"Cannot fetch accounts: {accounts.Error}");
    return;
}

var account = accounts.Data.FirstOrDefault();
if (account == null)
{
    Console.WriteLine("No account available for order placement.");
    return;
}

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    accountId: account.Id,
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - retry with backoff if the operation is idempotent"
        : "Permanent - surface to user";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// HTX.Net returns API, validation, and network failures via result.Error.
// Exceptions are for cancellation, disposal, misconfiguration, or programming errors.

// Common variations:
//   Cancellation:           pass ct: cancellationToken to any method
//   Request timeout:        options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Futures public data:    client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH-USDT")
//   Socket errors:          check subscription.Success before subscription.Data
