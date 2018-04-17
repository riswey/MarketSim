using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    /// <summary>
    /// All transactions to go through the bank
    /// </summary>
    public class Bank
    {
        public static Dictionary<int, Entity> world;

        public static void SetBankWorld(Dictionary<int, Entity> world)
        {
            Bank.world = world;
        }

        static public T Index2Entity<T>(int index) where T : Entity
        {
            return world[index] as T;   //null if error
        }

        static public void AddEntity(Entity e)          //deposit
        {
            world[e.index] = e;
        }

        static public void ConsumeEntity(Entity e)      //withdraw
        {
            world.Remove(e.index);
        }
    }
}
