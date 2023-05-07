using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Extensions
{
    internal static class StringExtensions
    {
        //var responseObj = JsonConvert.DeserializeObject<T>(json_data);
        public static double FromPercentageString(this string value)
        {
            return double.Parse(value.Replace("%", "")) / 100;
        }

        public static String SingleQuote(this String s)
        {
            return "'" + s + "'";
        }
        public static String FormatDouble(this double value)
        {
            return string.Format("{0:0,0.000}", value);
        }

        public static String FormatPercentage(this double value)
        {
            return string.Format("{0:0.00}%", value*100);
        }

        public static String FormatCurrency(this double value)
        {
            return FormatCurrency(Convert.ToDecimal(value));
        }

        public static String FormatCurrency(this decimal value)
        {
            //string fmt2 = "#,##0.00;(#,##0.00)";
            //return string.Format("${0:0,0.00}", value);
            return value.ToString("$#,##0.00;$(#,##0.00)");
        }

        public static String FormatDate(this DateTime value)
        {
            return value.ToString("MM-dd-yyyy");
        }

        public static String FormatDateForFile(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }


        public static String FormatDateTime(this DateTime value)
        {
            return value.ToString("MM-dd-yyyy hh:mm");
        }

        public static String FormatList<T>(this IList<T> list)
        {
            var str = "";
            var sep = "";
            foreach (var item in list)
            {
                str += (sep + item.ToString());
                sep = ",";

            }

            return str;
        }

    }
}
