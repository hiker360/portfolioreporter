using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioReporter.Utils
{
    internal class CalcUtils
    {
        public static double CalcSharpeRatio(double stockReturn, double lowRiskReturn, double standardDev)
        {
            //https://www.calculatorpro.com/calculator/sharpe-ratio-calculator/
            var sr = (stockReturn - lowRiskReturn) / standardDev;
            return sr;
        }

        public static double CalcSharpeRatio(double stockReturn, double lowRiskReturn, double standardDev, int f)
        {
            //https://logical-invest.com/universal-investment-strategy/
            // If f = 0, then sd ^ 0 = 1 and the ranking algorithm will choose the composition with the highest performance without considering volatility.
            // If f = 1, then I have the normal Sharpe formula.
            // If f> 1, then I rather want to find SPY - TLT combinations with a low volatility.With high f values, the algorithm becomes a “minimum variance” or “minimum volatility” algorithm.

            var sr = (stockReturn - lowRiskReturn) / Math.Pow(standardDev, f);
            return sr;
        }


        public static double CalcStandardDeviation(List<double> someDoubles)
        {
            //https://www.calculator.net/standard-deviation-calculator.html
            double average = someDoubles.Average();
            double sumOfSquaresOfDifferences = someDoubles.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / someDoubles.Count);
            return sd;
        }

        public static double AnnualizeReturns(DateTime periodStartDate, DateTime periodEndDate, decimal startingCapital, decimal endingCapital)
        {
            //https://www.asecurelife.com/annualized-return-formula/
            //https://www.calculator.net/roi-calculator.html
            double days = (periodEndDate - periodStartDate).TotalDays;
            var years = Math.Round(days / 365, 2);
            var roi = (double) (endingCapital / startingCapital) - 1;
            double apr = Math.Pow((1 + roi), (1 / years)) - 1.0d;
            return apr;

        }
        public static decimal CalcReturnAmount(decimal baseValue, decimal marketValue) => (decimal) CalcReturnAmount((double)baseValue, (double)marketValue);
        public static double CalcReturnAmount(double baseValue, double marketValue)
        {
            double rtnAmt = (marketValue - baseValue);
            return rtnAmt;

        }

        public static double CalcReturnPercent(decimal baseValue, decimal marketValue) => CalcReturnPercent((double)baseValue, (double)marketValue);

        public static double CalcReturnPercent(double baseValue, double marketValue)
        {
            if (baseValue == 0) return 0;
            double rtn = (marketValue - baseValue) / baseValue;
            rtn = RoundPercentage(rtn);
            return rtn;

        }

        public static double CalcTaxEffectiveYield(double yield, double taxRate)
        {
            double taxFactor = 1 - taxRate;
            double adjyield = yield / taxFactor;
            return adjyield;
        }

        public static double CalcShares(double cash, double sharePrice)
        {
            double rawQty = cash / sharePrice;
            decimal dQty = Convert.ToDecimal(rawQty);
            return Math.Truncate(rawQty);
        }

        public static double CalcAverage(int cnt, double sum)
        {
            return sum / cnt;
        }

        public static double CalcAverage(Queue<int> queue)
        {
            int sum = 0;
            foreach (int value in queue)
            {
                sum += value;
            }
            return sum / queue.Count;
        }

        public static double CalcAverage(Queue<double> queue)
        {
            double sum = 0;
            foreach (double value in queue)
            {
                sum += value;
            }
            return sum / queue.Count;
        }


        public static double CalcStandardDeviation(Queue<double> queue)
        {
            double avg = CalcAverage(queue);

            Queue<Double> diffs = new Queue<double>(queue.Count);
            foreach (double value in queue)
            {
                double diff = Math.Pow((value - avg), 2);
                diffs.Enqueue(diff);
            }

            double sdavg = CalcAverage(diffs);
            double result = Math.Sqrt(sdavg);
            return result;

        }

        public static double RoundPercentage(double val)
        {
            return Math.Round(val, 4);
        }

        public static double RoundCurrency(double val)
        {
            return Math.Round(val, 2);
        }

        public static double CalcSlope(double startingValue, double endingValue, int lookbackDays)
        {
            int x1 = -1 * (lookbackDays - 1);
            int x2 = 0;
            double y1 = startingValue;
            double y2 = endingValue;
            double slope = CalcSlope(x1, y1, x2, y2);
            return slope;

        }

        public static double CalcSlope(double x1, double y1, double x2, double y2)
        {
            // https://www.calculator.net/slope-calculator.html
            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            double slope = deltaY / deltaX;
            return slope;

        }

    }
}
