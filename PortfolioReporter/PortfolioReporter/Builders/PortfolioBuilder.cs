using PortfolioReporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Builders
{
    internal class PortfolioBuilder
    {
        public Portfolio Build()
        {

            var portfolio = new Portfolio();

            var accounts = BuilderSql.GetAccounts();
            foreach (var account in accounts)
            {
                var accountBuilder = new AccountBuilder();
                accountBuilder.Build(account);

                portfolio.Accounts.Add(account.Name, account);
            }

            return portfolio;

        }

    }
}
