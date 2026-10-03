var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient("backend", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendApi:BaseUrl"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/portfolio/{clientId}/summary", async (string clientId, IHttpClientFactory httpClientFactory) =>
{
    var backend = httpClientFactory.CreateClient("backend");

    var clientResponse = await backend.GetAsync($"api/clients/{clientId}/accounts");

    if (!clientResponse.IsSuccessStatusCode)
    {
        return Results.StatusCode((int)clientResponse.StatusCode);
    }

    var client = await clientResponse.Content.ReadFromJsonAsync<Client>();

    if (client is null)
    {
        return Results.NotFound();
    }

    var holdings = await Task.WhenAll(client.Accounts.Select(async account =>
    {
        var response = await backend.GetAsync($"api/accounts/{account.AccountId}/holdings");

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<AccountHoldings>()
            : null;
    }));

    var topHoldings = holdings
        .Where(a => a is not null)
        .SelectMany(a => a!.Holdings)
        .OrderByDescending(h => h.MarketValue)
        .Take(3)
        .Select(h => new TopHolding(h.AssetName, h.MarketValue))
        .ToArray();

    return Results.Ok(new PortfolioSummary(
        client.ClientFullName,
        client.Accounts.Sum(a => a.Balance),
        client.Accounts.Count(a => a.Status == "Active"),
        topHoldings));
});

app.Run();

record Client(string ClientId, string ClientFullName, Account[] Accounts);

record Account(string AccountId, string Status, decimal Balance);

record AccountHoldings(string AccountId, Holding[] Holdings);

record Holding(string AssetName, decimal MarketValue);

record PortfolioSummary(string ClientName, decimal TotalValue, int ActiveAccountCount, TopHolding[] TopHoldings);

record TopHolding(string Name, decimal Value);
