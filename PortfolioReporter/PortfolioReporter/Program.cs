using PortfolioReporter;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using PortfolioReporter.Builders;
using PortfolioReporter.Models;
using PortfolioReporter.Extensions;
using PortfolioReporter.Reporter;
using log4net.Config;
using log4net;
using System.Reflection;
using PortfolioReporter.Utils;

namespace PortfolioReporter
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

            log.Debug("Debug message");
            log.Info("Info message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

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

            EmailUtils.SendMail("Portfolio Reporter Summary",html);
        }
    }
}
