using PortfolioReporter.Utils;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PortfolioReporter.Models;
using PortfolioReporter.Utils;
using System.Data.SqlTypes;

namespace PortfolioReporter.Builders
{
    internal class BuilderSql :Sql 
    {
        public static IList<Account> GetAccounts()
        {
            using OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string sql = "SELECT Account, Group, SubGroup, LatestBalance FROM vAccounts";

            using OleDbCommand command = new OleDbCommand(sql, connection);
            using OleDbDataReader reader = command.ExecuteReader();

            var accounts = new List<Account>();
            while (reader.Read())
            {
                var acct = new Account()
                {
                    Name = OleDbUtils.GetValue<string>(reader["Account"]),
                    Group = OleDbUtils.GetValue<string>(reader["Group"]),
                    SubGroup = OleDbUtils.GetValue<string>(reader["SubGroup"]),
                    LatestBalance = (decimal) OleDbUtils.GetValue<decimal>(reader["LatestBalance"]),
                };
                accounts.Add(acct);
            }

            return accounts.AsReadOnly();

        }

        public static IList<PeriodBalance> GetPeriodBalances(string accountName)
        {
            using OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string sql = "SELECT Account, PeriodEnding, AccountBalance, PeriodInvestment FROM Balances where Account=@account Order by PeriodEnding";

            using OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbUtils.AddParameterValue(command, "account", System.Data.SqlDbType.Text, accountName);

            using OleDbDataReader reader = command.ExecuteReader();

            var balances = new List<PeriodBalance>();
            while (reader.Read())
            {
                var balance = new PeriodBalance()
                {
                    AccountName = OleDbUtils.GetValue<string>(reader["Account"]),
                    AccountBalance= OleDbUtils.GetValue<decimal>(reader["AccountBalance"]),
                    PeriodEndingDate = OleDbUtils.GetValue<DateTime>(reader["PeriodEnding"]),
                    Investment = OleDbUtils.GetValue<decimal>(reader["PeriodInvestment"]),
                };

                balances.Add(balance);
            }

            return balances.AsReadOnly();

        }

    }
}
