using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;

const string spotSymbol = "ETHUSDT";
const string futuresContract = "ETH-USDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";

Console.WriteLine("HTX.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(HTXRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.SpotApi.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    if (ticker.Data.ClosePrice == null)
    {
        Console.WriteLine("Spot ticker did not include a close price.");
        return;
    }

    var accounts = await client.SpotApi.Account.GetAccountsAsync();
    if (!accounts.Success)
    {
        Console.WriteLine($"Failed to get accounts: {accounts.Error}");
        return;
    }

    var spotAccount = accounts.Data.FirstOrDefault(d => d.Type == AccountType.Spot);
    if (spotAccount == null)
    {
        Console.WriteLine("No spot account found for these API credentials.");
        return;
    }

    var safePrice = Math.Round(ticker.Data.ClosePrice.Value * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        accountId: spotAccount.Id,
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(order.Data);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(order.Data);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(HTXRestClient client)
{
    Console.WriteLine($"Placing USDT futures reduce-only limit sell order for {futuresContract}...");

    var ticker = await client.UsdtFuturesApi.ExchangeData.GetTickerAsync(futuresContract);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    if (ticker.Data.ClosePrice == null)
    {
        Console.WriteLine("Futures ticker did not include a close price.");
        return;
    }

    var safePrice = Math.Round(ticker.Data.ClosePrice.Value * 1.05m, 2);
    var order = await client.UsdtFuturesApi.Trading.PlaceIsolatedMarginOrderAsync(
        contractCode: futuresContract,
        side: OrderSide.Sell,
        quantity: 1,
        leverageRate: 5,
        orderPriceType: OrderPriceType.Limit,
        price: safePrice,
        offset: Offset.Close,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId}");

    var orderStatus = await client.UsdtFuturesApi.Trading.GetIsolatedMarginOrderAsync(futuresContract, orderId: order.Data.OrderId);
    if (orderStatus.Success)
    {
        var status = orderStatus.Data.FirstOrDefault();
        Console.WriteLine(status == null
            ? "Futures order status was not returned by the exchange."
            : $"Futures order status: {status.Status}, executed: {status.QuantityFilled}");
    }
    else
    {
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");
    }

    var cancel = await client.UsdtFuturesApi.Trading.CancelIsolatedMarginOrderAsync(futuresContract, orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
