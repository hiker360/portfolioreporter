using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class GraphController : Controller
{
    private readonly PortfolioCalculationService _portfolioCalculationService;

    public GraphController(PortfolioCalculationService portfolioCalculationService)
    {
        _portfolioCalculationService = portfolioCalculationService;
    }

    public IActionResult Index()
    {
        var portfolioReturns = _portfolioCalculationService.GetPortfolioReturns();

        // Extract account group mappings (Account Name -> Group Name)
        var accountGroups = portfolioReturns.ToDictionary(entry => entry.Key, entry => entry.Value.Group);

        // Convert portfolio returns into the correct dictionary format for Chart.js
        var formattedReturns = portfolioReturns.ToDictionary(
            entry => entry.Key,
            entry => new Dictionary<string, decimal>
            {
                { "1M", entry.Value.Return1M },
                { "3M", entry.Value.Return3M },
                { "6M", entry.Value.Return6M },
                { "YTD", entry.Value.ReturnYTD },
                { "1Y", entry.Value.Return1Y },
                { "Balance", entry.Value.Balance } // Include balance for weighted aggregation
            }
        );

        // ✅ Ensure we pass the correct model
        var viewModel = new PortfolioGraphViewModel
        {
            PortfolioReturns = formattedReturns,
            AccountGroups = accountGroups
        };

        return View(viewModel); // ✅ This is the correct ViewModel
    }
}
