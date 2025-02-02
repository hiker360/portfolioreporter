using System.Collections.Generic;

public class Portfolio
{
    public List<Account> Accounts { get; set; }
    public List<Balance> Balances { get; set; }

    public Portfolio()
    {
        Accounts = new List<Account>();
        Balances = new List<Balance>();
    }
}
