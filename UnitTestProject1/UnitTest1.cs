using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;


namespace Traders
{
    [TestClass]
    public class UnitTest1
    {
        Trader t1, t2;

        [TestInitialize()]
        public void Initialize()
        {
        }
        /*
        void Init(double[] dp1, double[] dp2) {
            t1 = new Trader(new List<Commodity>() { new Commodity(0), new Commodity(1), new Commodity(2), new Commodity(3) }, dp1);
            t2 = new Trader(new List<Commodity>() { new Commodity(0), new Commodity(1), new Commodity(2), new Commodity(3) }, dp2);
        }
        */
        [TestCleanup()]
        public void Cleanup()
        {
            //ought clear portfolios
            t1 = null;
            t2 = null;
        }

        [TestMethod]
        public void TraderSetUpTest()
        {
            /*
            t1.desireProfile = new double[] { 0.1, 0.1, 0.25, 0.5 };

            Assert.IsTrue(t1.desireProfile[0] > 0, "Profile Not set correctly");

            Assert.AreEqual(t1.desireFor(new Commodity(2)), 0.25, "Desire wrong");

            //Test desireFor
            Assert.AreEqual(t1.desireProfile[1], t1.desireFor(new Commodity(1)), "Desire wrong");

            //portfolio 0 is type 1
            Assert.AreEqual(t1.desireProfile[1], t1.desireFor(t1.portfolio[1]), "Portfolio index wrong");
            //portfolio 0 is type 2
            Assert.AreEqual(t1.desireProfile[2], t1.desireFor(t1.portfolio[2]), "Portfolio index wrong");

            //2 * (type 1 + type 2)
            Assert.AreEqual(t1.satisfaction(), t1.desireFor(new Commodity(0)) + t1.desireFor(new Commodity(1)) + t1.desireFor(new Commodity(2)) + t1.desireFor(new Commodity(3)), "Satisfaction error");
            */
        }

        [TestMethod]
        public void TraderTakeRelease()
        {
            var c = new Entity(3);
            /*
            t1.Take( c );
            Assert.AreEqual(5,t1.portfolio.Count, "Count wrong after take");
            Assert.IsTrue(t1.portfolio.IndexOf( c ) > -1, "Does't have after a take");
            Assert.IsFalse(t1.portfolio.IndexOf(new Commodity(3)) > -1, "Has something it didn't take.");

            t1.Release( new Commodity(3) );
            Assert.AreEqual(5, t1.portfolio.Count, "Count wrong after failed release");
            Assert.IsTrue(t1.portfolio.IndexOf(c) > -1, "Released something it doesn't have");
            t1.Release( c );
            Assert.AreEqual(4, t1.portfolio.Count, "Count wrong after release");
            Assert.IsFalse(t1.portfolio.IndexOf(c) > -1, "Still has something released");
            */
        }

        [TestMethod]
        public void Offers()
        {

            //t1 has 2x 
            //Init(new double[] { 0.2, 0.3, 0.4, 0.5 }, null);

            Entity c;
            List<Entity> offers;
            /*
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
            */
        }

        [TestMethod]
        public void Consider()
        {
            //t1 has 2x 
            //Init(new double[] { 0.2, 0.3, 0.4, 0.5 }, null);

            List<Entity> ls = new List<Entity>() {new Entity(0)};
            /*
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
            */
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
            /*
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
            */
        }
    }
}
