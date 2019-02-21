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
    public class Bank
    {
        static Random rnd = new Random();

        public static Bank singleInstance;

        public static void SetUpBank(Dictionary<int, Entity> world, int num_types)
        {
            Bank.singleInstance = new Bank(world, num_types);
        }

        public Dictionary<int, Entity> world;
        public Dictionary<int, List<int>> portfolios;
        int num_types;

        public Bank(Dictionary<int, Entity> world, int num_types)
        {
            this.world = world;
            this.num_types = num_types;
        }

        void AddEntity(Entity e)
        {
            world[e.index] = e;
        }

        void RemoveEntity(Entity e)
        {
            world.Remove(e.index);
        }

        public void CreateRandomPortfolio(int id, int size)
        {
            portfolios[id] = new List<int>();
            for (int i=0;i<size; i++)
            {
                Entity e = new Entity(rnd.Next(num_types), id);
                //Add to world
                AddEntity(e);
                //Book keep powner
                portfolios[id].Add(e.index);
            }
        }

        public void Take(int id, int item)
        {
            Entity e = GetEntity<Entity>(item);
            //Remove from owner
            portfolios[e.owner].Remove(item);
            //Give to new owner
            e.owner = id;
            portfolios[id].Add(item);
        }

        private T GetEntity<T>(int index) where T : Entity
        {
            return world[index] as T;   //null if error
        }

    }
}
