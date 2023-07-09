using PortfolioReporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Models
{
    internal class Portfolio
    {
        private readonly Dictionary<string, Account> _accounts = new();

        public List<Account> Accounts {
            get
            {
                var list = new List<Account>();
                foreach (var acct in _accounts.Values)
                {
                    if (acct.IsBenchmark)
                        continue;

                    list.Add(acct);
                }
                return list;
            }

        }

        public List<Account> Benchmarks
        {
            get
            {
                var list = new List<Account>();
                foreach (var acct in _accounts.Values)
                {
                    if (!acct.IsBenchmark)
                        continue;

                    list.Add(acct);
                }
                return list;
            }

        }


        public Account GetAccount(string name)
        {
            if (!_accounts.ContainsKey(name))
                throw new Exception($"Account {name} does not exist");

            return _accounts[name];

        }

        public void AddAccount(Account acct)
        {
            _accounts.Add(acct.Name, acct);
        }

        public List<Account> GetAccountsForGroup(string group)
        {
            var list = new List<Account>();
            foreach (var acct in _accounts.Values)
            {
                if (acct.Group == group)
                    list.Add(acct);
            }
            return list;
        }

        public List<Account> GetAccountsForGroup(string group, string subGroup)
        {
            var list = new List<Account>();
            foreach (var acct in _accounts.Values)
            {
                if (acct.Group == group && acct.SubGroup == subGroup)
                    list.Add(acct);
            }
            return list;
        }

        public decimal MarketValue
        {
            get
            {
                var marketValue = 0m;
                foreach (var acct in Accounts)
                {
                    marketValue += acct.MarketValue;
                }
                return marketValue;

            }
        }

        public double GetReturnsPercent(DateTime fromDateTime)
        {
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in Accounts)
            {
                costBasis += acct.GetCostBasis(fromDateTime);
                marketValue += acct.MarketValue;
            }

            var rtnPct = CalcUtils.CalcReturnPercent(costBasis, marketValue);
            return (double)rtnPct;

        }

        public double GetAAR()
        {
            var periodBegin = new DateTime(DateTime.Today.Year, 1, 1);
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in Accounts)
            {
                costBasis += acct.GetCostBasis(periodBegin);
                marketValue += acct.MarketValue;
            }

            var periodEnd = DateTime.Today;

            var aar = CalcUtils.AnnualizeReturns(periodBegin, periodEnd, costBasis, marketValue);
            return aar;

        }

        public double GetAARForGroup(String group)
        {
            var periodBegin = new DateTime(DateTime.Today.Year, 1, 1);
            var accts = GetAccountsForGroup(group);
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in accts)
            {
                costBasis += acct.GetCostBasis(periodBegin);
                marketValue += acct.MarketValue;
            }

            var periodEnd = DateTime.Today;

            var aar = CalcUtils.AnnualizeReturns(periodBegin, periodEnd, costBasis, marketValue);
            return aar;

        }

        public double GetAARForGroupSubGroup(String group, String subGroup)
        {
            var periodBegin = new DateTime(DateTime.Today.Year, 1, 1);

            var accts = GetAccountsForGroup(group, subGroup);
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in accts)
            {
                costBasis += acct.GetCostBasis(periodBegin);
                marketValue += acct.MarketValue;
            }

            var periodEnd = DateTime.Today;

            var aar = CalcUtils.AnnualizeReturns(periodBegin, periodEnd, costBasis, marketValue);
            return aar;

        }


        public double GetReturnsPercentForGroup (String group, DateTime fromDateTime)
        {
            var accts = GetAccountsForGroup(group);
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in accts)
            {
                costBasis += acct.GetCostBasis(fromDateTime);
                marketValue += acct.MarketValue;
            }

            var rtnPct = CalcUtils.CalcReturnPercent(costBasis, marketValue);
            return (double) rtnPct;

        }

        public double GetReturnsPercentForGroupSubGroup(String group, String subGroup, DateTime fromDateTime)
        {
            var accts = GetAccountsForGroup(group, subGroup);
            var costBasis = 0m;
            var marketValue = 0m;

            foreach (var acct in accts)
            {
                costBasis += acct.GetCostBasis(fromDateTime);
                marketValue += acct.MarketValue;
            }

            var rtnPct = CalcUtils.CalcReturnPercent(costBasis, marketValue);
            return (double)rtnPct;

        }

        public bool HasAccount(string acctName) => _accounts.ContainsKey(acctName);

        // Get returns for portfolio
        public double ReturnsPctYTD()
        {
            return GetReturnsPercent(new DateTime(DateTime.Today.Year, 1, 1));
        }

        public double ReturnsPctMTD()
        {
            return GetReturnsPercent(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        }
        public double ReturnsPct1Year()
        {
            return GetReturnsPercent(DateTime.Today.AddYears(-1));
        }
        public double ReturnsPct6Month()
        {
            return GetReturnsPercent(DateTime.Today.AddMonths(-6));
        }
        public double ReturnsPct3Month()
        {
            return GetReturnsPercent(DateTime.Today.AddMonths(-3));
        }
        public double ReturnsPct1Month()
        {
            return GetReturnsPercent(DateTime.Today.AddMonths(-1));
        }


        // Get returns for group
        public double ReturnsPctYTD(string group)
        {
            return GetReturnsPercentForGroup(group, new DateTime(DateTime.Today.Year, 1, 1));
        }

        public double ReturnsPctMTD(string group)
        {
            return GetReturnsPercentForGroup(group, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        }
        public double ReturnsPct1Year(string group)
        {
            return GetReturnsPercentForGroup(group, DateTime.Today.AddYears(-1));
        }
        public double ReturnsPct6Month(string group)
        {
            return GetReturnsPercentForGroup(group, DateTime.Today.AddMonths(-6));
        }
        public double ReturnsPct3Month(string group)
        {
            return GetReturnsPercentForGroup(group, DateTime.Today.AddMonths(-3));
        }
        public double ReturnsPct1Month(string group)
        {
            return GetReturnsPercentForGroup(group, DateTime.Today.AddMonths(-1));
        }

        public double ReturnsPctYTD(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, new DateTime(DateTime.Today.Year, 1, 1));
        }

        public double ReturnsPctMTD(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        }
        public double ReturnsPct1Year(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, DateTime.Today.AddYears(-1));
        }
        public double ReturnsPct6Month(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, DateTime.Today.AddMonths(-6));
        }
        public double ReturnsPct3Month(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, DateTime.Today.AddMonths(-3));
        }
        public double ReturnsPct1Month(string group, string subGroup)
        {
            return GetReturnsPercentForGroupSubGroup(group, subGroup, DateTime.Today.AddMonths(-1));
        }

    }
}
