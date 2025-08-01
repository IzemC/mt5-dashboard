using Mt5Dashboard.Core.Models;

namespace Mt5Dashboard.Services.Interfaces;

public interface IMT5Service
{
    Task<Account> GetAccountDetails(string accountId);
    Task<IEnumerable<Trade>> GetAccountTrades(string accountId);
}