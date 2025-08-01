namespace Mt5Dashboard.Core.Models;

public class Account
{
    public string AccountId { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal Equity { get; set; }
    public decimal MarginLevel { get; set; }
    public DateTime LastLogin { get; set; }
    public string Status { get; set; } = string.Empty;
    public int OpenTradesCount { get; set; }
    public string Currency { get; set; } = "USD";
}