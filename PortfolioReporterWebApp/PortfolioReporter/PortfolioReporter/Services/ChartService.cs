using System;
using System.Collections.Generic;
using System.Linq;

public class ChartService
{
    private readonly PortfolioCalculationService _portfolioCalculationService;

    public ChartService(PortfolioCalculationService portfolioCalculationService)
    {
        _portfolioCalculationService = portfolioCalculationService;
    }

    public Dictionary<string, Dictionary<string, decimal>> GetGraphData()
    {
        var returns = _portfolioCalculationService.GetPortfolioReturns();

        // Organizing data for Chart.js
        var chartData = new Dictionary<string, Dictionary<string, decimal>>();

        foreach (var account in returns)
        {
            chartData[account.Key] = new Dictionary<string, decimal>
            {
                { "1M", account.Value.Return1M },
                { "3M", account.Value.Return3M },
                { "6M", account.Value.Return6M },
                { "YTD", account.Value.ReturnYTD },
                { "1Y", account.Value.Return1Y }
            };
        }

        return chartData;
    }
}
