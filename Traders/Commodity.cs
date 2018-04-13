using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    public interface I_Commodity
    {
        string myid { get; }
        int type { get; set; }
        Trader owner { get; set; }
    }

    public class Commodity: ICloneable, I_Commodity
    {
        static int ID = 0;
        public string myid { get; } = "C" + ID++;

        public int type { get; set; }
        public Trader owner { get; set; }

        public Commodity(int t)
        {
            type = t;
        }

        //For Cloning
        private Commodity(Commodity c)
        {
            this.myid = c.myid;
            this.type = c.type;
            this.owner = c.owner;       //TODO: copies reference... needs to be a new owners!
        }

        public static I_Commodity NewRandomCommodity(Random rnd, int t)
        {
            return new Commodity(rnd.Next(t));
        }

        public static List<I_Commodity> ListRandomCommodities(Random rnd, int size, int n_com_types)
        {
            List<I_Commodity> coms = new List<I_Commodity>();
            for (int i = 0; i < size; i++)
            {
                coms.Add(Commodity.NewRandomCommodity(rnd, n_com_types));
            }
            return coms;
        }

        public object Clone()
        {
            return new Commodity(this);
        }
    }
}
