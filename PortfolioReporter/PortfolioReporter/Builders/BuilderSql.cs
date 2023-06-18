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
using System.Text.Json;

namespace PortfolioReporter.Builders
{
    internal class ImporterSql :Sql 
    {
        public static IList<Account> GetAccounts()
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT Account, `Group`, SubGroup, IsBenchmark FROM Accounts";

            using MySqlCommand  command = new MySqlCommand (sql, connection);
            using MySqlDataReader  reader = command.ExecuteReader();

            var accounts = new List<Account>();
            while (reader.Read())
            {
                var acct = new Account()
                {
                    Name = MySqlDbUtils.GetValue<string>(reader["Account"]),
                    Group = MySqlDbUtils.GetValue<string>(reader["Group"]),
                    SubGroup = MySqlDbUtils.GetValue<string>(reader["SubGroup"]),
                    IsBenchmark = MySqlDbUtils.GetValue<bool>(reader["IsBenchmark"]),
                };


                accounts.Add(acct);
            }

            return accounts.AsReadOnly();

        }

        public static IList<PeriodBalance> GetPeriodBalances(string accountName)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT Account, PeriodEnding, MarketValue, PeriodInvestment FROM Balances where Account=@account Order by PeriodEnding";

            using MySqlCommand  command = new MySqlCommand (sql, connection);
            command.Parameters.AddWithValue("account", accountName);

            using MySqlDataReader  reader = command.ExecuteReader();

            var balances = new List<PeriodBalance>();
            while (reader.Read())
            {
                var balance = new PeriodBalance()
                {
                    AccountName = MySqlDbUtils.GetValue<string>(reader["Account"]),
                    MarketValue = MySqlDbUtils.GetValue<decimal>(reader["MarketValue"]),
                    PeriodEndingDate = MySqlDbUtils.GetValue<DateTime>(reader["PeriodEnding"]),
                    Investment = MySqlDbUtils.GetValue<decimal>(reader["PeriodInvestment"]),
                };

                balances.Add(balance);
            }

            return balances.AsReadOnly();

        }

    }
}
