using PortfolioReporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioReporter.Utils;
using PortfolioReporter.Extensions;
using System.Collections;

namespace PortfolioReporter.Reporter
{
    internal class SummaryReport : IComparer<Account>
    {
        private Portfolio _portfolio;
        public SummaryReport(Portfolio portfolio)
        {
            _portfolio = portfolio;
        }

        public string GetHtml()
        {
            var sb = HtmlUtils.BuildReportHeader("Portfolio Summary");
            sb += "<div>Report run on " + DateTime.Today.ToShortDateString() + "</div>";

            sb += BuildPeriodReturns();
            sb += BuildAccountBalances();

            sb += HtmlUtils.BuildReportFooter();
            return sb;
        }

        public int Compare(Account? x, Account? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            else if (x == null)
            {
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }
            else
            {
                return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            }
        }
        private string BuildAccountBalances()
        {
            var sb = "<h2>Account Balances</h2>";

            sb += StartAccountBalancesTable();
            var accounts = _portfolio.Accounts;
            accounts.Sort(this);

            foreach (var account in accounts)
            {
                //if (!account.IsActive) continue;
                //if (account.IsBenchmark) continue;

                sb += BuildAccountBalanceTableRow(account.Name, account.Group, account.SubGroup, account.MarketValue);
            }
            sb += BuildAccountBalanceTableRow("<b>Total Balance</b>", "","", _portfolio.MarketValue);
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
            sb += BuildPeriodReturnTableRow("Core", "Crypto");
            sb += BuildPeriodReturnTableRow("Core", "401k");
            sb += BuildPeriodReturnTableRow("Opportunity");
            sb += BuildPeriodReturnTableRow("Gold");
            sb += BuildPeriodReturnTableRow("Cash");
            sb += BuildPeriodReturnTableRow("Cash", "Money Market");
            sb += BuildPeriodReturnTableRow("Cash", "Cash");
            sb += BuildPeriodReturnTableRow("Cash", "Bank");
            sb += BuildPortfolioPeriodReturnTableRow();

            sb += BuildPeriodReturnTableRow("Markets");
            sb += BuildPeriodReturnTableRow("Markets", "S&P");
            sb += HtmlUtils.EndTable();
            sb += "</p>";

            return sb;

        }
        private String BuildAccountBalanceTableRow(string accountName, string group, string subgroup, decimal balance)
        {

            var msg = "<tr>";

            var balFormatted = balance.FormatCurrency();

            msg += HtmlUtils.BuildTableCell(accountName);
            msg += HtmlUtils.BuildTableCell(group);
            msg += HtmlUtils.BuildTableCell(subgroup);
            msg += HtmlUtils.BuildNumberCell(balFormatted);

            msg += "</tr>";
            return msg;
        }

        private String BuildPortfolioPeriodReturnTableRow()
        {
            var msg = "<tr>";
            msg += HtmlUtils.BuildTableCell("Portfolio");

            double periodRtn;

            // YTD
            periodRtn = _portfolio.ReturnsPctYTD();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // MTD
            periodRtn = _portfolio.ReturnsPctMTD();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Year
            periodRtn = _portfolio.ReturnsPct1Year();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 6 Months
            periodRtn = _portfolio.ReturnsPct6Month();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 3 Months
            periodRtn = _portfolio.ReturnsPct3Month();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // 1 Month
            periodRtn = _portfolio.ReturnsPct1Month();
            msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

            // AAR
            var aar = _portfolio.GetAAR();
            msg += HtmlUtils.BuildNumberCell(aar.FormatPercentage());

            msg += "</tr>";
            return msg;
        }

        //private String BuildAccountPeriodReturnTableRow(string acctName, bool indent)
        //{
        //    var acct = _portfolio.GetAccount(acctName);

        //    var msg = "<tr>";
        //    if (indent)
        //        msg += HtmlUtils.BuildTableCell($"&nbsp;&nbsp;&nbsp;{acctName}");
        //    else
        //        msg += HtmlUtils.BuildTableCell(acctName);

        //    double periodRtn;

        //    // YTD
        //    periodRtn = acct.ReturnsPctYTD;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    // MTD
        //    periodRtn = acct.ReturnsPctMTD;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    // 1 Year
        //    periodRtn = acct.ReturnsPct1Year;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    // 6 Months
        //    periodRtn = acct.ReturnsPct6Month;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    // 3 Months
        //    periodRtn = acct.ReturnsPct3Month;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    // 1 Month
        //    periodRtn = acct.ReturnsPct1Month;
        //    msg += HtmlUtils.BuildNumberCell(periodRtn.FormatPercentage());

        //    msg += "</tr>";
        //    return msg;
        //}


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

            // AAR
            var aar = _portfolio.GetAARForGroup(groupName);
            msg += HtmlUtils.BuildNumberCell(aar.FormatPercentage());

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

            // AAR
            var aar = _portfolio.GetAARForGroupSubGroup(groupName, subGroupName);
            msg += HtmlUtils.BuildNumberCell(aar.FormatPercentage());

            msg += "</tr>";
            return msg;
        }


        private String StartAccountBalancesTable()
        {
            var msg = "<table>";
            msg += "<tr>";
            msg += HtmlUtils.BuildTableHeader("Account");
            msg += HtmlUtils.BuildTableHeader("Group");
            msg += HtmlUtils.BuildTableHeader("Subgroup");
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
            msg += HtmlUtils.BuildTableHeader("Avg Annual Rtn");
            msg += "</tr>";

            return msg;
        }
    }
}
