using HTX.Net;
using HTX.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the HTX services
builder.Services.AddHTX();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddHTX(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IHTXRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data.ClosePrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.MapGet("/Balances", async ([FromServices] IHTXRestClient client) =>
{
    var account = await client.SpotApi.Account.GetAccountsAsync();
    if (!account.Success)
        return Results.Problem(account.Error?.Message, statusCode: 502);

    var spotAccount = account.Data.FirstOrDefault(d => d.Type == HTX.Net.Enums.AccountType.Spot);
    if (spotAccount == null)
        return Results.Problem("No spot account found for the configured API credentials.", statusCode: 404);

    var result = await client.SpotApi.Account.GetBalancesAsync(spotAccount.Id);
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
