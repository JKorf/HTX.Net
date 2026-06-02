using HTX.Net.Clients;

// REST
var restClient = new HTXRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETH-USDT: {ticker.Data.ClosePrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new HTXSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ethusdt", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.ClosePrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
