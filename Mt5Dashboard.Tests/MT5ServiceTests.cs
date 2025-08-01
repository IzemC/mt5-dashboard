using System.Text.Json;
using Moq;
using Microsoft.Extensions.Logging;
using Mt5Dashboard.Services;

namespace Mt5Dashboard.Tests;

public class MT5ServiceTests
{
    private readonly Mock<HttpClient> _httpClientMock = new();
    private readonly Mock<ILogger<MT5Service>> _loggerMock = new();
    private readonly MT5Service _service;

    public MT5ServiceTests()
    {
        _service = new MT5Service(_httpClientMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAccountDetails_ReturnsValidAccount()
    {
        var accountId = "12345";

        var result = await _service.GetAccountDetails(accountId);

        Assert.NotNull(result);
        Assert.Equal(accountId, result.AccountId);
        Assert.Equal(10000.50m, result.Balance);
        Assert.Equal(9700.25m, result.Equity);
        Assert.Equal(325.67m, result.MarginLevel);
        Assert.Equal("Active", result.Status);
    }

    [Fact]
    public async Task GetAccountTrades_ReturnsValidTrades()
    {
        var accountId = "12345";

        var result = await _service.GetAccountTrades(accountId);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        var firstTrade = result.First();
        Assert.Equal("10001", firstTrade.Ticket);
        Assert.Equal("EURUSD", firstTrade.Symbol);
        Assert.Equal(1.0m, firstTrade.Volume);
        Assert.Equal(100.20m, firstTrade.Profit);
    }
}
