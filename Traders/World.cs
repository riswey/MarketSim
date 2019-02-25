using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    /// <summary>
    /// All transactions (Take) to go through the bank
    /// Bank bookkeeps all portfolios
    /// </summary>
    public class World
    {
        //public delegate bool D_TradeMethod(Entity t1, Entity t2);

        public static Random r = new Random();

        public List<Entity> world;
        //lists of references to world for convenience
        public List<Entity> entities;
        public List<Trader> traders;

        public void LoadData(List<Entity> readonlydata)
        {
            this.world = World.CloneData(readonlydata);
            //few convenience lists
            entities = world.Where(e => e.type != Trader.TRADER_TYPE).Select(e => e).ToList();
            traders = world.Where(e => e.type == Trader.TRADER_TYPE).Select(e => e as Trader).ToList();
        }

        private static List<Entity> CloneData(List<Entity> world)
        {
            //only shallow. Need create ownerhip, and portfolio links
            var world1 = world.Clone();
            world.ForEach( e =>
            {
                if (e.type != Trader.TRADER_TYPE)
                {
                    Trader owner1 = (Trader)world1[e.owner.index];
                    Entity entity1 = world1[e.index];
                    owner1.Take(entity1);   //adds to portfolio and sets entity owner
                }
            });
            return world1;
        }

        public static List<Entity> WorldGenerator(int n_traders, int n_types, int size_portfolio)
        {
            List<Entity> world = new List<Entity>();
            Entity e;

            for (int j = 0; j < n_traders; j++)
            {

                Trader t = new Trader(Trader.RandomDesireProfile(r, n_types));
                t.index = world.Count;              //can track by refernce or index to world list
                world.Add(t);

                for (int i = 0; i < size_portfolio; i++)
                {
                    e = new Entity(r.Next(n_types));
                    e.index = world.Count;          //can track by refernce or index to world list 
                    world.Add(e);
                    t.Take(e);
                }

            }
            return world;
        }

    }
}
