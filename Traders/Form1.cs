using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traders
{

    public partial class Form1 : Form
    {

        World world = new World();
        List<List<Entity>> rawdata = new List<List<Entity>>();

        public Form1()
        {
            InitializeComponent();
            Run();
        }

        public void Run()
        {
            const int N_TRADERS = 20;
            const int N_TYPES = 10;
            const int SIZE_PORTFOLIO = 20;
            const int NTRADES = 3;
            const int REPS = 10;             //montcarly reps



            //Make the world & clone for each run replicate
            Enumerable
                .Range(0, REPS)
                .ForEachWithIndex((v, i) => rawdata.Add(World.WorldGenerator(N_TRADERS, N_TYPES, SIZE_PORTFOLIO)) );

            DoSim<Entity>(REPS, NTRADES, "Barter", TraderBehaviour.SimpleCompareEntities);
            DoSim<Entity>(REPS, NTRADES, "Sacrifice", TraderBehaviour.Sacrifice);
            DoSim<Entity>(REPS, NTRADES, "RelativeSacrifice", TraderBehaviour.Sacrifice);
            DoSim<Entity>(REPS, NTRADES, "Giving Poor", TraderBehaviour.GivingPoor);

            DoSim<Trader>(REPS, NTRADES, "Max Exchange", TraderBehaviour.MutualMaxSwap);

        }

        void DoSim<T>(int reps, int loops, string title, Action<Tuple<T, T>> InteractionFunction )
        {
            WriteLine("\n-- " + title + " ------------------------------------\n");

            List<double> resultSatisfaction = null;

            Extensions.Loop(reps, (rep) =>
            {
                WriteLine("\n> " + rep + "\n");
                //reset
                world.LoadData(rawdata[rep]);

                WriteLine("0\t" + Report.Stats(world.traders));

                if (typeof(T) == typeof(Trader))
                {
                    Extensions.Loop(loops, (i) =>
                    {
                        world.traders.AllPairs().ForEach(pair =>
                        {
                            InteractionFunction(pair as Tuple<T, T>);
                        });

                        WriteLine((i + 1) + "\t" + Report.Stats(world.traders));
                    });

                }

                if (typeof(T) == typeof(Entity))
                {
                    Extensions.Loop(loops, (i) =>
                    {
                        world.entities.AllPairs().ForEach(pair =>
                        {
                            InteractionFunction(pair as Tuple<T, T>);
                        });

                        WriteLine((i + 1) + "\t" + Report.Stats(world.traders));
                    });

                }

                var worldsat = Report.WorldSatisfactionData(world.traders);
                //cumulate
                if (resultSatisfaction == null)
                    resultSatisfaction = worldsat;
                else //add
                    resultSatisfaction = resultSatisfaction.Zip(worldsat, (x, y) => x + y).ToList();

            });

            //List<double> totals = resultSatisfaction.Aggregate((prev, next) => prev.Zip(next, (a, b) => a + b).ToList() );
        
            new Histogram(title, resultSatisfaction).Show();

        }

        void WriteLine(string txt)
        {
            textBox1.Text += txt + "\r\n";
        }

    }

}
