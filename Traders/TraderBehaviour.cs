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
    public class TraderBehaviour
    {
        public static void SimpleCompareEntities(Tuple<Entity, Entity> pair)
        {
            Trader t1 = (Trader)pair.Item1.owner;
            Trader t2 = (Trader)pair.Item2.owner;

            if (!t1.Equals(t2))
            {
                double d11 = t1.DesireFor(pair.Item1);
                double d12 = t1.DesireFor(pair.Item2);
                double d21 = t2.DesireFor(pair.Item1);
                double d22 = t2.DesireFor(pair.Item2);

                //if t1 desires e2 more than its own
                //if t2 desires e1 more than its own
                if (d12 > d11 && d21 > d22) Trader.Exchange(t1, t2, pair.Item1, pair.Item2);

            }

        }

        public static void RelativeCompareEntities(Tuple<Entity, Entity> pair)
        {
            Trader t1 = (Trader)pair.Item1.owner;
            Trader t2 = (Trader)pair.Item2.owner;

            double s1 = t1.Satisfaction();
            double s2 = t2.Satisfaction();

            if (!t1.Equals(t2))
            {
                double d11 = t1.DesireFor(pair.Item1) / s1;
                double d12 = t1.DesireFor(pair.Item2) / s1;
                double d21 = t2.DesireFor(pair.Item1) / s2;
                double d22 = t2.DesireFor(pair.Item2) / s2;

                //if t1 desires e2 more than its own
                //if t2 desires e1 more than its own
                if (d12 > d11 && d21 > d22) Trader.Exchange(t1, t2, pair.Item1, pair.Item2);

            }

        }


        public static void Sacrifice(Tuple<Entity, Entity> pair)
        {            
            Trader t1 = (Trader)pair.Item1.owner;
            Trader t2 = (Trader)pair.Item2.owner;

            if (!t1.Equals(t2))
            {
                double d11 = t1.DesireFor(pair.Item1);
                double d12 = t1.DesireFor(pair.Item2);
                double d21 = t2.DesireFor(pair.Item1);
                double d22 = t2.DesireFor(pair.Item2);

                //If t2 needs item 1 more than t1 then give to t2
                if (d21 > d11)
                {
                    t1.Release(pair.Item1);
                    t2.Take(pair.Item1);
                }

                if (d12 > d22)
                {
                    t2.Release(pair.Item2);
                    t1.Take(pair.Item2);
                }

            }

        }

        public static void RelativeSacrifice(Tuple<Entity, Entity> pair)
        {
            Trader t1 = (Trader)pair.Item1.owner;
            Trader t2 = (Trader)pair.Item2.owner;

            double s1 = t1.Satisfaction();
            double s2 = t2.Satisfaction();

            if (!t1.Equals(t2))
            {
                double d11 = t1.DesireFor(pair.Item1) / s1;
                double d12 = t1.DesireFor(pair.Item2) / s1;
                double d21 = t2.DesireFor(pair.Item1) / s2;
                double d22 = t2.DesireFor(pair.Item2) / s2;

                //If t2 needs item 1 more than t1 then give to t2
                if (d21 > d11)
                {
                    t1.Release(pair.Item1);
                    t2.Take(pair.Item1);
                }

                if (d12 > d22)
                {
                    t2.Release(pair.Item2);
                    t1.Take(pair.Item2);
                }

            }

        }

        public static void GivingPoor(Tuple<Entity, Entity> pair)
        {
            Trader t1 = (Trader)pair.Item1.owner;
            Trader t2 = (Trader)pair.Item2.owner;

            if (!t1.Equals(t2))
            {
                double d11 = t1.DesireFor(pair.Item1);
                double d12 = t1.DesireFor(pair.Item2);
                double d21 = t2.DesireFor(pair.Item1);
                double d22 = t2.DesireFor(pair.Item2);
                double sat1 = t1.Satisfaction();
                double sat2 = t2.Satisfaction();

                //If t2 needs item 1 more than t1 then give to t2
                if (d21 > d11 && (sat2 + d21) < (sat1 - d11) )
                {
                    t1.Release(pair.Item1);
                    t2.Take(pair.Item1);
                }

                if (d12 > d22 && (sat1 + d12) < (sat2 - d22) )
                {
                    t2.Release(pair.Item2);
                    t1.Take(pair.Item2);
                }

            }

        }







        static public void RandomExchange(Tuple<Trader, Trader> pair)
        {
            Trader t1 = (Trader)pair.Item1;
            Trader t2 = (Trader)pair.Item2;

            int pos1 = World.r.Next(t1.portfolio.Count);
            int pos2 = World.r.Next(t2.portfolio.Count);
            Trader.Exchange(t1, t2, t1.portfolio[pos1], t2.portfolio[pos2]);
            
        }

        //TODO: not finished
        static public void OptimalExchange(Tuple<Trader, Trader> pair)
        {
            Trader t1 = (Trader)pair.Item1;
            Trader t2 = (Trader)pair.Item2;

            //sort by desire profile of 
            List<double> desires11 = new List<double>();
            List<double> desires12 = new List<double>();
            List<double> desires21 = new List<double>();
            List<double> desires22 = new List<double>();

            //get a list of desires
            t1.portfolio.ForEach(e =>
            {
                desires11.Add(t1.DesireFor(e));
                desires21.Add(t2.DesireFor(e));
            });

            t2.portfolio.ForEach(e =>
            {
                desires12.Add(t1.DesireFor(e));
                desires22.Add(t2.DesireFor(e));
            });

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

        }

        public static void MutualMaxSwap(Tuple<Trader, Trader> pair)
        //loop traders
        //Compare all possible exchanges and select one with largest mutual desire magnitude  
        {
            Trader t1 = (Trader)pair.Item1;
            Trader t2 = (Trader)pair.Item2;

            double d1, d2, mag, max = 0;

            Order o = null;

            t1.portfolio.ForEach(e1 =>
            {
                t2.portfolio.ForEach(e2 =>
                {
                    d1 = t1.DesireFor(e2) - t1.DesireFor(e1);
                    d2 = t2.DesireFor(e1) - t2.DesireFor(e2);

                    //ignore -ve values (don;t allow get -ve)
                    if (d1 >= 0 && d2 >= 0)
                    {

                        mag = d1 * d1 + d2 * d2;
                        if (mag > max)
                        {
                            max = mag;
                            o = new Order() { t1 = t1 , t2 = t2, e1 = e1, e2 = e2 };
                        }
                    }
                });
            });

            if (o != null)
            {
                Order.collection.Add(o);
                Order.ExecuteOrders();
            }
        }

    }

}
