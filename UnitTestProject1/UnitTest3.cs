using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traders;
using System.Collections.Generic;
using System.Diagnostics;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void Gini()
        {
            List<double> values = new List<double>() { 0.1, 0.2, 0.3, 0.4 };

            double res = Market.Gini(values);

            Assert.IsTrue(res == 0);

            List<double> v1 = new List<double>() { 0.1, 0.15, 0.17, 0.4 };

            Debug.WriteLine("gini " + Market.Gini(v1));

        }
    }
}
