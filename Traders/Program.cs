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
            const int N_TRADERS = 10;
            const int N_COM_TYPES = 10;
            const int SIZE_PORTFOLIO = 20;

            World world = new World(N_TRADERS, N_COM_TYPES, SIZE_PORTFOLIO);

            Console.WriteLine(world.printDP());
            Console.WriteLine(world.printPortfolios());
            Console.WriteLine(world.PrintSatisfaction());

            //world.Run<Trader>(World.MeetRoundRobin, TraderBehaviour.RandomExchange);
            world.Run<Commodity>(World.MeetRoundRobin, TraderBehaviour.CommodityExchange);

            Console.ReadLine();
        }

    }
}


