using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{
    /// <summary>
    /// Only deals with indexes
    /// </summary>
    public class Order
    {
        public static List<Order> collection = new List<Order>();

        public static bool ExecuteOrders()
        {
            Order.collection.ForEach(o => o.Execute());
            ClearOrders();
            return true;
        }

        static void ClearOrders()
        {
            Order.collection.Clear();
        }

        public Trader t1;
        public Trader t2;
        public Entity e1;
        public Entity e2;

        public void Execute()
        {
            Trader.Exchange(t1, t2, e1, e2);            
        }

    }
}
