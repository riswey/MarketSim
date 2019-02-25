using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{

    public class Entity: ICloneable
    {
        public int index { get; set; }
        public int type { get; set; }
        public Entity owner { get; set; }

        public Entity(int type)
        {
            this.type = type;
        }
        
        //For Cloning
        protected Entity(Entity e)
        {
            this.index = e.index;
            this.type = e.type;
        }

        public virtual object Clone()
        {
            return new Entity(this);
        }
        
    }
}
