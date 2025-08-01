namespace Mt5Dashboard.Core.Models;

public class Trade
{
    public string Ticket { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public decimal Profit { get; set; }
}