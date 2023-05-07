using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioReporter.Extensions;

namespace PortfolioReporter.Models
{
    internal class PeriodBalance
    {
        public string AccountName { get; set; } = "";
        public DateTime PeriodEndingDate { get; set; }
        public decimal MarketValue { get; set; } = 0m;
        public decimal Investment { get; set; } = 0m;

        public override string ToString()
        {
            return $"[PeriodBalance] Date={PeriodEndingDate.FormatDate()}; AccountBalance={MarketValue.FormatCurrency()}; Investment={Investment.FormatCurrency()}";
        }
    }
}
