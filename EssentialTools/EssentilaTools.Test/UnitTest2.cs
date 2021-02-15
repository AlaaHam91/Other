using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace EssentilaTools.Test
{
    [TestClass]
    public class UnitTest2
    {

        private Product[] prods = {
                  new Product {Name="Prod1", Price=10M },
                  new Product {Name="Prod2", Price=50.5M },
                  new Product {Name="Prod3", Price=60M },
                  new Product {Name="Prod4", Price=70M }

              };
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IDiscountHelper> Mymock = new Mock<IDiscountHelper>();
            Mymock.Setup(l => l.ApplayDiscount(It.IsAny<decimal>())).Returns <decimal>(total=>total);
            var tareget = new LinqValueCalculator(Mymock.Object);
            decimal actual=tareget.ValueProducts(prods);

            Assert.AreEqual(prods.Sum(p => p.Price),actual);

    }
     
        private  Product [] GetProduct(decimal val)
        {

            return   new[] { new Product { Price = val } } ;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestComplex()
        {


            Mock<IDiscountHelper> m = new Mock<IDiscountHelper>();
          //  m.Setup(l => l.ApplayDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

              m.Setup(i => i.ApplayDiscount(It.Is<decimal>(v=>v==5))).Returns<decimal>(total => total );
            m.Setup(i => i.ApplayDiscount(It.Is<decimal>(v => v > 100))).Returns <decimal> (total => (total*0.9M));
              m.Setup(i => i.ApplayDiscount(It.IsInRange<decimal>(10,100,Range.Inclusive))).Returns<decimal>(total => (total - 5));
               m.Setup(i => i.ApplayDiscount(It.Is<decimal>(v => v == 0))).Throws<ArgumentOutOfRangeException>();


            var test1 = GetProduct(500);

            var target = new LinqValueCalculator(m.Object);

            decimal t1= target.ValueProducts(GetProduct(5));
            decimal t2 = target.ValueProducts(GetProduct(10));
            decimal t3 = target.ValueProducts(GetProduct(50));
            decimal t4 = target.ValueProducts(GetProduct(100));
            decimal t5 = target.ValueProducts(GetProduct(500));

            Assert.AreEqual(5,t1,"TEST 5$");
            Assert.AreEqual(5, t2, "TEST 10$");
            Assert.AreEqual(45, t3, "TEST 50$");
            Assert.AreEqual(95, t4, "TEST 100$");
            Assert.AreEqual(450, t5, "TEST 500$");

            target.ValueProducts(GetProduct(0));


        }
    }
}
