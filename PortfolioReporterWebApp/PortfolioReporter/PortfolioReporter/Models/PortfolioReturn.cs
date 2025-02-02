public class PortfolioReturn
{
    public string AccountName { get; set; }  // Ensures accounts have a name
    public string Group { get; set; }
    public string AccountType { get; set; } // Add this field
    public decimal Balance { get; set; }
    public decimal Return1M { get; set; }
    public decimal Return3M { get; set; }
    public decimal Return6M { get; set; }
    public decimal ReturnYTD { get; set; }
    public decimal Return1Y { get; set; }
}
