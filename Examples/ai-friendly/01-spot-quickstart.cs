// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated account lookup,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n HtxSpotQuickstart && cd HtxSpotQuickstart
//   dotnet add package JKorf.HTX.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below
//   dotnet run

using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create one per request.
var publicClient = new HTXRestClient();

var ticker = await publicClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
if (!ticker.Success)
{
    // .Error contains Code, Message, and error type details.
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

var closePrice = ticker.Data.ClosePrice;
if (closePrice == null)
{
    Console.WriteLine("Ticker did not include a close price.");
    return;
}

Console.WriteLine($"ETH/USDT close price: {closePrice}");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

// HTX spot trading needs an account id. Retrieve accounts first, then use the
// selected account id for balances and order placement.
var accounts = await tradingClient.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine($"Failed to get accounts: {accounts.Error}");
    return;
}

var spotAccount = accounts.Data.FirstOrDefault();
if (spotAccount == null)
{
    Console.WriteLine("No HTX account found for this API key.");
    return;
}

var balances = await tradingClient.SpotApi.Account.GetBalancesAsync(spotAccount.Id);
if (!balances.Success)
{
    Console.WriteLine($"Failed to get balances: {balances.Error}");
    return;
}

Console.WriteLine($"Account {spotAccount.Id} returned {balances.Data.Length} balance rows.");

// ---- 3. PLACE A LIMIT BUY ORDER ----
// HTX spot symbols are formatted without a separator, for example ETHUSDT.
// Keep clientOrderId null unless you need external correlation.
var safePrice = Math.Round(closePrice.Value * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    accountId: spotAccount.Id,
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.01m,
    price: safePrice);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed spot order {order.Data} at {safePrice}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync(order.Data);
if (status.Success)
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync(order.Data);
if (cancel.Success)
    Console.WriteLine($"Cancelled order {order.Data}");

// Common variations:
//   Market order:          type: OrderType.Market, omit price
//   Stop-limit order:      type: OrderType.StopLimit, add stopPrice and stopOperator
//   Open orders:           tradingClient.SpotApi.Trading.GetOpenOrdersAsync(accountId: spotAccount.Id)
//   Conditional orders:    tradingClient.SpotApi.Trading.PlaceConditionalOrderAsync(...)
//   Margin order:          tradingClient.SpotApi.Trading.PlaceMarginOrderAsync(...)
