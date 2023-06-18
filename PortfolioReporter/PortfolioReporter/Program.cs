using PortfolioReporter;
using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using PortfolioReporter.Builders;
using PortfolioReporter.Models;
using PortfolioReporter.Extensions;
using PortfolioReporter.Reporter;
using log4net.Config;
using log4net;
using System.Reflection;
using PortfolioReporter.Utils;
using PortfolioReporter.Importer;

namespace PortfolioReporter
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string DOWNLOAD_PATH= @"C:\Users\Todd\Downloads";
        private const bool SendEmail = true;

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

            var acctImporter = new MintAssetsImporter();
            acctImporter.Import(portfolio, $"{DOWNLOAD_PATH}\\trends.csv");

            var rpt = new SummaryReport(portfolio);
            var html = rpt.GetHtml();

            if (SendEmail)
                EmailUtils.SendMail("Portfolio Reporter Summary",html);
        }
    }
}
