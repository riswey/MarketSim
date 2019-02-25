using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    class Worker: Trader
    {
        int skill { get; set; }        //I make this type of commodity

        Worker(int skill, int[] portfolio, double[] dp, int owner): base(portfolio, dp, owner) {
            this.skill = skill;
        }

        private Worker(Worker w): base(w)
        {
            this.skill = w.skill;
        }

        public new object Clone()
        {
            return new Worker(this);
        }

        public void Work()
        {
            Entity e = new Entity(this.skill, Entity.FREE);

            portfolio.Add(e.index);
            World.singleInstance.AddEntity(e);
        }

    }
}
