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

    [Fact]
    public async Task GetAccountDetails_LogsError_WhenExceptionOccurs()
    {
        var accountId = "99999";
        var exception = new JsonException("Invalid JSON");
        
        var service = new MT5Service(new HttpClient(new MockHttpMessageHandler(exception)), _loggerMock.Object);
        
         var ex = await Assert.ThrowsAsync<JsonException>(() => service.GetAccountDetails(accountId));
        
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Error fetching account details for account {accountId}")),
                ex,
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }
}

public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Exception _exceptionToThrow;

    public MockHttpMessageHandler(Exception exceptionToThrow)
    {
        _exceptionToThrow = exceptionToThrow;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        throw _exceptionToThrow;
    }
}