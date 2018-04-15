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
    public delegate bool D_TradeMethod(Entity t1, Entity t2);

    public partial class Market
    {
        private static Dictionary<int, Entity> world;
        public static void BindMarketToData(Dictionary<int, Entity> world)
        {
            Market.world = world;
        }

        static public T Index2Entity<T>(int index) where T : Entity
        {
            return world[index] as T;   //null if error
        }


        public List<Entity> entities;
        //Traders is just to help with looping
        public List<Trader> traders;

        public static Random rnd = new Random();

        static public void GenerateWorld(out Dictionary<int, Entity> world, int n_traders, int n_com_types, int pf_size, int[] capitalists = null)
        {
            world = new Dictionary<int, Entity >();

            int n_commodities = n_traders * pf_size, type;

            //Create commodities
            Entity e;
            for (int i = 0; i < n_commodities; i++)
            {
                type = rnd.Next(n_com_types);
                e = new Entity(type);
                world[e.index] = e;
            }

            Dictionary<int, int> tradermap = new Dictionary<int, int>();

            //Traders need world bound to static reference
            BindMarketToData(world);

            //Create traders
            for (int i = 0; i < n_traders; i++)
            {
                double[] dp = Trader.RandomDesireProfile(rnd, n_com_types);
                int[] portfolio = Trader.RangePortfolio(i* pf_size, pf_size);
                Trader t = new Trader(portfolio, dp);

                tradermap[i] = t.index;

                world[t.index] = t;
            }

            if (capitalists == null) return;

            //Add Capitalism
            for(int i=0;i<capitalists.Length;i+=2)
            {
                int owner = tradermap[capitalists[i]];
                int worker = tradermap[capitalists[i + 1]];

                ((Trader)world[owner]).Take(worker);
            }

        }

        public static List<Entity> ListRandomCommodities(Random rnd, int size, int n_com_types)
        {
            List<Entity> coms = new List<Entity>();
            for (int i = 0; i < size; i++)
            {
                coms.Add(new Entity(rnd.Next(n_com_types)));
            }
            return coms;
        }

        public Market(Dictionary<int, Entity> world)
        {
            //Bind Market to this world
            BindMarketToData(world);

            //Create convenience lists of world
            entities = new List<Entity>();
            traders = new List<Trader>();
            foreach(KeyValuePair<int, Entity> e in world)
            {
                entities.Add(e.Value);
                if (e.Value.type == Trader.TRADER_TYPE)
                {
                    traders.Add((Trader) e.Value);
                }
            }
        }

        public static void MeetRoundRobin(D_TradeMethod TradeMethod)
        {
            //Every trader meets every trader
            //triangle or do in reverse also?
            foreach (KeyValuePair<int, Entity> a in world)
            {
                foreach (KeyValuePair<int, Entity> b in world)
                {
                    if (a.Value.Equals(b.Value)) continue;

                    //Ignoring bool return
                    TradeMethod(a.Value, b.Value);

                }
            }
        }

        public bool Run(int loops, Action<D_TradeMethod> MeetMethod, D_TradeMethod TradeMethod)
        {
            Console.WriteLine("\n--- WORLD RUN ---- " + TradeMethod.GetType().ToString() + "\n");

            int i = 0;
            while (i++ < loops) {

                //Console.WriteLine(PrintSatisfaction());
                Console.WriteLine(WorldSatisfaction());
                Console.WriteLine("Gini: " + WorldGini());

                MeetMethod(TradeMethod);
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

    partial class Market {
        public string printDP()
        {
            string str = "▄ Desire Profile------------------------------------------------\n\t";

            traders[0].PrintDesireProfile(true);

            foreach (Trader t in traders)
            {
                str += "\n" + t.PrintDesireProfile();
            }
            return str;
        }

        public string printPortfolios()
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
                str += t.index + "\t";
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
