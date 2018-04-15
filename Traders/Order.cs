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

        public int t1;
        public int t2;
        public int c1;
        public int c2;

        public void Execute()
        {
            Trader.Exchange(t1, t2, c1, c2);            
        }

    }
}
