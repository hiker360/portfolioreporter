using PortfolioReporter;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using PortfolioReporter.Builders;
using PortfolioReporter.Models;
using PortfolioReporter.Extensions;
using PortfolioReporter.Reporter;

namespace PortfolioReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var portfolioBuilder = new PortfolioBuilder();
            var portfolio = portfolioBuilder.Build();

            //var acct = portfolio.GetAccount("Fundrise - TB IRA");
            //Console.WriteLine(acct.ReturnsPctYTD.FormatPercentage());
            //Console.WriteLine(acct.ReturnsPctMTD.FormatPercentage());
            //Console.WriteLine(acct.ReturnsPct1Year.FormatPercentage());
            //Console.WriteLine(acct.ReturnsPct6Month.FormatPercentage());
            //Console.WriteLine(acct.ReturnsPct3Month.FormatPercentage());
            //Console.WriteLine(acct.ReturnsPct1Month.FormatPercentage());

            var rpt = new SummaryReport(portfolio);
            var html = rpt.GetHtml();


        }
    }
}
