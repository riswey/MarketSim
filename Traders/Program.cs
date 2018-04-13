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
            const int N_COM_TYPES = 5;
            const int SIZE_COM_PORTFOLIO = 10;
            const int LOOPS = 5;
            //Capitalists , Owns

            //9{6{0,1},7{2,3},8{4,5}}

            Dictionary<int, int> capitalists = new Dictionary<int, int>() {
                {6,0},
                {6,1},
                {7,2},
                {7,3},
                {8,4},
                {8,5},
                {9,6},
                {9,7},
                {9,8}
            };

            world = new World(N_TRADERS, N_COM_TYPES, SIZE_COM_PORTFOLIO, capitalists);
            
            Console.WriteLine(world.printDP());
            Console.WriteLine(world.printPortfolios());
            Console.WriteLine(world.PrintSatisfaction());
            Console.WriteLine("Gini: " + world.WorldGini());

            world.Run<Trader>(LOOPS, World.MeetRoundRobin, TraderBehaviour.MutualMaxSwap);

            Console.WriteLine("");
            world = new World(N_TRADERS, N_COM_TYPES, SIZE_COM_PORTFOLIO, capitalists);
            world.Run<I_Commodity>(LOOPS, World.MeetRoundRobin, TraderBehaviour.CommodityExchange);

            Console.ReadLine();
        }
    }
}


