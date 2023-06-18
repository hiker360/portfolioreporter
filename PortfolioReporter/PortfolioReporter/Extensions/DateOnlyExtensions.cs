using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Extensions
{
    internal static class DateOnlyExtensions
    {

        public static String FormatSQLDate(this DateOnly value)
        {
            return value.ToString("yyyy/MM/dd");
        }
    }
}
