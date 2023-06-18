using PortfolioReporter.Utils;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PortfolioReporter.Models;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using PortfolioReporter.Extensions;

namespace PortfolioReporter.Importer
{
    internal class ImporterSql :Sql 
    {
        public static void DeleteBalances()
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM Balances WHERE PeriodEnding=Date()";

            using MySqlCommand  cmd = new MySqlCommand (sql, connection);

            cmd.ExecuteNonQuery();

        }
        public static void UpdateAccountBalance(string accountName, DateOnly periodEnding, decimal marketValue)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "Insert into Balances (Account, PeriodEnding, EquityBalance) values (@accountName, @periodEnding, @marketValue)" +
                " ON DUPLICATE KEY UPDATE EquityBalance=@marketValue";

            using MySqlCommand  cmd = new MySqlCommand (sql, connection);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            cmd.Parameters.AddWithValue("@marketValue", marketValue);
            cmd.Parameters.AddWithValue("@periodEnding", periodEnding.FormatSQLDate());
            cmd.ExecuteNonQuery();
        }
    }
}
