using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    
    public class Entity: ICloneable
    {
        protected const int FREE = -1;

        private static Dictionary<int, Entity> world;
        public static void BindWorld(Dictionary<int, Entity> world)
        {
            Entity.world = world;
        }

        static int ID = 0;
        public int index { get; } = ID++;

        public int type { get; set; }
        public int owner { get; set; } = FREE;

        public Entity(int t)
        {
            type = t;
        }

        //For Cloning
        protected Entity(Entity c)
        {
            this.index = c.index;
            this.type = c.type;
            this.owner = c.owner;
        }

        static public T Index2Entity<T>(int index) where T: Entity
        {
            return world[index] as T;   //null if error
        }

        public virtual object Clone()
        {
            return new Entity(this);
        }
    }
}
