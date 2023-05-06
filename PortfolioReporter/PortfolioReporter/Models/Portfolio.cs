using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Models
{
    internal class Portfolio
    {
        public Dictionary<string, Account> Accounts = new();
        public Account GetAccount(string name)
        {
            return Accounts[name];

        }

    }
}
