using HTX.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Bitget services
builder.Services.AddHTX();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddHTX(options =>
{    
   options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
   options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoints and inject the HTX rest client
app.MapGet("/{Symbol}", async ([FromServices] IHTXRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.MapGet("/Balances", async ([FromServices] IHTXRestClient client) =>
{
    var account = await client.SpotApi.Account.GetAccountsAsync();
    var result = await client.SpotApi.Account.GetBalancesAsync(account.Data.Single(d => d.Type == HTX.Net.Enums.AccountType.Spot).Id);
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();