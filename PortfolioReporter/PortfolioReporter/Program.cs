using PortfolioReporter;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using PortfolioReporter.Builders;
using PortfolioReporter.Models;

namespace PortfolioReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var portfolioBuilder = new PortfolioBuilder();
            var portfolio = portfolioBuilder.Build();



        }
    }
}
