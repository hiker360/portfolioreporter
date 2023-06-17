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

            var accounts = ImporterSql.GetAccounts();
            foreach (var account in accounts)
            {
                var accountBuilder = new AccountBuilder();
                accountBuilder.Build(account);

                portfolio.AddAccount(account);
            }

            return portfolio;

        }

    }
}
