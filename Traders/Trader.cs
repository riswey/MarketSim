using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{

    public partial class Trader : Entity, ICloneable
    {
        public const int TRADER_TYPE = -1;

        public double[] desireProfile { get; set; }
        public List<Entity> portfolio { get; set; } = new List<Entity>();

        public Trader(double[] dp) : base(TRADER_TYPE)
        {
            this.desireProfile = dp;
            this.portfolio = portfolio;
            portfolio.ForEach(e => Take(e));
        }

        public static double[] RandomDesireProfile(Random rnd, int n_com_types)
        {
            var dp = new double[n_com_types];
            for (int i = 0; i < n_com_types; i++)
            {
                dp[i] = rnd.NextDouble();
            }
            return dp;
        }

        public double DesireFor(Entity e)
        {            
            if (e.type != TRADER_TYPE)
            {
                return desireProfile[e.type];
            }
            else
            {
                //TODO: BEWARE: Trader -> Trader can form a loop
                //Half the satisfaction has been set aside for me already so this is mine 
                return (e as Trader).Satisfaction();
            }
        }

        public double Satisfaction()
        {
            double sum = portfolio.Sum(e => DesireFor(e));

            //If has a capitalist then half will go to them so return only half
            if (owner == null)
            {
                return sum;
            } else {
                //Free
                return sum / 2;
            }
        }

        public bool Take(Entity e)
        {
            //can't take something that still owned
            if (e.owner != null) return false;
            e.owner = this;
            portfolio.Add(e);
            return true;
        }

        public bool Release(Entity e)
        {
            e.owner = null;
            return portfolio.Remove(e);
        }

        public static void Exchange(Trader t1, Trader t2, Entity e1, Entity e2)
        {
            t1.Release(e1);
            t2.Release(e2);
            t1.Take(e2);
            t2.Take(e1);
        }


        //Shallow Cloning
        protected Trader(Trader t) : base(t)
        {
            this.desireProfile = new double[t.desireProfile.Length];
            t.desireProfile.CopyTo(this.desireProfile, 0);
        }

        public virtual object Clone()
        {
            return new Trader(this);
        }


    }
}
