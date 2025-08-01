using Microsoft.AspNetCore.Mvc;
using Mt5Dashboard.Core.Models;
using Mt5Dashboard.Services.Interfaces;

namespace Mt5Dashboard.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IMT5Service _mt5Service;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IMT5Service mt5Service, ILogger<DashboardController> logger)
    {
        _mt5Service = mt5Service;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string accountId = "12345")
    {
        try
        {
            var accountTask = _mt5Service.GetAccountDetails(accountId);
            var tradesTask = _mt5Service.GetAccountTrades(accountId);

            await Task.WhenAll(accountTask, tradesTask);

            var account = await accountTask;
            var trades = await tradesTask;

            account.OpenTradesCount = trades.Count();

            return View(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard for account {AccountId}", accountId);
            return View("Error", new ErrorViewModel { Message = "Failed to load account data. Please try again later." });
        }
    }
}