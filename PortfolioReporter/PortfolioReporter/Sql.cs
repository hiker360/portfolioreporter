using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioReporter.Utils;

namespace PortfolioReporter
{
    internal class Sql
    {
        internal const string dbPath = @"C:\Users\Todd\SynologyDrive\PortfolioReporter\PortfolioReporter.accdb";
        internal  const string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

    }
}
