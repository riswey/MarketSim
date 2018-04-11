using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    public class Commodity
    {
        static int ID = 0;
        public string myid { get; } = "C" + ID++;

        public int type { get; set; }
        public Trader owner { get; set; }

        public Commodity(int t)
        {
            type = t;
        }

        public static Commodity NewRandomCommodity(Random rnd, int t)
        {
            return new Commodity(rnd.Next(t));

        }

        public static List<Commodity> ListRandomCommodities(Random rnd, int size, int n_com_types)
        {
            List<Commodity> coms = new List<Commodity>();
            for (int i = 0; i < size; i++)
            {
                coms.Add(Commodity.NewRandomCommodity(rnd, n_com_types));
            }
            return coms;
        }

    }
}
