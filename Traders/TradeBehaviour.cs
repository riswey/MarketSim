using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    /// <summary>
    /// Static collection of Trader behaviours
    /// </summary>
    class TraderBehaviour
    {

        static public bool RandomExchange(Trader t1, Trader t2)
        {
            int size = t1.portfolio.Count;

            //do a random swap;

            int pos = World.rnd.Next(size);

            Trader.Exchange(t1, t2, t1.portfolio[pos], t2.portfolio[pos]);

            return true;
        }

        static public bool OptimalExchange(Trader t1, Trader t2)
        {
            int size = t1.portfolio.Count;

            //sort by desire profile of 
            List<double> desires11 = new List<double>();
            List<double> desires12 = new List<double>();
            List<double> desires21 = new List<double>();
            List<double> desires22 = new List<double>();

            //get a list of desires
            for (int i = 0; i < size; i++)
            {
                desires11.Add(t1.DesireFor(t1.portfolio[i]) );
                desires12.Add(t1.DesireFor(t2.portfolio[i]));
                desires21.Add(t2.DesireFor(t1.portfolio[i]));
                desires22.Add(t2.DesireFor(t2.portfolio[i]));
            }

            //Ascending
            desires11.Sort();
            desires12.Sort();
            desires21.Sort();
            desires22.Sort();

            for (int i = size; i < 0; i--)
            {
                if (
                    desires12[i] > desires11[i]     //I desire your top item more than my top item
                    &&
                    desires21[i] > desires22[i]     //You desire my top item more than your top item
                    )
                {

                }
            }

            return true;

        }

        public bool CigaretteCardSwaps(Trader seller, Trader buyer)
        {

            double dpcc;
            double dpcd;
            double dpdc;
            double dpdd;

            foreach (Commodity c in seller.portfolio)
            {
                foreach (Commodity d in buyer.portfolio)
                {
                    dpcc = seller.DesireFor(c);
                    dpcd = seller.DesireFor(d);
                    dpdc = buyer.DesireFor(c);
                    dpdd = buyer.DesireFor(d);

                    if (dpcd > dpcc && dpdc > dpdd)
                    {
                        //They agreed
                        Console.WriteLine( seller.myid + "," + buyer.myid + "," + c.myid + d.myid );
                        //Order.collection.Add(new Order() { t1 = seller, t2 = buyer, c1 = c, c2 = d, type = Order.ORDERTYPE.SWAP });
                    }
                }
            }
            Order.ExecuteOrders();

            return true;

        }

        static public bool CommodityExchange(Commodity c1, Commodity c2)
        {
            Trader t1 = c1.owner;
            Trader t2 = c2.owner;

            double dpcc = t1.DesireFor(c1);
            double dpcd = t1.DesireFor(c2);
            double dpdc = t2.DesireFor(c1);
            double dpdd = t2.DesireFor(c2);

            if (dpcd > dpcc && dpdc > dpdd)
            {
                //They agreed
                Trader.Exchange(t1, t2, c1, c2);
            }
            return true;
        }

    }

}
