// 02-futures.cs
//
// Demonstrates: USDT futures cross margin - set leverage, place market order,
// retrieve position, close position.
//
// Setup: dotnet add package JKorf.HTX.Net
// Substitute API_KEY / API_SECRET. The API key must have futures permissions.

using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;

var client = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

const string contractCode = "ETH-USDT";

// ---- 1. SET CROSS-MARGIN LEVERAGE ----
// HTX USDT futures has explicit isolated/cross methods. Pick the one that
// matches the account mode and order method you will use.
var leverage = await client.UsdtFuturesApi.Trading.SetCrossMarginLeverageAsync(
    leverageRate: 5,
    contractCode: contractCode);

if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}

Console.WriteLine($"Leverage updated for {contractCode}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// quantity is contract quantity for HTX futures. Use Offset.Open to open and
// Offset.Close/reduceOnly when closing.
var openOrder = await client.UsdtFuturesApi.Trading.PlaceCrossMarginOrderAsync(
    quantity: 1,
    side: OrderSide.Buy,
    leverageRate: 5,
    orderPriceType: OrderPriceType.Market,
    contractCode: contractCode,
    offset: Offset.Open);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open futures position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened futures order: {openOrder.Data.OrderId}");

// ---- 3. GET CURRENT POSITIONS ----
var positions = await client.UsdtFuturesApi.Account.GetCrossMarginPositionsAsync(contractCode);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get futures positions: {positions.Error}");
    return;
}

Console.WriteLine($"Received {positions.Data.Length} cross-margin position rows.");

// ---- 4. CLOSE THE POSITION ----
// CloseCrossMarginPositionAsync closes using HTX's lightning close endpoint.
var closeOrder = await client.UsdtFuturesApi.Trading.CloseCrossMarginPositionAsync(
    direction: OrderSide.Sell,
    contractCode: contractCode,
    orderPriceType: LightningPriceType.Market);

if (!closeOrder.Success)
{
    Console.WriteLine($"Failed to close position: {closeOrder.Error}");
    return;
}

Console.WriteLine($"Close request accepted: {closeOrder.Data}");

// Common variations:
//   Isolated margin:       SetIsolatedMarginLeverageAsync + PlaceIsolatedMarginOrderAsync
//   Limit order:           orderPriceType: OrderPriceType.Limit, add price
//   Reduce only close:     PlaceCrossMarginOrderAsync(..., offset: Offset.Close, reduceOnly: true)
//   Trigger order:         PlaceCrossMarginTriggerOrderAsync(...)
//   TP/SL order:           SetCrossMarginTpSlAsync(...)
