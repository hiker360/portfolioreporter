using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using PortfolioReporter.Extensions;
using PortfolioReporter.Models;

namespace PortfolioReporter.Importer
{
    internal class MintAssetsImporter
    {
        public void Import(Portfolio portfolio, String mintTrendsFile)
        {
            TextFieldParser parser = new TextFieldParser(mintTrendsFile) ?? throw new NullReferenceException();

            parser.Delimiters = new string[] { "," };
            parser.HasFieldsEnclosedInQuotes = true;
            bool foundStart = false;

            ImporterSql.DeleteBalances();
            while (true)
            {
                string[] parts = parser.ReadFields() ?? throw new NullReferenceException();

                if (!foundStart)
                {
                    if (parts.Length > 1 && parts[0] == "Account")
                        foundStart = true;

                    continue;

                }

                var accountName = parts[0];
                var marketValue = parts[1].ParseMoneyToDecimal();

                // Detect end of file
                if (accountName == "Total")
                    break;


                if (!portfolio.HasAccount(accountName))
                    continue;

                ImporterSql.UpdateAccountBalance(accountName, marketValue);

            }
        }
    }
}
