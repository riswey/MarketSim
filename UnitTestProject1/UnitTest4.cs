using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Collections.Generic;
using System.Diagnostics;


namespace UnitTestProject1
{

    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestWorldCreation()
        {
            Dictionary<int, Entity> world;
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            Market.GenerateWorld(out world, n_trader, n_com_types, pf_size);

            //Right number of entities
            Assert.IsTrue(world.Count == n_trader * pf_size + n_trader);

            //Check ownership
            int traderstartindex = n_trader * pf_size;
            for (int i = 0; i < n_trader; i++)
            {
                for (int j = 0; j < pf_size; j++)
                {
                    int index = i * pf_size + j;
                    //Commodity indexing correct
                    Assert.IsTrue(world[index].index == index);
                    //Ownerd by sequential traders
                    Assert.IsTrue(world[index].owner == traderstartindex + i);
                }
            }

        }

        [TestMethod]
        public void TestWorldCloning()
        {
            Dictionary<int, Entity> world, world1;
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            Market.GenerateWorld(out world, n_trader, n_com_types, pf_size);

            //Test this
            world1 = world.Clone();

            for (int i = 0; i < world.Count; i++)
            {
                Assert.IsTrue(world[i] != world1[i]);
                Assert.IsTrue(world[i].index == world1[i].index);
                Assert.IsTrue(world[i].owner == world1[i].owner);
                Assert.IsTrue(world[i].type == world1[i].type);

                if (world[i].type == Trader.TRADER_TYPE)
                {
                    //Check desires copied
                    for (int j = 0; j < n_com_types; j++)
                    {
                        Assert.IsTrue(((Trader)world[i]).DesireFor(j) == ((Trader)world1[i]).DesireFor(j));
                    }

                    //Check portfolio same
                    for (int j = 0; j < pf_size; j++)
                    {
                        Assert.IsTrue(((Trader)world[i]).portfolio[j] == ((Trader)world1[i]).portfolio[j]);
                    }

                }
            }
        }

        [TestMethod]
        public void TestBindModelToWorld()
        {
            Dictionary<int, Entity> world, world1;
            int n_trader = 8;
            int pf_size = 5;
            int n_com_types = 5;

            Market.GenerateWorld(out world, n_trader, n_com_types, pf_size);

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


    }

}
