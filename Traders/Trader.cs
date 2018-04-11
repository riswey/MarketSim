using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{

    public partial class Trader
    {
        static int ID = 0;
        public string myid { get; } = "T" + ID++;

        double[] desireProfile;

        public List<Commodity> portfolio { get; }

        public Trader(List<Commodity> portfolio, double[] dp)
        {
            desireProfile = dp;
            foreach(Commodity c in portfolio)
            {
                c.owner = this;
            }
            this.portfolio = portfolio;
        }

        public static double[] RandomDesireProfile(Random rnd, int n_com_types)
        {
            var dp = new double[n_com_types];
            for (int i = 0; i < n_com_types; i++)
            {
                dp[i] = rnd.NextDouble();
            }
            return dp;
        }

        public double DesireFor(Commodity c)
        {
            return desireProfile[c.type];
        }
        /*
        public void SortPortfolio()
        {
            portfolio.Sort(delegate (Commodity c1, Commodity c2)
            {
                double d1 = desireFor(c1);
                double d2 = desireFor(c2);

                if (d1 == d2) return 0;
                if (d1 < d2) return -1;
                return 1;
            });
        }
        */
        public double Satisfaction()
        {
            double sum = 0;
            portfolio.ForEach(delegate (Commodity i) { sum += DesireFor(i); });
            return sum;
        }

        //Needs to be able public Take so can add commodities in setup
        bool Take(Commodity c)
        {
            if (c.owner == null)
            {
                c.owner = this;
                portfolio.Add(c);
            }
            else
            {
                return false;
            }
            return true;
        }

        bool Release(Commodity c)
        {
            c.owner = null;
            return portfolio.Remove(c);
        }

        public static void Exchange(Trader t1, Trader t2, Commodity c1, Commodity c2)
        {
            t1.Release(c1);
            t2.Release(c2);
            t1.Take(c2);
            t2.Take(c1);
        }


        /*
        //REVIEW THESE
        public List<Commodity> offerCommodity(Commodity com)
        {
            List<Commodity> myoffers = new List<Commodity>();

            double target = desireFor(com);

            foreach (Commodity c in portfolio)
            {
                if (target >= desireFor(c))
                {
                    myoffers.Add(c);
                }
            }

            return myoffers;
        }

        //TODO: return a list, pass back
        public Commodity considerOffers(List<Commodity> ls)
        {
            //no offers
            if (ls.Count == 0) return null;

            int select = 0;
            double max = desireFor(ls[0]);

            for (int i = 1; i < ls.Count; i++)
            {
                if (desireFor(ls[i]) > max)
                {
                    select = i;
                    max = desireFor(ls[i]);
                }
            }

            //find lowest and see if worth swapping
            if (max > desireFor(portfolio[0]))
            {
                //present for swap
                return portfolio[0];
            }
            return null;
        }

        public static void meet(Trader t, Trader u)
        {
            //can't meet self
            if (t.Equals(u)) return;
            //show around each others store
            t.tradePortfolioTo(u);
            u.tradePortfolioTo(t);
        }

        public void tradePortfolioTo(Trader t)
        {
            List<Commodity> ls;

            //list my own stock. My portfolio will be changed by the operation
            var pf = new Commodity[portfolio.Count];
            portfolio.CopyTo(pf);

            foreach (Commodity c in pf)
            {
                //Offer all my commodities 
                ls = t.offerCommodity(c);
                //give offers to customer
                Commodity swap = considerOffers(ls);

                if (swap != null)
                {
                    bool okme = release(c);
                    bool okt = t.release(swap);
                    if (okme && okt)
                    {
                        take(swap);
                        t.take(c);
                    } else if (okme) {
                        //I released but t didn't so pick up commodity again
                        take(c);
                    }
                }
            }
        }
        */
    }

    partial class Trader
    {
        public string PrintDesireProfile()
        {
            string str = myid + ":\t";
            foreach (double d in desireProfile)
            {
                str += Math.Round(d * 100) + "\t";
            }
            return str;
        }

        public string PrintPortfolioI(int i)
        {
            return portfolio[i].type + " (" + Math.Round(DesireFor(portfolio[i]) * 100) + ")";
        }
    }
}
