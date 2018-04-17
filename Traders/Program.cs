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
            const int N_COM_TYPES = 5;
            const int SIZE_COM_PORTFOLIO = 10;
            const int LOOPS = 5;
            //Owner/Worker dictionary
            //e.g. 9{6{0,1},7{2,3},8{4,5}}
            int[] capitalists = new int[] { 
                6,0,
                6,1,
                7,2,
                7,3,
                8,4,
                8,5,
                9,6,
                9,7,
                9,8
            };

            //Make the world & clone for each run replicate
            Dictionary<int, Entity> world;
            Market.WorldGenerator(out world, N_TRADERS, N_COM_TYPES, SIZE_COM_PORTFOLIO);

            
            Console.WriteLine("\n#Original##################################\n");
            Dictionary<int, Entity> world1 = world.Clone();
            Bank.SetBankWorld(world1);
            Market market1 = new Market(world1);
            //Console.WriteLine(market1.printDP());
            Console.WriteLine(market1.printOwners());
            Console.WriteLine(market1.printPortfolios());
            Console.WriteLine(market1.PrintSatisfaction());
            Console.WriteLine("Gini: " + market1.WorldGini());
            market1.Run(LOOPS, Market.MeetRoundRobin, TraderBehaviour.MutualMaxSwap);

            Console.WriteLine("\n#With Capitalists##################################\n");
            Dictionary<int, Entity> world2 = world.Clone();
            Bank.SetBankWorld(world2);

            Market.AddCapitalists(world2, capitalists);

            Market market2 = new Market(world2);
            //Console.WriteLine(market2.printDP());
            Console.WriteLine(market2.printOwners());
            Console.WriteLine(market2.printPortfolios());
            Console.WriteLine(market2.PrintSatisfaction());
            Console.WriteLine("Gini: " + market2.WorldGini());
            market2.Run(LOOPS, Market.MeetRoundRobin, TraderBehaviour.MutualMaxSwap);

            Console.ReadLine();
        }
    }
}


