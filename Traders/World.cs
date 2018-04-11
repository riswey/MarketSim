using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Market : holds traders (primary agents), parameters {#traders, #num comm types to expect}
 * Traders: hold desire profiles, commodities
 * Commodities: simply types (int)
 */


namespace Traders
{
    public delegate bool D_TradeMethod<T>(T t1, T t2);

    partial class World
    {
        static public MyRandom rnd = new MyRandom();

        //Who is in this world
        int NUM_COM_TYPES;
        int PORTFOLIO_SIZE;

        public List<I_Commodity> commodities = new List<I_Commodity>();
        public List<Trader> traders = new List<Trader>();

        //Give all traders same portfolio size in this model
        public World(int n_traders, int n_com_types, int pf_size)
        {
            rnd.Reset();

            NUM_COM_TYPES = n_com_types;
            PORTFOLIO_SIZE = pf_size;

            for (int i = 0; i < n_traders; i++)
            {
                List<I_Commodity> portfolio = Commodity.ListRandomCommodities(rnd, pf_size, n_com_types);
                double[] dp = Trader.RandomDesireProfile(rnd, n_com_types);
                traders.Add(new Trader(portfolio, dp) );
                commodities.AddRange(portfolio);
            }

        }

        public static void MeetRoundRobin<T>(List<T> collection, D_TradeMethod<T> TradeMethod)
        {
            //Every trader meets every trader
            //triangle or do in reverse also?
            foreach (T a in collection)
            {
                foreach (T b in collection)
                {
                    if (a.Equals(b)) continue;

                    //Ignoring bool return
                    TradeMethod(a, b);

                }
            }
        }

        public bool Run<T>(int loops, Action<List<T>, D_TradeMethod<T>> MeetMethod, D_TradeMethod<T> TradeMethod)
        {
            int i = 0;
            while (i++ < loops) {
                Console.WriteLine(WorldSatisfaction());

                if (typeof(T) == typeof(Trader))
                {
                    MeetMethod(traders as List<T>, TradeMethod);
                }
                else if (typeof(T) == typeof(I_Commodity))
                {
                    MeetMethod(commodities as List<T>, TradeMethod);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public double WorldSatisfaction()
        {
            double sum = 0;
            foreach (Trader t in traders)
            {
                sum += t.Satisfaction();
            }
            return sum;
        }

    }

    //////////////////////////////////////////
    // TELEMETRY /////////////////////////////
    //////////////////////////////////////////

    partial class World {
        public string printDP()
        {
            string str = "▄ Desire Profile------------------------------------------------\n\t";

            for (int i = 0; i < NUM_COM_TYPES; i++)
            {
                str += "Type" + i + "\t";
            }
            foreach (Trader t in traders)
            {
                str += "\n" + t.PrintDesireProfile();
            }
            return str;
        }

        public string printPortfolios()
        {
            string str = "▄ Portfolios-----------------------------------------------------------------\n";
            foreach (Trader t in traders)
            {
                str += t.myid + "\t";
            }

            for (int i = 0; i < PORTFOLIO_SIZE; i++)
            {
                str += "\n";
                foreach (Trader t in traders)
                {
                    str += t.PrintPortfolioI(i)+ "\t";
                }
            }
            return str;
        }

        public string PrintSatisfaction()
        {
            string str = "▄ Satisfaction-------------------------------------------------------------\n";
            double sum = 0;
            foreach (Trader t in traders)
            {
                str += t.myid + "\t";
            }
            str += "\n";
            foreach (Trader t in traders)
            {
                double s = t.Satisfaction();
                sum += s;
                str += Math.Round(s,2) + "\t";
            }
            str += "\nTotal: " + Math.Round(sum, 2);
            return str;
        }
    }

}
