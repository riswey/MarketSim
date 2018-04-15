using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    /// <summary>
    /// Static collection of Trader behaviours
    /// 
    /// Portfolios may be different sizes
    /// </summary>
    class TraderBehaviour
    {
        static bool TypeTrader(Entity a, Entity b, out Trader t1, out Trader t2)
        {
            if(a.type == Trader.TRADER_TYPE && b.type == Trader.TRADER_TYPE)
            {
                t1 = (Trader)a;
                t2 = (Trader)b;
                return true;
            }
            t1 = null;
            t2 = null;

            return false;
        }

        static public bool RandomExchange(Entity e1, Entity e2)
        {
            Trader t1, t2;
            if (!TypeTrader(e1,e2,out t1,out t2)) return false;

            int pos1 = Market.rnd.Next(t1.portfolio.Count);
            int pos2 = Market.rnd.Next(t2.portfolio.Count);
            Trader.Exchange(t1.index, t2.index, t1.portfolio[pos1], t2.portfolio[pos2]);
            return true;
        }

        //TODO: not finished
        static public bool OptimalExchange(Entity e1, Entity e2)
        {
            Trader t1, t2;
            if (!TypeTrader(e1, e2, out t1, out t2)) return false;
            //sort by desire profile of 
            List<double> desires11 = new List<double>();
            List<double> desires12 = new List<double>();
            List<double> desires21 = new List<double>();
            List<double> desires22 = new List<double>();

            //get a list of desires
            foreach(int c in t1.portfolio)
            {
                desires11.Add(t1.DesireFor(c));
                desires21.Add(t2.DesireFor(c));
            }
            foreach (int c in t2.portfolio)
            {
                desires12.Add(t1.DesireFor(c));
                desires22.Add(t2.DesireFor(c));
            }

            //Ascending
            desires11.Sort();
            desires12.Sort();
            desires21.Sort();
            desires22.Sort();


            int p1 = desires11.Count;
            int p2 = desires12.Count;
            
            while (p1 > 0 && p2 > 0) {
                if (
                    desires12[p1] > desires11[p1]     //I desire your top item more than my top item
                    &&
                    desires21[p2] > desires22[p2]     //You desire my top item more than your top item
                    )
                {

                }

                --p1;--p2;
            }

            return true;

        }

        public static bool MutualMaxSwap(Entity e1, Entity e2)
        {
            Trader t1, t2;
            if (!TypeTrader(e1, e2, out t1, out t2)) return false;

            double d1, d2, mag, arg, max = 0;

            Order o = null;

            foreach (int c1 in t1.portfolio)
            {
                foreach (int c2 in t2.portfolio)
                {
                    d1 = t1.DesireFor(c2) - t1.DesireFor(c1);
                    d2 = t2.DesireFor(c1) - t2.DesireFor(c2);

                    //ignore -ve values (don;t allow get -ve)
                    if (d1 < 0 || d2 < 0) continue;

                    mag = d1 * d1 + d2 * d2;
                    if (mag > max)
                    {
                        max = mag;
                        o = new Order() { t1 = t1.index, t2 = t2.index, c1 = c1, c2 = c2 };
                    }
                }
            }

            if (o != null)
            {
                Order.collection.Add(o);
                Order.ExecuteOrders();
            }

            return true;

        }

        static public bool CommodityExchange(Entity c1, Entity c2)
        {
            if (c1.type == Trader.TRADER_TYPE || c1.type == Trader.TRADER_TYPE) return false;

            Trader t1 = Market.Index2Entity<Trader>(c1.owner);
            Trader t2 = Market.Index2Entity<Trader>(c2.owner);

            double dpcc = t1.DesireFor(c1.index);
            double dpcd = t1.DesireFor(c2.index);
            double dpdc = t2.DesireFor(c1.index);
            double dpdd = t2.DesireFor(c2.index);

            if (dpcd > dpcc && dpdc > dpdd)
            {
                //They agreed
                Trader.Exchange(c1.owner, c2.owner, c1.index, c2.index);
            }
            return true;
        }

    }

}
