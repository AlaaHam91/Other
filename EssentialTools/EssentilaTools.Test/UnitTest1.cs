using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;

namespace EssentilaTools.Test
{
    [TestClass]
    public class UnitTest1
    {
        private  IDiscountHelper GetDiscountHelper()
        {
            return new MinimumDiscountHelper();
        }
        [TestMethod]
        public void TestMethod1()
        {
            IDiscountHelper d = GetDiscountHelper();
            decimal total = 100;
            var a= d.ApplayDiscount(total);
            Assert.AreEqual(total * 0.9M, a);
        }
        [TestMethod]
        public void TestMethod10_100()
        {
            IDiscountHelper d = GetDiscountHelper();
            decimal total = 100;
            var a = d.ApplayDiscount(total);
            Assert.AreEqual(total * 0.3M, a,"Me Message");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Negative()
        {
            decimal total = -10;
            IDiscountHelper d = GetDiscountHelper();
            var v = d.ApplayDiscount(total);
        }
    }
}
