using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    public class Order
    {
        public static List<Order> collection = new List<Order>();

        public static bool ExecuteOrders()
        {
            foreach (Order order in Order.collection)
            {
                order.Execute();
            }

            ClearOrders();

            return true;
        }

        static void ClearOrders()
        {
            Order.collection.Clear();
        }

        public Trader t1;
        public Trader t2;
        public Commodity c1;
        public Commodity c2;

        public void Execute()
        {
            Trader.Exchange(t1,t2,c1,c2);            
        }

        public string ToString()
        {
            return t1.DesireFor(c1) + "->" + t1.DesireFor(c2) + "," + t2.DesireFor(c2) + "->" + t2.DesireFor(c1);
        }
    }
}
