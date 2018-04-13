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

    public partial class World
    {

        //Who is in this world (remove this?)
        int NUM_COM_TYPES;
        int PORTFOLIO_SIZE;

        /// <summary>
        /// Used for traversing the world commodities
        /// </summary>
        public List<I_Commodity> commodities = new List<I_Commodity>();
        /// <summary>
        /// Used for traversing the world traders
        /// </summary>
        public List<Trader> traders = new List<Trader>();

        static void GenerateTraders(int n_traders, int n_com_types, int pf_size, out List<Trader> traders, out List<I_Commodity> commodities)
        {
            Random rnd = new Random();

            traders = new List<Trader>();
            commodities = new List<I_Commodity>();

            for (int i = 0; i < n_traders; i++)
            {
                double[] dp = Trader.RandomDesireProfile(rnd, n_com_types);
                List<I_Commodity> portfolio = Commodity.ListRandomCommodities(rnd, pf_size, n_com_types);
                traders.Add(new Trader(portfolio, dp));
                commodities.AddRange(portfolio);
            }

        }


        //Give all traders same portfolio size in this model
        public World(List<Trader> traders, List<I_Commodity> commodities, int n_com_types, int pf_size, Dictionary<int,int> capitalists = null)
        {
            NUM_COM_TYPES = n_com_types;
            PORTFOLIO_SIZE = pf_size;

            this.traders = traders;
            this.commodities = commodities;

            if (capitalists == null) return;

            //Add Capitalism
            foreach(KeyValuePair<int,int> status in capitalists)
            {
                traders[status.Key].Take(traders[status.Value]);
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
            Console.WriteLine("\n--- WORLD RUN ---- " + TradeMethod.GetType().ToString() + "\n");

            int i = 0;
            while (i++ < loops) {

                //Console.WriteLine(PrintSatisfaction());
                Console.WriteLine(WorldSatisfaction());
                Console.WriteLine("Gini: " + WorldGini());


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

        //////////////////////////////////////////
        // METRICS ///////////////////////////////
        //////////////////////////////////////////

        /// <summary>
        /// calculates Gini, TotalSatisfaction on the side 
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public double WorldGini()
        {
            List<double> sat = new List<double>();
            foreach (Trader t in traders)
            {
                sat.Add(t.Satisfaction());
            }
            return Gini(sat);
        }

        public static double Gini(List<double> values)
        {
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
                str += Math.Round(s,3) + "\t";
            }
            str += "\nTotal: " + Math.Round(sum, 3);
            return str;
        }

    }

}
