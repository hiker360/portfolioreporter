using Microsoft.AspNetCore.Mvc;

public class HistoryController : Controller
{
    private readonly PortfolioCalculationService _portfolioCalculationService;

    public HistoryController(PortfolioCalculationService portfolioCalculationService)
    {
        _portfolioCalculationService = portfolioCalculationService;
    }

    public IActionResult Index()
    {
        var historyData = _portfolioCalculationService.GetHistoricalReturns();
        return View(historyData);
    }
}
