using PortfolioReporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioReporter.Utils;
using PortfolioReporter.Extensions;

namespace PortfolioReporter.Reporter
{
    internal class SummaryReport
    {
        private Portfolio _portfolio;
        public SummaryReport(Portfolio portfolio)
        {
            _portfolio = portfolio;
        }

        public string GetHtml()
        {
            var sb = HtmlUtils.BuildReportHeader("Portfolio Summary");

            sb += BuildPeriodReturns();
            sb += BuildAccountBalances();

            sb += HtmlUtils.BuildReportFooter();
            return sb;
        }


        private string BuildAccountBalances()
        {
            var sb = "<h2>Account Balances</h2>";

            sb += StartAccountBalancesTable();
            var accounts = _portfolio.Accounts;
            foreach (var account in accounts)
            {
                //if (!account.IsActive) continue;
                //if (account.IsBenchmark) continue;

                sb += BuildAccountBalanceTableRow(account.Name, account.MarketValue);
            }
            sb += BuildAccountBalanceTableRow("<b>Total Balance</b>", _portfolio.MarketValue);
            sb += HtmlUtils.EndTable();
            sb += "</p>";

            return sb;
        }

        private string BuildPeriodReturns()
        {

            var sb = "<h2>Period Returns</h2>";

            sb += StartPeriodReturnTable();
            sb += BuildPeriodReturnTableRow("Core");
            sb += BuildPeriodReturnTableRow("Core", "Logical Invest");
            sb += BuildPeriodReturnTableRow("Core", "Real Estate");
            sb += BuildPeriodReturnTableRow("Core", "TB 401(k)-2");
            sb += BuildPeriodReturnTableRow("Core", "Crypto Wallet");
            sb += BuildPeriodReturnTableRow("Opportunity");
            sb += BuildPeriodReturnTableRow("Gold");
            //sb += BuildPeriodReturnTableRow("Portfolio");
            //sb += BuildPeriodReturnTableRow("SPY");
            sb += HtmlUtils.EndTable();
            sb += "</p>";

            return sb;

        }
        private String BuildAccountBalanceTableRow(string accountName, decimal balance)
        {

            var msg = "<tr>";

            var balFormatted = balance.FormatCurrency();

            msg += HtmlUtils.BuildTableCell(accountName);
            msg += HtmlUtils.BuildNumberCell(balFormatted);

            msg += "</tr>";
            return msg;
        }

        private String BuildPeriodReturnTableRow(string groupName)
        {
            var msg = "<tr>";
            msg += HtmlUtils.BuildTableCell(groupName);

            double periodRtn;

            // YTD
            periodRtn = _portfolio.ReturnsPctYTD(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // MTD
            periodRtn = _portfolio.ReturnsPctMTD(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Year
            periodRtn = _portfolio.ReturnsPct1Year(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 6 Months
            periodRtn = _portfolio.ReturnsPct6Month(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 3 Months
            periodRtn = _portfolio.ReturnsPct3Month(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Month
            periodRtn = _portfolio.ReturnsPct1Month(groupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            msg += "</tr>";
            return msg;
        }

        private String BuildPeriodReturnTableRow(string groupName, string subGroupName)
        {
            var msg = "<tr>";
            msg += HtmlUtils.BuildTableCell("&nbsp;&nbsp;&nbsp;" + subGroupName);

            double periodRtn;

            // YTD
            periodRtn = _portfolio.ReturnsPctYTD(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // MTD
            periodRtn = _portfolio.ReturnsPctMTD(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Year
            periodRtn = _portfolio.ReturnsPct1Year(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 6 Months
            periodRtn = _portfolio.ReturnsPct6Month(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 3 Months
            periodRtn = _portfolio.ReturnsPct3Month(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Month
            periodRtn = _portfolio.ReturnsPct1Month(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            msg += "</tr>";
            return msg;
        }


        private String StartAccountBalancesTable()
        {
            var msg = "<table>";
            msg += "<tr>";
            msg += HtmlUtils.BuildTableHeader("Account");
            msg += HtmlUtils.BuildTableHeader("Balance");
            msg += "</tr>";

            return msg;
        }

        private String StartPeriodReturnTable()
        {
            var msg = "<table>";
            msg += "<tr>";
            msg += HtmlUtils.BuildTableHeader("Group");
            msg += HtmlUtils.BuildTableHeader("YTD");
            msg += HtmlUtils.BuildTableHeader("MTD");
            msg += HtmlUtils.BuildTableHeader("Year");
            msg += HtmlUtils.BuildTableHeader("6 Months");
            msg += HtmlUtils.BuildTableHeader("3 Months");
            msg += HtmlUtils.BuildTableHeader("1 Month");
            msg += "</tr>";

            return msg;
        }
    }
}
