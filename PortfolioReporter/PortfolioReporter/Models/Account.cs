using PortfolioReporter.Extensions;
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
        public decimal MarketValue => PeriodBalances.Last().MarketValue;

        public DateTime AsOfDate => PeriodBalances.Last().PeriodEndingDate;

        public List<PeriodBalance> PeriodBalances { get; private set; } = new ();

        public override string ToString()
        {
            return $"[Account] Name={Name}; LatestBalance={MarketValue.FormatCurrency()}";
        }

        public decimal GetReturnsAmount(DateTime fromDate)
        {
            var costBasis = GetCostBasis(fromDate);
            var amt = CalcUtils.CalcReturnAmount(costBasis, MarketValue);
            return amt;
        }

        public double GetReturnsPercent(DateTime fromDate)
        {
            var costBasis = GetCostBasis(fromDate);
            var rtnPct = CalcUtils.CalcReturnPercent(costBasis, MarketValue);
            return (double) rtnPct;
        }

        public decimal GetCostBasis(DateTime fromDateTime)
        {
            var idx = GetNearestPeriodBalanceIndex(fromDateTime);
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


        public int GetNearestPeriodBalanceIndex(DateTime targetDate)
        {
            if (PeriodBalances.Count == 0)
            {
                throw new ArgumentException("List of dates must not be null or empty");
            }

            // Loop through the list of dates and find the index of the first date that is not after the target date
            for (int i = 0; i < PeriodBalances.Count; i++)
            {
                if (PeriodBalances[i].PeriodEndingDate > targetDate)
                {
                    return i-1;
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
