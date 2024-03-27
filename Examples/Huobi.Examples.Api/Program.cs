using Huobi.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Bitget services
builder.Services.AddHuobi();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddHuobi(restOptions =>
{
    restOptions.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
    restOptions.RequestTimeout = TimeSpan.FromSeconds(5);
}, socketOptions =>
{
    socketOptions.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoints and inject the Huobi rest client
app.MapGet("/{Symbol}", async ([FromServices] IHuobiRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.MapGet("/Balances", async ([FromServices] IHuobiRestClient client) =>
{
    var account = await client.SpotApi.Account.GetAccountsAsync();
    var result = await client.SpotApi.Account.GetBalancesAsync(account.Data.Single(d => d.Type == Huobi.Net.Enums.AccountType.Spot).Id);
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();