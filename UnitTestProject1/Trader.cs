using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// Run these test separately as some of them change the world
/// </summary>
namespace Traders
{
    [TestClass]
    public class TraderTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            //This is shit
            //we should inject bank
            Bank.world = new Dictionary<int, Entity>();

            for (int i=0;i<9;i++)
            {
                Bank.world[i] = new Entity(1);
            }

            double[] dp = new double[] { 0.1, 0.2, 0.3 };

            Bank.world[9] = new Trader(new int[] { 0, 1 }, dp);
            Bank.world[10] = new Trader(new int[] { 2, 3 }, dp);
            Bank.world[11] = new Trader(new int[] { 4, 5 }, dp);
            Bank.world[12] = new Trader(new int[] { 6, 7 }, dp);

            //Note: bit shakey if a mistake is made then ID++ goes out of sync with external

        }

        [TestCleanup()]
        public void Cleanup()
        {
            Bank.world.Clear();
        }

        [TestMethod]
        public void TraderSetUpTest()
        {
            //All entities are type 1
            Trader t1 = Bank.world[10].Cast<Trader>();
            Trader t2 = Bank.world[11].Cast<Trader>();

            Assert.AreEqual(t1.DesireFor(1), 0.2, "Desire wrong");

        }

        [TestMethod]
        public void TraderTakeRelease()
        {
            Trader t1 = Bank.world[10].Cast<Trader>();
            Trader t2 = Bank.world[11].Cast<Trader>();

            Assert.AreEqual(2, t1.portfolio.Count, "Count wrong after take");
            Assert.AreEqual(2, t2.portfolio.Count, "Count wrong after take");

            int take8 = 8;
            t1.Take(take8);

            Assert.AreEqual(3,t1.portfolio.Count, "Count wrong after take");
            Assert.IsTrue(t1.portfolio.IndexOf(take8) > -1, "Does't have after a take");
            Assert.AreEqual(2, t2.portfolio.Count, "Count wrong after take");

            //Exchange (really just a take with release. TODO update take)
            Trader.Exchange(t1.index,t2.index, take8, 4);
            Assert.AreEqual(3, t1.portfolio.Count, "Count wrong after exchange");
            Assert.AreEqual(2, t2.portfolio.Count, "Count wrong after exchange");
            Assert.IsTrue(t1.portfolio.IndexOf(3) > -1, "Does't have after a take");
            Assert.IsTrue(t2.portfolio.IndexOf(take8) > -1, "Does't have after a take");

        }

        [TestMethod]
        public void AddCapitalist()
        {
            Trader t1 = Bank.world[10].Cast<Trader>();
            Trader t2 = Bank.world[11].Cast<Trader>();

            int t2size = t2.portfolio.Count;

            t2.Take(t1.index);

            Assert.IsTrue(t2.portfolio.Count == t2size + 1);

            Assert.IsTrue(t1.owner == t2.index);

        }

        [TestMethod]
        public void CapitalistImpactOnSatisfaction()
        {
            //Each have 3 type 1 entities with dp 0.2

            Trader t0 = Bank.world[9].Cast<Trader>();
            Trader t1 = Bank.world[10].Cast<Trader>();
            Trader t2 = Bank.world[11].Cast<Trader>();
            Trader t3 = Bank.world[12].Cast<Trader>();

            Assert.IsTrue(Math.Round(t0.Satisfaction(), 5) == 0.4);  //weird rounding error
            Assert.IsTrue(Math.Round(t1.Satisfaction(), 5) == 0.4);  //weird rounding error
            Assert.IsTrue(Math.Round(t2.Satisfaction(), 5) == 0.4);  //weird rounding error
            Assert.IsTrue(Math.Round(t3.Satisfaction(), 5) == 0.4);  //weird rounding error
            //Total = 1.6

            //Add Capitalist
            t2.Take(t1.index);

            Assert.IsTrue(Math.Round(t0.Satisfaction(), 5) == 0.4);
            Assert.IsTrue(Math.Round(t1.Satisfaction(), 5) == 0.2);
            Assert.IsTrue(Math.Round(t2.Satisfaction(), 5) == 0.6);
            Assert.IsTrue(Math.Round(t3.Satisfaction(), 5) == 0.4);
            //Total = 1.6


            //Desire is split at each level
            t3.Take(t2.index);
            Assert.IsTrue(Math.Round(t0.Satisfaction(), 5) == 0.4);
            Assert.IsTrue(Math.Round(t1.Satisfaction(), 5) == 0.2);
            Assert.IsTrue(Math.Round(t2.Satisfaction(), 5) == 0.3);
            Assert.IsTrue(Math.Round(t3.Satisfaction(), 5) == 0.7);
            //Total = 1.6

            //Desire is split at each level
            //    0   1
            //      2
            //      3
            t2.Take(t0.index);
            Assert.IsTrue(Math.Round(t0.Satisfaction(), 5) == 0.2);
            Assert.IsTrue(Math.Round(t1.Satisfaction(), 5) == 0.2);
            Assert.IsTrue(Math.Round(t2.Satisfaction(), 5) == 0.4);
            Assert.IsTrue(Math.Round(t3.Satisfaction(), 5) == 0.8);
            //Total = 1.6

        }

        /*
        Test for unused offer/accept trading

        [TestMethod]
        public void Offers()
        {

            //t1 has 2x 
            //Init(new double[] { 0.2, 0.3, 0.4, 0.5 }, null);

            Entity c;
            List<Entity> offers;

            c = new Commodity(0);
            offers = t1.offerCommodity(c);
            Assert.AreEqual(1, offers.Count);
            
            c = new Commodity(1);
            offers = t1.offerCommodity(c);
            Assert.AreEqual(2, offers.Count);

            c = new Commodity(2);
            offers = t1.offerCommodity(c);
            Assert.AreEqual(3, offers.Count);

            c = new Commodity(3);
            offers = t1.offerCommodity(c);
            Assert.AreEqual(4, offers.Count);
            
        }

        [TestMethod]
        public void Consider()
        {
            //t1 has 2x 
            //Init(new double[] { 0.2, 0.3, 0.4, 0.5 }, null);

            List<Entity> ls = new List<Entity>() {new Entity(0)};
            
            I_Commodity swap = t1.considerOffers( ls );
            Assert.AreEqual(null, swap);

            ls.Clear();
            ls.Add(new Commodity(1));
            swap = t1.considerOffers(ls);
            Assert.AreEqual(0, swap.type);

            ls.Clear();
            ls.Add(new Commodity(2));
            swap = t1.considerOffers(ls);
            Assert.AreEqual(0, swap.type);
            
        }

        [TestMethod]
        public void tradeTo()
        {
            //Each have 1 of each

            //t1 has 2x 
            //Init(new double[] { 0.2, 0.3, 0.4, 0.5 }, new double[] { 0.5, 0.4, 0.3, 0.2 });

            //1,2,3,4
            //1,2,3,4
            //=>
            //4,3,3,4
            //1,2,2,1
            
            //t1 cycles thru portfolio and swaps with t2
            t1.tradePortfolioTo(t2);

            Debug.WriteLine(t1.portfolio[0].type);
            Debug.WriteLine(t1.portfolio.Count);
            
            string pf1 = "+" + t1.portfolio[0].type + t1.portfolio[1].type + t1.portfolio[2].type + t1.portfolio[3].type;
            string pf2 = "+" + t2.portfolio[0].type + t2.portfolio[1].type + t2.portfolio[2].type + t2.portfolio[3].type;

            Debug.WriteLine(pf1);
            Debug.WriteLine(pf2);

            //Assert.AreEqual("+4334", pf1);
            //Assert.AreEqual("+1221", pf2);
            
        }
        */
    }
}
