public class PortfolioGraphViewModel
{
    public Dictionary<string, Dictionary<string, decimal>> PortfolioReturns { get; set; } = new();
    public Dictionary<string, string> AccountGroups { get; set; } = new(); // Maps Account -> Group
}
