using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Collections.Generic;
using System.Diagnostics;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        Trader t1, t2;
        List<Entity> pf1 = new List<Entity>() { new Entity(0), new Entity(1), new Entity(0), new Entity(1) };
        double[] dp1 = new double[] { 0.1, 0.9 };

        [TestInitialize()]
        public void Initialize()
        {
            //t1 = new Trader(pf1, dp1);
        }


        [TestMethod]
        public void TraderDesire()
        {
            //Assert.IsTrue(t1.DesireFor(new Entity(0)) == dp1[0]);
            //Assert.IsTrue(t1.DesireFor(new Entity(1)) == dp1[1]);
        }

        [TestMethod]
        public void TraderSatisfaction()
        {
            Assert.IsTrue(t1.Satisfaction() == 0.1 * 2 + 0.9 * 2);
        }

        //Check if coms are refs or copies

        [TestMethod]
        public void CapitalistSatisfaction()
        {
            //Before capitalism
            Assert.IsTrue(t1.Satisfaction() == 0.1 * 2 + 0.9 * 2);
            Assert.IsTrue(t1.Satisfaction() == 2);

            //Owns t1
            List<Entity> pf2 = new List<Entity>() {
                new Entity(0),
                new Entity(1),
                new Entity(0),
                t1
            };
            double[] dp2 = new double[] { 0.2, 0.3 };

            //Trader t2 = new Trader(pf2, dp2);

            Assert.IsTrue(t1.Satisfaction() == 2 / 2);
            Assert.IsTrue(t2.Satisfaction() == 0.7 + 2 / 2);
            Assert.IsTrue(t2.Satisfaction() == 1.7);

            //Owns t2, t2 own t1
            List <Entity> pf3 = new List<Entity>() {
                new Entity(0),
                new Entity(1),
                new Entity(0),
                t2
            };
            double[] dp3 = new double[] { 0.4, 0.1 };
/*
            Trader t3 = new Trader(pf3, dp3);

            Assert.IsTrue(t1.Satisfaction() == 2 / 2);
            Assert.IsTrue(t2.Satisfaction() == (0.7 + 1) / 2);
            Assert.IsTrue(t3.Satisfaction() == 0.9 + (0.7 + 1) / 2);
            */
        }


    }
}
