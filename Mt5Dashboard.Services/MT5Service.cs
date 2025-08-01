using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mt5Dashboard.Core.Models;
using Mt5Dashboard.Services.Interfaces;

namespace Mt5Dashboard.Services;

public class MT5Service : IMT5Service
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MT5Service> _logger;

    public MT5Service(HttpClient httpClient, ILogger<MT5Service> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Account> GetAccountDetails(string accountId)
    {
        try
        {
            // In a real application, this would be an actual API call
            // var response = await _httpClient.GetAsync($"/api/mt5/accounts/{accountId}");
            await Task.Delay(50);
            // Mock response for demonstration
            var mockJson = $$"""
            {
                "accountId": "{{accountId}}",
                "balance": 10000.50,
                "equity": 9700.25,
                "marginLevel": 325.67,
                "lastLogin": "2025-07-21T14:10:00Z",
                "status": "Active",
                "currency": "USD"
            }
            """;
            
            var account = JsonSerializer.Deserialize<Account>(mockJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return account ?? throw new InvalidOperationException("Failed to deserialize account details");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching account details for account {AccountId}", accountId);
            throw;
        }
    }

    public async Task<IEnumerable<Trade>> GetAccountTrades(string accountId)
    {
        try
        {
            // In a real application, this would be an actual API call
            // var response = await _httpClient.GetAsync($"/api/mt5/accounts/{accountId}/trades");
            await Task.Delay(50);
            // Mock response for demonstration
            var mockJson = """
            [
                {
                    "ticket": "10001",
                    "symbol": "EURUSD",
                    "volume": 1.0,
                    "profit": 100.20
                },
                {
                    "ticket": "10002",
                    "symbol": "GBPUSD",
                    "volume": 0.5,
                    "profit": -45.10
                }
            ]
            """;
            
            var trades = JsonSerializer.Deserialize<IEnumerable<Trade>>(mockJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return trades ?? Enumerable.Empty<Trade>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching trades for account {AccountId}", accountId);
            throw;
        }
    }
}