using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Reporter
{
    internal class HtmlUtils
    {
        public static String BuildReportFooter()
        {
            return "</body></html>";
        }

        public static String BuildReportHeader(string reportTitle)
        {
            var msg = "";
            var header = "<head><style>";
            header += "table {border-collapse: collapse}";
            header += "table,td,th {border: 1px solid black}";
            header += "td,th {padding: 5px; white-space:nowrap}";
            header += ".number {text-align:right}";
            header += ".date {text-align:right}";
            header += ".warning {color:red}";
            header += "</style></head>";

            msg += $"<html>{header}<body>";
            msg += $"<h1>{reportTitle}</h1>";

            return msg;

        }

        public static String EndTable()
        {
            var msg = "</table>";
            return msg;
        }

        public static String BuildTableCell(String val)
        {
            return $"<td>{val}</td>";
        }
        public static String BuildNumberCell(String val)
        {
            return $"<td class='number'>{val}</td>";
        }

        public static String BuildDateCell(String val)
        {
            return $"<td class='date'>{val}</td>";
        }


        public static String BuildTableHeader(String val)
        {
            return $"<th>{val}</th>";
        }


    }
}
