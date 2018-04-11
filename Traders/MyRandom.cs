using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    public class MyRandom
    {
        Random r = new Random();

        List<double> table= new List<double>();
        int c = 0;

        public void Reset()
        {
            c = 0;
        }

        public double NextDouble()
        {
            if (c == table.Count)
            {
                //Double 0.0 <= x < 1.0
                this.table.Add(r.NextDouble());
            }
            return table[c++];
        }

        public int Next(int range)
        {
            return (int) Math.Floor(NextDouble() * range);
        }
    }
}
