﻿using PortfolioReporter.Extensions;
using PortfolioReporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Models
{
    internal class Account
    {
        public string Name { get; set; } = "";
        public string Group { get; set; } = "";
        public string SubGroup { get; set; } = "";
        public decimal MarketValue
        {
            get
            {
                if (!HasBalances)
                    return 0;
                else
                    return PeriodBalances.Last().MarketValue;
            }
        }

        public bool HasBalances => PeriodBalances.Count > 0;
        public bool IsBenchmark { get; set; }

        public DateTime AsOfDate => PeriodBalances.Last().PeriodEndingDate;

        public List<PeriodBalance> PeriodBalances { get; private set; } = new ();

        public override string ToString()
        {
            return $"[Account] Name={Name}; LatestBalance={MarketValue.FormatCurrency()}";
        }

        public decimal GetReturnsAmount(DateTime fromDate)
        {
            if (!HasBalances)
                return 0;

            var costBasis = GetCostBasis(fromDate);
            var amt = CalcUtils.CalcReturnAmount(costBasis, MarketValue);
            return amt;
        }

        public double GetReturnsPercent(DateTime fromDate)
        {
            if (!HasBalances)
                return 0;

            var costBasis = GetCostBasis(fromDate);
            var rtnPct = CalcUtils.CalcReturnPercent(costBasis, MarketValue);
            return (double) rtnPct;
        }

        public decimal GetCostBasis(DateTime fromDateTime)
        {
            if (!HasBalances)
                return 0;

            var idx = GetNearestPeriodBalanceIndex(fromDateTime);
            if (idx<0)
            {
                Console.WriteLine($"Unable to determine cost bases for {Name}");
                return 0;
            }

            var startPerBal = PeriodBalances[idx];

            var totInvestment = 0m;
            for (int i = idx; i < PeriodBalances.Count; i++)
            {
                var perBal = PeriodBalances[i];

                // Don't count investment on the start balance
                if (i>idx)
                    totInvestment += perBal.Investment;
            }

            var costBasis = startPerBal.MarketValue + totInvestment;
            return costBasis;

        }

        private int GetNearestPeriodBalanceIndex(DateTime targetDate)
        {
            if (PeriodBalances.Count == 0)
            {
                throw new ArgumentException("List of dates must not be null or empty");
            }

            // If there is only a single balance, return it.
            if (PeriodBalances.Count == 1)
                return 0;

            // Loop through the list of dates and find the index of the first date that is not after the target date
            for (int i = 0; i < PeriodBalances.Count; i++)
            {
                if (PeriodBalances[i].PeriodEndingDate > targetDate)
                {
                    if (i == 0)
                        return 0;
                    else
                        return i - 1;
                }
            }

            // If no date was found that is not after the target date, return the index of the last date in the list
            return PeriodBalances.Count - 1;
        }


        public double ReturnsPctYTD => GetReturnsPercent(new DateTime(DateTime.Today.Year, 1, 1));
        public double ReturnsPctMTD => GetReturnsPercent(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        public double ReturnsPct1Year => GetReturnsPercent(DateTime.Today.AddYears(-1));
        public double ReturnsPct6Month => GetReturnsPercent(DateTime.Today.AddMonths(-6));
        public double ReturnsPct3Month => GetReturnsPercent(DateTime.Today.AddMonths(-3));
        public double ReturnsPct1Month => GetReturnsPercent(DateTime.Today.AddMonths(-1));

        public decimal ReturnsAmtYTD => GetReturnsAmount(new DateTime(DateTime.Today.Year, 1, 1));
        public decimal ReturnsAmtMTD => GetReturnsAmount(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        public decimal ReturnsAmt1Year => GetReturnsAmount(DateTime.Today.AddYears(-1));
        public decimal ReturnsAmt6Month => GetReturnsAmount(DateTime.Today.AddMonths(-6));
        public decimal ReturnsAmt3Month => GetReturnsAmount(DateTime.Today.AddMonths(-3));
        public decimal ReturnsAmt1Month => GetReturnsAmount(DateTime.Today.AddMonths(-1));



    }
}
