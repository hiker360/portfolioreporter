using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioReporter.Models;

namespace PortfolioReporter.Builders
{
    internal class AccountBuilder
    {
        public void Build(Account account)
        {
            var periodBalances = ImporterSql.GetPeriodBalances(account.Name);
            foreach (var bal in periodBalances)
            {
                account.PeriodBalances.Add(bal);
            }

        }
    }
}
