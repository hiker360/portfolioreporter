using PortfolioReporter.Utils;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PortfolioReporter.Models;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace PortfolioReporter.Importer
{
    internal class ImporterSql :Sql 
    {
        public static void DeleteBalances()
        {
            using OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM Balances WHERE PeriodEnding=Date()";

            using OleDbCommand cmd = new OleDbCommand(sql, connection);

            cmd.ExecuteNonQuery();

        }
        public static void UpdateAccountBalance(string accountName, decimal marketValue)
        {
            using OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string sql = "Insert into Balances (Account, PeriodEnding, EquityBalance) values (@accountName, Date(), @marketValue)";

            using OleDbCommand cmd = new OleDbCommand(sql, connection);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            cmd.Parameters.AddWithValue("@marketValue", marketValue);
            cmd.ExecuteNonQuery();
        }
    }
}
