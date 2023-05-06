using PortfolioReporter.Extensions;
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
        public decimal LatestBalance { get; set; } = 0m;

        public List<PeriodBalance> PeriodBalances { get; private set; } = new ();

        public override string ToString()
        {
            return $"[Account] Name={Name}; LatestBalance={LatestBalance.FormatCurrency()}";
        }

    }
}
