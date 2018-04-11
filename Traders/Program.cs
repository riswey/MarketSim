using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traders
{
    static class Program
    {
        static void Main(string[] args)
        {
            World world;

            const int N_TRADERS = 10;
            const int N_COM_TYPES = 20;
            const int SIZE_PORTFOLIO = 20;

            world = new World(N_TRADERS, N_COM_TYPES, SIZE_PORTFOLIO);

            Console.WriteLine(world.printDP());
            Console.WriteLine(world.printPortfolios());
            Console.WriteLine(world.PrintSatisfaction());

            world.Run<Trader>(10, World.MeetRoundRobin, TraderBehaviour.MutualMaxSwap);

            Console.WriteLine("");
            world = new World(N_TRADERS, N_COM_TYPES, SIZE_PORTFOLIO);
            world.Run<I_Commodity>(10, World.MeetRoundRobin, TraderBehaviour.CommodityExchange);

            Console.ReadLine();
        }
    }
}


