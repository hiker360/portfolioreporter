using System;
using System.Collections.Generic;
using System.Linq;

public class PortfolioCalculationService
{
    private readonly List<Account> _accounts;
    private readonly List<Balance> _balances;

    public PortfolioCalculationService(List<Account> accounts, List<Balance> balances)
    {
        _accounts = accounts;
        _balances = balances;
    }

    public Dictionary<string, PortfolioReturn> GetPortfolioReturns()
    {
        var portfolioReturns = new Dictionary<string, PortfolioReturn>();
        var latestDate = _balances.Max(b => b.BalanceDate);

        foreach (var account in _accounts)
        {
            var balanceRecords = _balances
                .Where(b => b.Account == account.Name)
                .OrderBy(b => b.BalanceDate)
                .ToList();

            if (balanceRecords.Count > 1)
            {
                var latestBalance = balanceRecords.Last();
                var previousBalance = balanceRecords.First();
                var netCashFlow = balanceRecords.Sum(b => b.CashIn - b.CashOut);

                var return1M = CalculateReturn(balanceRecords, latestDate, 30);
                var return3M = CalculateReturn(balanceRecords, latestDate, 90);
                var return6M = CalculateReturn(balanceRecords, latestDate, 180);
                var returnYTD = CalculateReturn(balanceRecords, latestDate, latestDate.DayOfYear);
                var return1Y = CalculateReturn(balanceRecords, latestDate, 365);

                portfolioReturns[account.Name] = new PortfolioReturn
                {
                    AccountName = account.Name,
                    Group = account.Group,
                    AccountType = account.AccountType, // Ensure this is assigned!
                    Balance = latestBalance.BalanceAmount,
                    Return1M = return1M,
                    Return3M = return3M,
                    Return6M = return6M,
                    ReturnYTD = returnYTD,
                    Return1Y = return1Y
                };
            }
            else
            {
                portfolioReturns[account.Name] = new PortfolioReturn
                {
                    Balance = 0,
                    Return1M = 0,
                    Return3M = 0,
                    Return6M = 0,
                    ReturnYTD = 0,
                    Return1Y = 0,
                    Group = account.Group
                };
            }
        }

        return portfolioReturns;
    }

    // Helper function to calculate returns based on past periods
    private decimal CalculateReturn(List<Balance> balances, DateTime latestDate, int daysBack)
    {
        var startDate = latestDate.AddDays(-daysBack);
        var startBalance = balances.OrderByDescending(b => b.BalanceDate)
                                   .FirstOrDefault(b => b.BalanceDate <= startDate);

        if (startBalance != null && startBalance.BalanceAmount > 0)
        {
            var latestBalance = balances.Last();
            var netCashFlow = balances.Where(b => b.BalanceDate > startBalance.BalanceDate)
                                      .Sum(b => b.CashIn - b.CashOut);
            return (latestBalance.BalanceAmount - startBalance.BalanceAmount - netCashFlow) / startBalance.BalanceAmount;
        }

        return 0;
    }
    public Dictionary<string, List<decimal>> GetHistoricalReturns()
    {
        var historicalReturns = new Dictionary<string, List<decimal>>();
        var groupedBalances = _balances.GroupBy(b => b.Account);

        foreach (var accountGroup in groupedBalances)
        {
            var accountName = accountGroup.Key;
            var sortedBalances = accountGroup.OrderBy(b => b.BalanceDate).ToList();
            var historicalData = new List<decimal>();

            if (sortedBalances.Count > 1)
            {
                for (int i = 1; i < sortedBalances.Count; i++)
                {
                    var previousBalance = sortedBalances[i - 1];
                    var currentBalance = sortedBalances[i];

                    var netCashFlow = currentBalance.CashIn - currentBalance.CashOut;
                    var returnValue = (currentBalance.BalanceAmount - previousBalance.BalanceAmount - netCashFlow) / previousBalance.BalanceAmount;

                    historicalData.Add(returnValue);
                }
            }
            else
            {
                historicalData.Add(0); // Default return if not enough data
            }

            historicalReturns[accountName] = historicalData;
        }

        return historicalReturns;
    }

}
