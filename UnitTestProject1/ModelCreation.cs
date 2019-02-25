using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace UnitTestProject1
{

    [TestClass]
    public class ModelCreation
    {
        [TestMethod]
        public void TestWorldCreation()
        {
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            List<Entity> data = World.WorldGenerator(n_trader, n_com_types, pf_size);

            //Right number of entities
            Assert.IsTrue(data.Count == n_trader * pf_size + n_trader);
        }

        [TestMethod]
        public void TestCloning()
        {
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            List<Entity> data = World.WorldGenerator(n_trader, n_com_types, pf_size);
            Debug.WriteLine(Report.printPortfolios(data.Where(e => e.type == Trader.TRADER_TYPE).Select(e => e as Trader).ToList()));

            List<Entity> data1 = data.Clone();

            //does this change data1. Is it references?
            data = World.WorldGenerator(n_trader, n_com_types, pf_size);

            Debug.WriteLine(Report.printPortfolios(data1.Where(e => e.type == Trader.TRADER_TYPE).Select(e => e as Trader).ToList()));

        }


        /*
        [TestMethod]
        public void TestWorldCloning()
        {
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            List<Entity> data = World.WorldGenerator(n_trader, n_com_types, pf_size);

            List<Entity> data1 = data.Clone();

            for (int i = 0; i < data.Count; i++)
            {
                Assert.IsTrue(data[i] != data1[i]);
                Assert.IsTrue(data[i].index == data1[i].index);
                Assert.IsTrue(data[i].owner == data1[i].owner);
                Assert.IsTrue(data[i].type == data1[i].type);

                if (data[i].type == Trader.TRADER_TYPE)
                {
                    //Check desires copied
                    for (int j = 0; j < n_com_types; j++)
                    {
                        Assert.IsTrue(((Trader)data[i]).DesireFor(j) == ((Trader)data1[i]).DesireFor(j));
                    }

                    //Check portfolio same
                    for (int j = 0; j < pf_size; j++)
                    {
                        Assert.IsTrue(((Trader)data[i]).portfolio[j] == ((Trader)data1[i]).portfolio[j]);
                    }

                }
            }
        }
        */
        /*
        [TestMethod]
        public void TestBindModelToWorld()
        {
            Dictionary<int, Entity> world, world1;
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            Market.WorldGenerator(out world, n_trader, n_com_types, pf_size);

            //Check that Markets bind correctly to different worlds
            //A change to one market doesn't change another
            world1 = world.Clone();

            Market m1 = new Market(world1);

            int original = m1.entities[0].owner;

            m1.entities[0].owner = 1000;

            Assert.IsTrue(m1.entities[0].owner == 1000);

            Market m = new Market(world);

            Assert.IsTrue(m.entities[0].owner == original);

        }

        [TestMethod]
        public void TestAddCapitalists() {
            //static public void AddCapitalists(Dictionary<int, Entity> world, int[] capitalists)
            Dictionary<int, Entity> world = new Dictionary<int, Entity>();
            world[0] = new Trader(new int[] { }, new double[] { });
            world[1] = new Trader(new int[] { }, new double[] { });

            World.world = world;

            int[] capitalists = new int[] { 0, 1 };

            Assert.IsTrue(world[1].owner == Entity.FREE);

            Market.AddCapitalists(world, capitalists);

            Assert.IsTrue(world[1].owner == 0);
            Assert.IsTrue(World.world[1].owner == 0);
        }

        [TestMethod]
        public void CapitalistCreation()
        {
            Dictionary<int, Entity> world;
            int n_trader = 3;
            int pf_size = 2;
            int n_com_types = 5;

            Market.WorldGenerator(out world, n_trader, n_com_types, pf_size);

            //0..5 = coms
            //6..8 = traders = {0..2}

            Assert.IsFalse(Trader.IsTrader(world[5]));
            Assert.IsTrue(Trader.IsTrader(world[6]));

            Assert.IsTrue(world[6].owner == Entity.FREE);

            
            int[] capitalists = new int[]
            {
                2,0 //2 owns 0
            };

            Market.AddCapitalists(world, capitalists);

            Assert.IsTrue(world[6].owner == 8);
            Assert.IsTrue(world[8].Cast<Trader>().portfolio.Contains(6) );

        }

        [TestMethod]
        public void SecondTest()
        {
            const int N_TRADERS = 10;
            const int N_COM_TYPES = 5;
            const int SIZE_COM_PORTFOLIO = 10;

            //Make the world & clone for each run replicate
            Dictionary<int, Entity> world, world2;

            Market.WorldGenerator(out world, N_TRADERS, N_COM_TYPES, SIZE_COM_PORTFOLIO);

            world2 = world.Clone();
            World.SetBankWorld(world2);

            foreach(KeyValuePair<int, Entity> e in world2)
            {
                if (Trader.IsTrader(e.Value))
                    Assert.IsTrue(e.Value.owner == Entity.FREE);
            }

            //Coms end at 99
            //traders start at 100

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

            Market.AddCapitalists(world2, capitalists);

            int baseCapitalist = 100;

            for(int i = 0; i<capitalists.Length;i+=2)
            {
                Trader owner = (Trader)world2[baseCapitalist + capitalists[i]];
                Trader worker = (Trader)world2[baseCapitalist + capitalists[i+1]];

                Assert.IsTrue(worker.owner == owner.index);

            }

        }
        */
    }

}
