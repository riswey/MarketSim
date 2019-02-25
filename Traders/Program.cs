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
            const int N_TYPES = 5;
            const int SIZE_PORTFOLIO = 10;
            const int LOOPS = 3;

            World world = new World();

            //Make the world & clone for each run replicate
            List<Entity> rawdata = World.WorldGenerator(N_TRADERS, N_TYPES, SIZE_PORTFOLIO);

            world.LoadData(rawdata);
            //Console.WriteLine(Report.printOwners(world.entities));
            //Console.WriteLine(Report.printPortfolios(world.traders));
            Console.WriteLine(Report.PrintSatisfaction(world.traders));

            Console.WriteLine("\n#Barter##################################\n");

            Console.WriteLine("0\t" + Report.Stats(world.traders));
            Extensions.Loop(LOOPS, (i) => 
            {
                world.entities.AllPairs().ForEach(pair =>
                {
                    TraderBehaviour.SimpleCompareEntities(pair);
                });

                Console.WriteLine( (i+1) + "\t" + Report.Stats(world.traders));
            });
            Console.WriteLine(Report.PrintSatisfaction(world.traders));

            Console.WriteLine("\n#Sacrifice##################################\n");
            //reset
            world.LoadData(rawdata);
            Console.WriteLine("0\t" + Report.Stats(world.traders));
            Extensions.Loop(LOOPS, (i) =>
            {
                world.entities.AllPairs().ForEach(pair =>
                {
                    TraderBehaviour.Sacrifice(pair);
                });

                Console.WriteLine((i + 1) + "\t" + Report.Stats(world.traders));
            });
            Console.WriteLine(Report.PrintSatisfaction(world.traders));

            Console.WriteLine("\n#GivingPoor##################################\n");
            //reset
            world.LoadData(rawdata);
            Console.WriteLine("0\t" + Report.Stats(world.traders));
            Extensions.Loop(LOOPS, (i) =>
            {
                world.entities.AllPairs().ForEach(pair =>
                {
                    TraderBehaviour.GivingPoor(pair);
                });

                Console.WriteLine((i + 1) + "\t" + Report.Stats(world.traders));
            });
            Console.WriteLine(Report.PrintSatisfaction(world.traders));


            Console.WriteLine("\n#MaxSwap##################################\n");

            //reset
            world.LoadData(rawdata);

            Console.WriteLine("0\t" + Report.Stats(world.traders));
            Extensions.Loop(LOOPS, (i) =>
            {
                world.traders.AllPairs().ForEach(pair =>
                {
                    TraderBehaviour.MutualMaxSwap(pair);
                });

                Console.WriteLine((i + 1) + "\t" + Report.Stats(world.traders));
            });

            Console.WriteLine(Report.PrintSatisfaction(world.traders));

        
            Console.ReadLine();
        }
    }
}


