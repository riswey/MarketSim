using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    public class Report
    {
        //Stats

        public static string Stats(List<Trader> traders) { 
            return string.Format("Gini: {0:0.000}\tMin: {1:0.000}\tMedian: {2:0.000}\tMax: {3:0.000}\tTotal: {4:0.000}",
                Report.WorldGini(traders),
                Report.MinSatisfaction(traders),
                Report.MedianSatisfaction(traders),
                Report.MaxSatisfaction(traders),
                Report.WorldSatisfaction(traders)
            );
        }

        public static double MedianSatisfaction(List<Trader> traders)
        {
            return traders.Median(t => t.Satisfaction());
        }

        public static double MaxSatisfaction(List<Trader> traders)
        {
            return traders.Max(t => t.Satisfaction());
        }

        public static double MinSatisfaction(List<Trader> traders)
        {
            return traders.Min(t => t.Satisfaction());
        }

        public static double WorldGini(List<Trader> traders)
        {
            List<double> values = traders.Select(t => t.Satisfaction()).ToList();
            values.Sort();

            double areaB = 0;
            double sum = 0;
            foreach (double s in values)
            {
                sum = sum + s;
                areaB = areaB + sum;
            }

            double areaTri = sum * values.Count / 2.0;
            double areaA = areaTri - areaB;

            return areaA / areaTri;
        }

        public static double WorldSatisfaction(List<Trader> traders)
        {
            return traders.Sum(t => t.Satisfaction());
        }

        //Reports

        public static string printDP(List<Trader> traders)
        {
            string str = "▄ Desire Profile------------------------------------------------\n\t";
            str += Report.PrintDesireProfile(traders[0], true);
            traders.ForEach( t => str += "\n" + PrintDesireProfile(t) );
            return str;
        }

        public static string printOwners(List<Entity> entities)
        {
            string str = "▄ Owners------------------------------------------------\n";
            entities.ForEach(t => str += t.index + "(" + t.owner.index + ")\t");
            return str;
        }

        public static string printPortfolios(List<Trader> traders)
        {
            string str = "▄ Portfolios-----------------------------------------------------------------\n";

            int max_pf_size = 0;
            foreach (Trader t in traders)
            {
                str += t.index + "\t";
                if (t.portfolio.Count > max_pf_size)
                {
                    max_pf_size = t.portfolio.Count;
                }
            }

            for (int i = 0; i < max_pf_size; i++)
            {
                str += "\n";
                foreach (Trader t in traders)
                {
                    str += PrintPortfolioI(t, i) + "\t";
                }
            }
            return str;
        }

        public static string PrintSatisfaction(List<Trader> traders)
        {
            string str = "▄ Satisfaction-------------------------------------------------------------\n";
            double sum = 0;

            traders.ForEach(t => str += t.index + "\t");
            str += "\n";
            traders.ForEach(t => {
                double s = t.Satisfaction();
                sum += s;
                str += Math.Round(s, 3) + "\t";
            });
            str += "\nTotal: " + Math.Round(sum, 3);
            return str;
        }

        public static string PrintDesireProfile(Trader trader, bool header = false)
        {
            string str = "";
            if (header)
            {
                for (int i = 0; i < trader.desireProfile.Length; i++)
                {
                    str += "Type" + i + "\t";
                }
                return str;
            }

            str = trader.index + ":\t";
            foreach (double d in trader.desireProfile)
            {
                str += Math.Round(d * 1000) + "\t";
            }
            return str;
        }

        public static string PrintPortfolioI(Trader trader, int index)
        {
            if (index >= trader.portfolio.Count) return "";

            Entity e = trader.portfolio[index];
            return e.type + "(" + Math.Round(trader.DesireFor(e) * 1000) + ")";
        }



    }
}
